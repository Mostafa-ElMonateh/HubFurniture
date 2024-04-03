using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Repositories
{
    public interface IAddressReposatory : IGenericRepository<UserAddress>
    {
        Task<IReadOnlyList<UserAddress>> GetAdressesForUserAsync(string UserId);
    }
}
