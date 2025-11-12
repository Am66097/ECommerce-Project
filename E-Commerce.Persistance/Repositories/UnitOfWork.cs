using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities;
using E_Commerce.Persistance.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private Dictionary<Type, object> _repositories = [] ; // [] => Equal null 

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var EntityType = typeof(TEntity);

            if (_repositories.TryGetValue(EntityType, out var repository))
                return (IGenericRepository<TEntity, TKey>) repository;

            var newRepo= new GenericRepository<TEntity,TKey>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;

        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
