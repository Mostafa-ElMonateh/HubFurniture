using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
        : base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(O => O.OrderItems);

            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpecifications(int orderId, string buyerEmail)
            : base(o => o.BuyerEmail == buyerEmail && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
        }
    }
}
