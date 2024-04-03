using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Repository
{
    public class AddressReposatory : GenericRepository<UserAddress>, IAddressReposatory
    {
        public AddressReposatory(StoreContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<UserAddress>> GetAdressesForUserAsync(string userId)
        {
            return await _dbContext.Set<UserAddress>().Where(u => u.UserId == userId).ToListAsync();
        }

    }
}
