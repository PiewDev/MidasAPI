using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Domain
{
    public interface ICrudService<TDto, TId>
    where TDto : class
{
    Task<List<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(TId id);
    Task CreateAsync(object dto);
    Task UpdateAsync(TDto dto);
    Task DeleteAsync(TId id);
}

}
