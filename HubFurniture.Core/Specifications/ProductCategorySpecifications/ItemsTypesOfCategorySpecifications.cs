using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductCategorySpecifications
{
    public class ItemsTypesOfCategorySpecifications : BaseSpecifications<CategoryItemType>
    {
        public ItemsTypesOfCategorySpecifications(int categoryId)
            : base(cit => cit.CategoryId == categoryId)
        {
            
        }
    }
}
