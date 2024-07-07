using DesafioPitang.Entities.Entities;

namespace DesafioPitang.Repository.Interface.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetById(object id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(IEnumerable<TEntity> entities);
        Task DeleteById(object id);
        Task<bool> Exists(TEntity entity);
        Task<bool> ExistsById(object id);
    }
}
