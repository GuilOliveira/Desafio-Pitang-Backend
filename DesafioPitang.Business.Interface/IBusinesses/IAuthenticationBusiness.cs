using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Models;

namespace DesafioPitang.Business.Interface.IBusinesses
{
    public interface IAuthenticationBusiness
    {
        Task<UserTokenDTO> Login(string email, string password);
        Task<UserTokenDTO> RefreshToken();
    }
}
