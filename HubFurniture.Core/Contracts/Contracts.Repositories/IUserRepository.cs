using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task UpdateBasketId(string userId, string basketId);
        Task<ApplicationUser?> GetUserById(string userId); 
        Task<int> SaveChangesAsync();
    }
}
