using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> specifications);
        Task<T?> GetWithSpecAsync(ISpecifications<T> specifications);
    }
}
