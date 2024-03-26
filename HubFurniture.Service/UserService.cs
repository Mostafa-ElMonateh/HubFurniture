using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UpdateBasketId(string userId, string basketId)
        {
            await _userRepository.UpdateBasketId(userId, basketId);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<string?> GetBasketId(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return user?.BasketId;
        }
    }
}
