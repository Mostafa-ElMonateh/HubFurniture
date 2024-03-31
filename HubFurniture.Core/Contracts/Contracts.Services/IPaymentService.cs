using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.Core.Contracts.Contracts.Services
{
    public interface IPaymentService
    {
        public Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
        Task<Order> UpdatePaymentIntentToSucceededOrFailed(string paymentIntentId, bool isSucceeded);
    }
}
