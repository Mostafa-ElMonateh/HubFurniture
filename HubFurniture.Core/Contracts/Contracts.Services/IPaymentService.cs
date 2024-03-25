using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Contracts.Contracts.Services
{
    public interface IPaymentService
    {
        public Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
