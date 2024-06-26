﻿using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.ProductCategorySpecifications
{
    public class ProductCategorySpecifications: BaseSpecifications<Category>
    {
        // This Constructor will be used for creating an Object, that will be used to get all productItems
        public ProductCategorySpecifications()
            :base()
        {
            AddIncludes();
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ProductCategorySpecifications(int? id)
            :base(c => c.Id == id)
        {
            AddIncludes();
        }


        private void AddIncludes()
        {
            Includes.Add(c => c.CategorySetsTypes);
            Includes.Add(c => c.CategoryItemsTypes);
        }
    }
}
