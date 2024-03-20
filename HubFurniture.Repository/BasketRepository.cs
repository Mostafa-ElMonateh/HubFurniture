using System.Text.Json;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Entities;
using StackExchange.Redis;

namespace HubFurniture.Repository
{
    public class BasketRepository : IBasketRepository
    {

        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket?> GetBasketAsync(string BasketId)
        {
            var basket = await _database.StringGetAsync(BasketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var createdOrUpdated = await _database.StringSetAsync(customerBasket.BasketId, JsonSerializer.Serialize(customerBasket),
                TimeSpan.FromDays(30));
            if (createdOrUpdated is false)
            {
                return null;
            }

            return await GetBasketAsync(customerBasket.BasketId);
        }

        public async Task<bool> DeleteBasketAsync(string BasketId)
        {
            return await _database.KeyDeleteAsync(BasketId);
        }
    }
}
