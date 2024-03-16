using HubFurniture.Core.Contracts.Contracts.repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductItemSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    public class ProductItemController : BaseApiController
    {
        private readonly IGenericRepository<ProductItem> _productRepo;

        public ProductItemController(IGenericRepository<ProductItem> productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProductItems()
        {
            var specifications = new ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications();
            var productItems = await _productRepo.GetAllWithSpecAsync(specifications);
            return Ok(productItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItem>> GetProductItem(int id)
        {
            var specifications = new ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications(id);

            var productItem = await _productRepo.GetWithSpecAsync(specifications);

            if (productItem is null)
            {
                return NotFound();
            }

            return Ok(productItem);
        }


    }
}
