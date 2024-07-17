using DesafioPitang.Business.Interface.IBusinesses;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Models;
using DesafioPitang.Repository.Interface.IRepositories;
using System.Security.Cryptography;
using System.Text;

namespace DesafioPitang.Business.Businesses
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Register(UserRegistrationModel userModel)
        {
            var user = new User
            {
                Name = userModel.Name,
                Email = userModel.Email,
                CreatedAt = DateTime.Now,
                Profile = userModel.Profile,
            };

            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userModel.Password));
            user.PasswordSalt = hmac.Key;

            await _userRepository.Insert(user);
        }
    }
}
