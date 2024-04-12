using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class CategoryItemType:BaseEntityWithNames
    {
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // Navigational Property 1-M => [M]
        public ICollection<CategoryItem> CategoryItems { get; set; }

    }
}
