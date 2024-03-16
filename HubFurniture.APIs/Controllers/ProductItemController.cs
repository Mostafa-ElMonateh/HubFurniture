using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductItemSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    public class ProductItemController : BaseApiController
    {
        private readonly IGenericRepository<ProductItem> _productRepo;
        private readonly IMapper _mapper;

        public ProductItemController(IGenericRepository<ProductItem> productRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemToReturnDto>>> GetProductItems()
        {
            var specifications = new ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications();
            var productItems = await _productRepo.GetAllWithSpecAsync(specifications);
            var mappedProductItems = _mapper.Map<IEnumerable<ProductItem>, IEnumerable<ProductItemToReturnDto>>(productItems);
            return Ok(mappedProductItems);
        }

        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemToReturnDto>> GetProductItem(int id)
        {
            var specifications = new ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications(id);

            var productItem = await _productRepo.GetWithSpecAsync(specifications);

            if (productItem is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductItem = _mapper.Map<ProductItem, ProductItemToReturnDto>(productItem);

            return Ok(mappedProductItem);
        }


    }
}
