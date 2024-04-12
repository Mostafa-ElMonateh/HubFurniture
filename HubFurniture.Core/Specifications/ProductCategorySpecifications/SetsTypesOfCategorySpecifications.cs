using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.ProductCategorySpecifications
{
    public class SetsTypesOfCategorySpecifications : BaseSpecifications<CategorySetType>
    {
        public SetsTypesOfCategorySpecifications(int categoryId)
            : base(cit => cit.CategoryId == categoryId)
        {

        }
    }
}
