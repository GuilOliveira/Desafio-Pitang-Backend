using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }
        public async Task<User> GetByEmail(string email)
        {
            var a = await EntitySet.FirstOrDefaultAsync(user => user.Email == email);
            return await EntitySet.FirstOrDefaultAsync(user=>user.Email == email);
        }
    }
}
