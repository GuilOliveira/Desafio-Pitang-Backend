using DesafioPitang.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Repository.Interface.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetById(object id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Insert(TEntity entity);
        Task Insert(IEnumerable<TEntity> entity);
        Task<TEntity> Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(IEnumerable<TEntity> entity);
        Task DeleteById(object id);
    }
}
