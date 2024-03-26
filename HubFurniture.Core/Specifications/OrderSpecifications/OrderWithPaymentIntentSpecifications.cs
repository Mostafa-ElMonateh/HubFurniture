using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.Core.Specifications.OrderSpecifications
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId)
            :base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
