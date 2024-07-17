using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Configuration;
using DesafioPitang.Utils.Extensions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Utils.UserContext;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DesafioPitang.Business.Businesses
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthConfiguration _authConfig;
        private readonly IUserContext _userContext;
        public AuthenticationBusiness(IUserRepository userRepository,
                                   IOptionsMonitor<AuthConfiguration> authConfig,
                                   IUserContext userContext)
        {
            _userRepository = userRepository;
            _authConfig = authConfig.CurrentValue;
            _userContext = userContext;
        }

        public async Task<UserTokenDTO> Login(string email, string password)
        {
            var isUserValid = await Authenticate(email, password);
            var user = await _userRepository.GetByEmail(email);
            string token;
            string refreshToken;

            if (isUserValid && user != null)
            {
                token = GenerateToken(user);
                refreshToken = GenerateRefreshToken(user);
            }
            else
                throw new UnauthorizedAccessException(BusinessMessages.InvalidLogin);

            return new UserTokenDTO(token, refreshToken);
        }

        public async Task<UserTokenDTO> RefreshToken()
        {
            var login = UserContextExtensions.Login(_userContext);
            var usuario = await _userRepository.GetByEmail(login);
            string token;
            string refreshToken;

            if (usuario != null)
            {
                token = GenerateToken(usuario);
                refreshToken = GenerateRefreshToken(usuario);
            }
            else
                throw new UnauthorizedAccessException();

            return new UserTokenDTO(token, refreshToken);
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmail(email);

            if (user == null)
                return false;

            using var hmac = new HMACSHA512(user.PasswordSalt);

            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password))
                       .SequenceEqual(user.PasswordHash);
        }

        public string GenerateToken(User user)
        {
            var expiration = DateTime.Now.AddMinutes(_authConfig.AccessTokenExpiration);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, user.Profile),
                new(ClaimTypes.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken(User user)
        {
            var expiration = DateTime.Now.AddMinutes(_authConfig.RefreshTokenExpiration);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email)
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authConfig.Issuer,
                audience: _authConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
