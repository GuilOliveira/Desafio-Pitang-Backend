using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }
        public async Task<User> GetByEmail(string email)
        {
            return await EntitySet.FirstOrDefaultAsync(user=>user.Email == email);
        }
    }
}
