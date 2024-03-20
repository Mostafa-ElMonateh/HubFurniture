using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Succeeded")]
        PaymentSucceeded,
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}
