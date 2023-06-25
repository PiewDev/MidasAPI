using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Midas.Net.Domain;
using Midas.Net.Domain.Crud;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Midas.Net.Database.Crud
{
    public class CrudRepository<TEntity> : ICrudRepository<TEntity>
    where TEntity : class
    {
        protected readonly CommerceDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly dynamic _dbSet;
        public CrudRepository(CommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _dbSet = GetDbSetInstance();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            dynamic result = await CallWithResponse("ToListAsync");
            
            return _mapper.Map<List<TEntity>>(result);
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            dynamic result = await FindById(id);

            return _mapper.Map<TEntity>(result);
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var dbEntity = MapToDb(entity);

            Call("Add", dbEntity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TEntity>(dbEntity);
        }
       
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            object dbEntity = MapToDb(entity);
            Call("Update", dbEntity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TEntity>(dbEntity);

        }
        public async Task DeleteAsync(long id)
        {
            var entity = await FindById(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                Call("Update", entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        private object MapToDb(TEntity entity)
        {
            return _mapper.Map(entity, typeof(TEntity), DbMappingAttribute.GetDbEntity<TEntity>());
        }
        private void Call(string method, object dbEntity)
        {
            var addMethod = _dbSet.GetType().GetMethod(method);
            addMethod.Invoke(_dbSet, new[] { dbEntity });
        }
        private async Task<dynamic> CallWithResponse(string method, object dbEntity)
        {
            var addMethod = _dbSet.GetType().GetMethod(method);
            return await addMethod.Invoke(_dbSet, new[] { dbEntity });
        }
        private async Task<dynamic> CallWithResponse(string method)
        {
            var addMethod = _dbSet.GetType().GetMethod(method);
            return await addMethod.Invoke(_dbSet);
        }
        async Task<dynamic> FindById(long id)
        {
            var findMethod = _dbSet.GetType().GetMethod("FindAsync", new[] { typeof(object[])});
            var entity = await findMethod.Invoke(_dbSet, new object[] { new object[] { id } });

            return entity;
        }       

        dynamic GetDbSetInstance()
        {
           
            var dbSetType = DbMappingAttribute.GetDbEntity<TEntity>();
            
            var genericDbSetType = typeof(DbSet<>).MakeGenericType(dbSetType);
           
            var setMethod = _dbContext.GetType().GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(dbSetType);

            var dbSet = setMethod?.Invoke(_dbContext, null);

            return dbSet;
        }
    }
    

}
