using DesafioPitang.Utils.UserContext;
using DesafioPitang.Utils.Extensions;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesafioPitang.WebApi.Middlewares
{
    public class UserContextMiddleware : IMiddleware
    {
        private readonly IUserContext _userContext;

        public UserContextMiddleware(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (SetUser(context))
                await next.Invoke(context);
        }

        private bool SetUser(HttpContext context)
        {
            if (IsAuthenticated(context))
            {
                var securityToken = GetSecurityToken(context);

                SetUserContext(context, securityToken);

                return true;
            }
            else
            {
                throw new UnauthorizedAccessException("Usuário não autorizado");
            }
        }

        private static JwtSecurityToken GetSecurityToken(HttpContext context)
        {
            var authToken = context.Request.Headers["Authorization"].String();

            if (authToken != null && authToken.Trim().Length > 0)
            {
                var token = authToken.Replace("Bearer", string.Empty).Trim();
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            }

            return null;
        }

        private static bool IsAuthenticated(HttpContext context)
        {
            var authToken = context.Request.Headers["Authorization"].String();

            if (!string.IsNullOrEmpty(authToken))
                return (context.User?.Identity?.IsAuthenticated ?? false) || !string.IsNullOrEmpty(authToken);
            else
                return true;
        }

        private void SetUserContext(HttpContext context, JwtSecurityToken securityToken)
        {
            _userContext.RequestId = Guid.NewGuid();
            _userContext.StartDateTime = DateTime.UtcNow;
            _userContext.SourceInfo = new SourceInfo
            {
                IP = context?.Connection?.RemoteIpAddress,
                Data = GetAllHeaders(context)
            };

            if (securityToken != null && securityToken.Claims.Any())
            {
                var userId = securityToken.Claims.GetClaimValue(ClaimTypes.Sid);
                var userName = securityToken.Claims.GetClaimValue(ClaimTypes.Name);
                var roles = securityToken.Claims.GetValuesOfType(ClaimTypes.Role);
                var email = securityToken.Claims.GetClaimValue(ClaimTypes.Email);

                _userContext.AddData("Id", userId);
                _userContext.AddData("Name", userName);
                _userContext.AddData("Role", roles);
                _userContext.AddData("Email", email);
            }
        }

        private static Hashtable GetAllHeaders(HttpContext context)
        {
            var hashtable = new Hashtable();
            var requestHeaders = context?.Request?.Headers;

            if (requestHeaders == null)
                return hashtable;

            foreach (var header in requestHeaders)
                hashtable.Add(header.Key, header.Value);

            return hashtable;
        }
    }
}
