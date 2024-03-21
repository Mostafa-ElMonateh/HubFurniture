using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<List<T>> GetAllWithCredentialAsync(Expression<Func<T, bool>> criteria);
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        Task<T?> GetWithSpecAsync(ISpecifications<T> specifications);
        Task<int> GetCountAsync(ISpecifications<T> specifications);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
