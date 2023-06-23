using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Midas.Net.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Midas.Net.Database.Crud
{
    public class CrudRepository<TEntity, TId> : IRepository<TEntity, TId>
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

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            dynamic result = await findByAsync(id);

            return _mapper.Map<TEntity>(result);
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var dbEntity = MapToDb(entity);

            Call("Add", dbEntity);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<TEntity>(dbEntity);
        }

       
        public async Task UpdateAsync(TEntity entity)
        {
            object dbEntity = MapToDb(entity);
            Call("Update", dbEntity);

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(TId id)
        {
            var entity = await findByAsync(id);

            if (entity != null)
            {
                entity.IsDeleted = true;
                Call("Update", entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await GetFilteredEntitiesAsync(predicate);
            return _mapper.Map<List<TEntity>>(result);
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
        public async Task<List<TEntity>> GetFilteredEntitiesAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var whereMethod = _dbSet.GetType().GetMethod("Where");
            var toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions).GetMethod("ToListAsync");

            
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var predicateExpression = Expression.Lambda<Func<TEntity, bool>>(predicate, parameter);

            
            var filteredDbSet = whereMethod.Invoke(_dbSet, new object[] { predicateExpression });

            
            var resultListTask = (Task)toListAsyncMethod.MakeGenericMethod(typeof(TEntity)).Invoke(null, new object[] { filteredDbSet });
            await resultListTask;

          
            var resultList = (IEnumerable<TEntity>)resultListTask.GetType().GetProperty("Result").GetValue(resultListTask);

            return resultList.ToList();
        }
        dynamic GetDbSetInstance()
        {
           
            var dbSetType = DbMappingAttribute.GetDbEntity<TEntity>();
            
            var genericDbSetType = typeof(DbSet<>).MakeGenericType(dbSetType);
           
            var setMethod = _dbContext.GetType().GetMethod("Set", Type.EmptyTypes)?.MakeGenericMethod(dbSetType);

            var dbSet = setMethod?.Invoke(_dbContext, null);

            return dbSet;
        }

        async Task<dynamic> findByAsync(object id)
        {
            var findMethod = _dbSet.GetType().GetMethod("FindAsync", BindingFlags.Public | BindingFlags.Instance);
            var result = await(Task<object>)findMethod.Invoke(_dbSet, new object[] { id });
            return (TEntity)result;
        }
    }

}
