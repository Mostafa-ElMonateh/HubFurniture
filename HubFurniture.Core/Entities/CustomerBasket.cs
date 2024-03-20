using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class CustomerBasket
    {
        public string BasketId { get; set; }
        public List<BasketItem> BasketItems{ get; set; }

        public CustomerBasket()
        {
            
        }
        public CustomerBasket(string id)
        {
            BasketId = id;
            BasketItems = new List<BasketItem>();
        }
    }

}
