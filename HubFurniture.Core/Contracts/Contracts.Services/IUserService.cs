using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Services
{
    public interface IUserService 
    {
        Task UpdateBasketId(string basketId, string userId);
        Task<string?> GetBasketId(string userId);
        Task<ApplicationUser?> GetUserById(string userId);

    }
}
