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
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _dbContext;

        public UserRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task UpdateBasketId(string userId, string basketId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.BasketId = basketId;
            }
            
        }

        public async Task<ApplicationUser?> GetUserById(string userId)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
