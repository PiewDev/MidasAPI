using Midas.Net.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Service
{
    public class CrudService<TEntity, TId> : ICrudService<TEntity, TId>
     where TEntity : class
    {
        private readonly IRepository<TEntity, TId> _repository;
        public CrudService(IRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity;
        }

        public async Task CreateAsync(object entity)
        {
            await _repository.CreateAsync((TEntity)entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            await _repository.DeleteAsync(id);
            
        }
    }
}
