using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<CategorySet> CategorySets { get; set; } = new HashSet<CategorySet>();
    }
}
