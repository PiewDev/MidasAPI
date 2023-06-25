using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain.Crud
{
    public interface ICrudService<TEntity>
    where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> CreateAsync(object dto);
        Task<TEntity> UpdateAsync(object dto);
        Task DeleteAsync(long id);
    }

}
