using DesafioPitang.Entities.Models;


namespace DesafioPitang.Business.Interface.IBusinesses
{
    public interface IUserBusiness
    {
        Task Register(UserRegistrationModel userModel);
    }
}
