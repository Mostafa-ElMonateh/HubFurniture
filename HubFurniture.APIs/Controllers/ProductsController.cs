using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductItemSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductItem> _productRepo;
        private readonly IGenericRepository<ProductCollection> _productCollectionRepo;
        private readonly IGenericRepository<CategorySet> _categorySetRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<ProductItem> productRepo,
            IGenericRepository<ProductCollection> productCollectionRepo,
            IGenericRepository<CategorySet> categorySetRepo,
            IGenericRepository<Category> categoryRepo,
            IMapper mapper)
        {
            _productRepo = productRepo;
            _productCollectionRepo = productCollectionRepo;
            _categorySetRepo = categorySetRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }



        [HttpGet("items")]
        public async Task<ActionResult<IReadOnlyList<ProductItemToReturnDto>>> GetProductItems()
        {
            var specifications = new ProductItemWithItsCollectionsAndItsPicturesAndItsReviewsSpecifications();
            var productItems = await _productRepo.GetAllWithSpecAsync(specifications);
            var mappedProductItems = _mapper.Map<IReadOnlyList<ProductItem>, IReadOnlyList<ProductItemToReturnDto>>(productItems);
            return Ok(mappedProductItems);
        }



        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("item/{id}")]
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



        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryToReturnDto>>> GetProductsCategory()
        {
            var specifications = new ProductCategoryWithItsSetsSpecifications();
            var categories = await _categoryRepo.GetAllWithSpecAsync(specifications);
            var mappedProductsCategory = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ProductCategoryToReturnDto>>(categories);
            return Ok(mappedProductsCategory);
        }


    }
}
