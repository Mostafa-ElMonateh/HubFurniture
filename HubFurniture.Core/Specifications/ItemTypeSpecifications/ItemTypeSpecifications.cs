using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ItemTypeSpecifications
{
    public class ItemTypeSpecifications : BaseSpecifications<CategoryItemType>
    {
        public ItemTypeSpecifications() : base()
        {
            AddOrderByDesc(cit => cit.Id);
            AddIncludes();
        }

        public ItemTypeSpecifications(int id)
            : base(cit => cit.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(ci => ci.Category);
        }
    }
}
