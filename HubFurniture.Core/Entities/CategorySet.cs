using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class CategorySet: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductCollection> ProductCollections { get; set; } = new HashSet<ProductCollection>();
        public int CategoryId { get; set; }
        [JsonIgnore] // Avoid Circle Ref
        public Category Category { get; set; }

    }
}
