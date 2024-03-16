using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductItemSpecifications
{
    public class ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications : BaseSpecifications<ProductItem>
    {

        // This Constructor will be used for creating an Object, that will be used to get all productItems
        public ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications()
            :base()
        {
            AddIncludes();
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(pi => pi.ProductCollections);
            Includes.Add(pi => pi.ProductPictures);
            Includes.Add(pi => pi.CustomerReviews);
        }
    }
}
