using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class CustomerReview : BaseEntity
    {
        public int Rate { get; set; }
        public string CustomerName { get; set; }
        public string Review { get; set; }
        public int? CategorySetId { get; set; }
        public int? CategoryItemId { get; set; }


    }
}
