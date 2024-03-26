using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.APIs.Helpers;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // {{BaseUrl}}/api/products/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryToReturnDto>>> GetCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            var mappedProductsCategory = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ProductCategoryToReturnDto>>(categories);
            return Ok(mappedProductsCategory);
        }


        // {{BaseUrl}}/api/Products/sets/types?CategoryId=1
        [HttpGet("sets/types")]
        public async Task<ActionResult<CategorySetsToReturnDto>> GetCategorySetsTypes([FromQuery]ProductSpecParams specParams)
        {
            var category = await _productService.GetCategoryByIdAsync(specParams);
            var mappedProductsCategory = _mapper.Map<Category, CategorySetsToReturnDto>(category);
            return Ok(mappedProductsCategory);
        }

        // {{BaseUrl}}/api/Products/items/types?CategoryId=1
        [HttpGet("items/types")]
        public async Task<ActionResult<CategoryItemsToReturn>> GetCategoryItemsTypes([FromQuery]ProductSpecParams specParams)
        {
            // _dbContext.CategoryItems.Where(c => c.Name == Name).CountAsync();

            var category = await _productService.GetCategoryByIdAsync(specParams);
            var mappedProductsCategory = _mapper.Map<Category, CategoryItemsToReturn>(category);
            return Ok(mappedProductsCategory);
        }


        [HttpGet("sets")]
        public async Task<ActionResult<Pagination<SetFlashCardToReturnDto>>> GetSets([FromQuery]ProductSpecParams specParams)
        {
            IReadOnlyList<CategorySet> sets = await _productService.GetSetsAsync(specParams);
            decimal minimumPrice = _productService.GetMinimumPriceOfSets(sets);
            decimal maximumPrice = _productService.GetMaximumPriceOfSets(sets);
            int count = await _productService.GetCountOfSetsAsync(specParams);
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategorySet>, IReadOnlyList<SetFlashCardToReturnDto>>(sets);
            return Ok(new Pagination<SetFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }


        [HttpGet("items")]
        public async Task<ActionResult<Pagination<ItemFlashCardToReturnDto>>> GetItems([FromQuery]ProductSpecParams specParams)
        {
            IReadOnlyList<CategoryItem> items = await _productService.GetItemsAsync(specParams);
            decimal minimumPrice = _productService.GetMinimumPriceOfItems(items);
            decimal maximumPrice = _productService.GetMaximumPriceOfItems(items);
            int count = await _productService.GetCountOfItemsAsync(specParams);
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategoryItem>, IReadOnlyList<ItemFlashCardToReturnDto>>(items);
            return Ok(new Pagination<ItemFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }

        // {{BaseUrl}}/api/products/sets?SetId=1
        [ProducesResponseType(typeof(ProductSetToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("set")]
        public async Task<ActionResult<ProductSetToReturnDto>> GetSet(int setId)
        {
            var set = await _productService.GetSetById(setId);

            if (set is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductSet = _mapper.Map<CategorySet, ProductSetToReturnDto>(set);

            return Ok(mappedProductSet);
        }


        // {{BaseUrl}}/api/products/item?ItemId=1
        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("item")]
        public async Task<ActionResult<ProductItemToReturnDto>> GetItem(int itemId)
        {
            var item = await _productService.GetItemById(itemId);

            if (item is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductItem = _mapper.Map<CategoryItem, ProductItemToReturnDto>(item);

            return Ok(mappedProductItem);
        }


    }
}
