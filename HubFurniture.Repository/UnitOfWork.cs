using System.Collections;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using HubFurniture.Repository.Data;

namespace HubFurniture.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;

        private Hashtable _repositories = new Hashtable();

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
            
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepository<TEntity>(_storeContext);
                _repositories.Add(key, repository);
            }
            return _repositories[key] as IGenericRepository<TEntity>;
        }

        public async Task<int> CompleteAsync()
        {
            return await _storeContext.SaveChangesAsync();
        }
        public async ValueTask DisposeAsync()
        {
            await _storeContext.DisposeAsync();
        }
    }
}
