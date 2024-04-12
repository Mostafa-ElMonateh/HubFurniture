using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Services
{
    public interface IAddcressService
    {
        Task<IReadOnlyList<UserAddress>> GetAdressesForUserAsync(string userId);
        Task CreateAddressAsync(UserAddress userAddress);
        Task UpdateAddressAsync(UserAddress userAddress);
        Task<UserAddress?> GetAdressByIdForUserAsync(int AddressId);
        Task DeleteAdressByIdForUserAsync(int AddressId);
    }
}
