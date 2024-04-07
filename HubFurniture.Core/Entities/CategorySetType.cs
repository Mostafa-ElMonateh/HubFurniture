using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class CategorySetType : BaseEntityWithNames
    {
        public int? CategoryId { get; set; }

        // Navigational Property 1-M => [M]
        public ICollection<CategorySet> CategorySets { get; set; }

    }
}
