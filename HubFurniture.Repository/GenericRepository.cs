using System.Linq.Expressions;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using HubFurniture.Repository.Data;
using Microsoft.EntityFrameworkCore;
using HubFurniture.Core.Contracts.Contracts.Repositories;

namespace HubFurniture.Repository
{
    public class GenericRepository <T>: IGenericRepository<T> where T : BaseEntity
    {
        protected readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).ToListAsync();
        }

        public async Task<T?> GetWithSpecAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecifications(specifications).CountAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }


        private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
        {
            return SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), specifications);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
