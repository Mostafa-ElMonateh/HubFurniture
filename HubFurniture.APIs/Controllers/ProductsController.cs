using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.APIs.Helpers;
using HubFurniture.Core.Contracts.Contracts.Entities;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<CategoryItem> _categoryItemRepo;
        private readonly IGenericRepository<CategorySet> _categorySetRepo;
        private readonly IGenericRepository<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<CategoryItem> productRepo,
            IGenericRepository<CategorySet> categorySetRepo,
            IGenericRepository<Category> categoryRepo,
            IMapper mapper)
        {
            _categoryItemRepo = productRepo;
            _categorySetRepo = categorySetRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }


        // {{BaseUrl}}/api/products/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryToReturnDto>>> GetProductsCategory()
        {
            var specifications = new ProductCategorySpecifications();
            var categories = await _categoryRepo.GetAllWithSpecAsync(specifications);
            var mappedProductsCategory = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ProductCategoryToReturnDto>>(categories);
            return Ok(mappedProductsCategory);
        }



        [HttpGet("sets")]
        public async Task<ActionResult<Pagination<SetFlashCardToReturnDto>>> GetCategorySetsProducts([FromQuery]ProductSpecParams specParams)
        {
            decimal minimumPrice = 0, maximumPrice = 0;
            var specifications = new ProductSetWithItsPicturesItsReviewsSpecifications(specParams);
            var setProducts = await _categorySetRepo.GetAllWithSpecAsync(specifications);
            if (setProducts.Any())
            {
                minimumPrice = setProducts.Min(ci => ci.Price);
                maximumPrice = setProducts.Max(ci => ci.Price);
            }
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategorySet>, IReadOnlyList<SetFlashCardToReturnDto>>(setProducts);
            var countSpecifications = new ProductsSetsWithFilterationForCountSpecifications(specParams);
            int count = await _categorySetRepo.GetCountAsync(countSpecifications);
            return Ok(new Pagination<SetFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }



        [HttpGet("items")]
        public async Task<ActionResult<Pagination<ItemFlashCardToReturnDto>>> GetCategoryItemsProducts([FromQuery]ProductSpecParams specParams)
        {
            decimal minimumPrice = 0, maximumPrice = 0;
            var specifications = new ProductItemWithItsPicturesItsReviewsSpecifications(specParams);
            var setProducts = await _categoryItemRepo.GetAllWithSpecAsync(specifications);
            if (setProducts.Any())
            {
                minimumPrice = setProducts.Min(ci => ci.Price);
                maximumPrice = setProducts.Max(ci => ci.Price);
            }
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategoryItem>, IReadOnlyList<ItemFlashCardToReturnDto>>(setProducts);
            var countSpecifications = new ProductsItemsWithFilterationForCountSpecifications(specParams);
            int count = await _categoryItemRepo.GetCountAsync(countSpecifications);
            return Ok(new Pagination<ItemFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }


        // {{BaseUrl}}/api/products/sets?SetId=1
        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("set")]
        public async Task<ActionResult<ProductItemToReturnDto>> GetProductSet(int setId)
        {
            var specifications = new ProductSetWithItsPicturesItsReviewsSpecifications(setId);

            var productSet = await _categorySetRepo.GetWithSpecAsync(specifications);

            if (productSet is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductSet = _mapper.Map<CategorySet, ProductSetToReturnDto>(productSet);

            return Ok(mappedProductSet);
        }

        // {{BaseUrl}}/api/products/item?ItemId=1
        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("item")]
        public async Task<ActionResult<ProductItemToReturnDto>> GetProductItem(int itemId)
        {
            var specifications = new ProductItemWithItsPicturesItsReviewsSpecifications(itemId);

            var productItem = await _categoryItemRepo.GetWithSpecAsync(specifications);

            if (productItem is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductItem = _mapper.Map<CategoryItem, ProductItemToReturnDto>(productItem);

            return Ok(mappedProductItem);
        }

        // {{BaseUrl}}/api/Products/sets/types?CategoryId=1
        [HttpGet("sets/types")]
        public async Task<ActionResult<CategorySetsToReturnDto>> GetCategorySets([FromQuery]ProductSpecParams specParams)
        {
            // _dbContext.CategoryItems.Where(c => c.Name == Name).CountAsync();

            var specifications = new ProductCategorySpecifications(specParams.CategoryId);
            var categories = await _categoryRepo.GetWithSpecAsync(specifications);
            var mappedProductsCategory = _mapper.Map<Category, CategorySetsToReturnDto>(categories);
            return Ok(mappedProductsCategory);
        }

        // {{BaseUrl}}/api/Products/items/types?CategoryId=1
        [HttpGet("items/types")]
        public async Task<ActionResult<CategoryItemsToReturn>> GetCategoryItems([FromQuery]ProductSpecParams specParams)
        {
            // _dbContext.CategoryItems.Where(c => c.Name == Name).CountAsync();

            var specifications = new ProductCategorySpecifications(specParams.CategoryId);
            var categories = await _categoryRepo.GetWithSpecAsync(specifications);
            var mappedProductsCategory = _mapper.Map<Category, CategoryItemsToReturn>(categories);
            return Ok(mappedProductsCategory);
        }


    }
}
