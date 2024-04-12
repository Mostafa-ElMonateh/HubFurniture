using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class Category : BaseEntityWithNames
    {
        // Navigational Property 1-M => [M]
        public ICollection<CategorySetType> CategorySetsTypes { get; set; } = new HashSet<CategorySetType>();
        
        // Navigational Property 1-M => [M]
        public ICollection<CategoryItemType> CategoryItemsTypes { get; set; } = new HashSet<CategoryItemType>();
        
        [JsonIgnore] // Avoid Circle Ref
        public ICollection<CategorySet> CategorySets { get; set; } = new HashSet<CategorySet>();
        
        [JsonIgnore] // Avoid Circle Ref
        public ICollection<CategoryItem> CategoryItems { get; set; } = new HashSet<CategoryItem>();

    }
}
