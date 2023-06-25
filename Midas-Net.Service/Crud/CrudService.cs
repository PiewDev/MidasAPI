using Midas.Net.Domain;
using Midas.Net.Domain.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Midas.Net.Service.Crud
{
    public class CrudService<TEntity> : ICrudService<TEntity>
     where TEntity : class
    {
        private readonly ICrudRepository<TEntity> _repository;
        public CrudService(ICrudRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity;
        }

        public async Task<TEntity> CreateAsync(object entity)
        {
            return await _repository.CreateAsync((TEntity)entity);
        }

        public async Task<TEntity> UpdateAsync(object entity)
        {
            return await _repository.UpdateAsync((TEntity)entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);

        }
    }
}
