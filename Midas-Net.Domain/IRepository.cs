using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain
{
    public interface IRepository<TEntity, TIdEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TIdEntity idEntity);
        Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TIdEntity idEntity);
    }
}
