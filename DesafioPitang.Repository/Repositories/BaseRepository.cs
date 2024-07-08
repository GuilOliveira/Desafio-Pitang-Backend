using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioPitang.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly Context _context;
        protected virtual DbSet<TEntity> EntitySet { get; }
        public BaseRepository(Context context)
        {
            _context = context;
            EntitySet = _context.Set<TEntity>();
        }
        public async Task Delete(TEntity entity)
        {
            EntitySet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<TEntity> entities)
        {
            EntitySet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(object id)
        {
            var searchedEntity = await EntitySet.FindAsync(id);
            if (searchedEntity != null) { await Delete(searchedEntity); }
        }

        public async Task<List<TEntity>> GetAll() => await EntitySet.ToListAsync();

        public async Task<TEntity> GetById(object id)
        {
            return await EntitySet.FindAsync(id);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            var entityEntry = await EntitySet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task Insert(IEnumerable<TEntity> entities)
        {
            await EntitySet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            var entityEntry = EntitySet.Update(entity);
            await _context.SaveChangesAsync();
            return entityEntry.Entity;
        }

        public async Task<bool> Exists(TEntity entity)
        {
            if (await EntitySet.FindAsync(entity) != null) return true;
            return false;
        }

        public async Task<bool> ExistsById(object id)
        {
            if (await EntitySet.FindAsync(id) != null) return true;
            return false;
        }
    }
}
