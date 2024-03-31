using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.APIs.Helpers;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HubFurniture.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        string currentCulture;

        public ProductsController(IProductService productService,
            IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
            currentCulture = CultureInfo.CurrentCulture.Name;
        }

        // {{BaseUrl}}/api/products/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategoryToReturnDto>>> GetCategories()
        {
            var categories = await _productService.GetCategoriesAsync();

            // localization
            var mappedProductsCategory = categories.Select(category =>
            {
                var dto = _mapper.Map<ProductCategoryToReturnDto>(category);


                dto.Name = currentCulture.StartsWith("ar") ? category.NameArabic : category.NameEnglish;

                return dto;
            }).ToList();

            return Ok(mappedProductsCategory);
        }


        // {{BaseUrl}}/api/Products/sets/types?CategoryId=1
        [HttpGet("sets/types")]
        public async Task<ActionResult<CategorySetsToReturnDto>> GetCategorySetsTypes([FromQuery]ProductSpecParams specParams)
        {
            var category = await _productService.GetCategoryByIdAsync(specParams);
            var mappedProductsCategory = _mapper.Map<Category, CategorySetsToReturnDto>(category);

            //Localize the category name based on the current culture
            mappedProductsCategory.Name = currentCulture.StartsWith("ar") ? category.NameArabic : category.NameEnglish;

            return Ok(mappedProductsCategory);
        }

        // {{BaseUrl}}/api/Products/items/types?CategoryId=1
        [HttpGet("items/types")]
        public async Task<ActionResult<CategoryItemsToReturn>> GetCategoryItemsTypes([FromQuery]ProductSpecParams specParams)
        {
            // _dbContext.CategoryItems.Where(c => c.Name == Name).CountAsync();

            var category = await _productService.GetCategoryByIdAsync(specParams);
            var mappedProductsCategory = _mapper.Map<Category, CategoryItemsToReturn>(category);

            // Localize the category name based on the current culture
            mappedProductsCategory.Name = currentCulture.StartsWith("ar") ? category.NameArabic : category.NameEnglish;

            return Ok(mappedProductsCategory);
        }


        [HttpGet("sets")]
        public async Task<ActionResult<Pagination<SetFlashCardToReturnDto>>> GetSets([FromQuery]ProductSpecParams specParams)
        {
            bool checkCulture = currentCulture.StartsWith("ar");

            IReadOnlyList<CategorySet> sets = await _productService.GetSetsAsync(specParams);
            decimal minimumPrice = _productService.GetMinimumPriceOfSets(sets);
            decimal maximumPrice = _productService.GetMaximumPriceOfSets(sets);
            int count = await _productService.GetCountOfSetsAsync(specParams);
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategorySet>, IReadOnlyList<SetFlashCardToReturnDto>>(sets);
            for(int i =0; i <sets.Count(); i++)
            {
                mappedSetsProducts[i].Name = checkCulture ? sets[i].NameArabic : sets[i].NameEnglish;
            }
            return Ok(new Pagination<SetFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }


        [HttpGet("items")]
        public async Task<ActionResult<Pagination<ItemFlashCardToReturnDto>>> GetItems([FromQuery]ProductSpecParams specParams)
        {
            bool checkCulture = currentCulture.StartsWith("ar");
            IReadOnlyList<CategoryItem> items = await _productService.GetItemsAsync(specParams);
            decimal minimumPrice = _productService.GetMinimumPriceOfItems(items);
            decimal maximumPrice = _productService.GetMaximumPriceOfItems(items);
            int count = await _productService.GetCountOfItemsAsync(specParams);
            var mappedSetsProducts = _mapper.Map<IReadOnlyList<CategoryItem>, IReadOnlyList<ItemFlashCardToReturnDto>>(items);
            for (int i = 0; i < items.Count(); i++)
            {
                mappedSetsProducts[i].Name = checkCulture ? items[i].NameArabic : items[i].NameEnglish;
            }
            return Ok(new Pagination<ItemFlashCardToReturnDto>(specParams.PageIndex, specParams.PageSize, count, minimumPrice,maximumPrice, mappedSetsProducts));
        }

        // {{BaseUrl}}/api/products/sets?SetId=1
        [ProducesResponseType(typeof(ProductSetToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("set")]
        public async Task<ActionResult<ProductSetToReturnDto>> GetSet(int setId)
        {
            bool checkCulture = currentCulture.StartsWith("ar");
            var set = await _productService.GetSetById(setId);

            if (set is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductSet = _mapper.Map<CategorySet, ProductSetToReturnDto>(set);

            mappedProductSet.Name = checkCulture ? set.NameArabic : set.NameEnglish;
            mappedProductSet.Style = checkCulture ? set.StyleArabic : set.StyleEnglish;
            mappedProductSet.Room = checkCulture ? set.RoomArabic : set.RoomEnglish;
            List <SetItemToReturnDto> items = new List<SetItemToReturnDto>();
            for (int i =0; i < mappedProductSet.Items.Count(); i++)
            {
                mappedProductSet.Items[i].Name = checkCulture ? set.Items[i].NameArabic : set.Items[i].NameEnglish;
            }

            return Ok(mappedProductSet);
        }


        // {{BaseUrl}}/api/products/item?ItemId=1
        [ProducesResponseType(typeof(ProductItemToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("item")]
        public async Task<ActionResult<ProductItemToReturnDto>> GetItem(int itemId)
        {
            bool checkCulture = currentCulture.StartsWith("ar");

            var item = await _productService.GetItemById(itemId);

            if (item is null)
            {
                return NotFound(new ApiResponse(404));
            }

            var mappedProductItem = _mapper.Map<CategoryItem, ProductItemToReturnDto>(item);

            mappedProductItem.Name = checkCulture ? item.NameArabic : item.NameEnglish;
            mappedProductItem.Style = checkCulture ? item.StyleArabic : item.StyleEnglish;
            mappedProductItem.Room = checkCulture ? item.RoomArabic : item.RoomEnglish;

            return Ok(mappedProductItem);
        }


    }
}
