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
        public ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications()
            :base()
        {
            Includes.Add(pi => pi.ProductCollections);
            Includes.Add(pi => pi.ProductPictures);
            Includes.Add(pi => pi.CustomerReviews);
        }
    }
}
