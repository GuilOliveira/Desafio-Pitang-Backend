using DesafioPitang.Entities.Entities;


namespace DesafioPitang.Repository.Interface.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
