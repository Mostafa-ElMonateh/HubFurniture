using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Service
{
    public class AdressServices : IAddcressService
    {
        private readonly IAddressReposatory _addressRepository;

        public AdressServices(IAddressReposatory addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task CreateAddressAsync(UserAddress userAddress)
        {
            await _addressRepository.AddAsync(userAddress);
            await _addressRepository.SaveChangesAsync();
        }

        public async Task DeleteAdressByIdForUserAsync(int AdderssId)
        {
            var address = await _addressRepository.GetByIdAsync(AdderssId);
            if (address != null)
            {
                _addressRepository.Delete(address);
                await _addressRepository.SaveChangesAsync();
            }
        }

        public async Task<UserAddress?> GetAdressByIdForUserAsync(int AddressId)
        {
            return await _addressRepository.GetByIdAsync(AddressId);
        }

        public async Task<IReadOnlyList<UserAddress>> GetAdressesForUserAsync(string userId)
        {
            return await _addressRepository.GetAdressesForUserAsync(userId);
        }

        public async Task UpdateAddressAsync(UserAddress userAddress)
        {
            _addressRepository.Update(userAddress);
            await _addressRepository.SaveChangesAsync();
        }


    }
}
