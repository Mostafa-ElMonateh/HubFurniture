using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HubFurniture.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository,
            IProductService productService,
            IMapper mapper)
        {
            _basketRepository = basketRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet] // {{BaseUrl}}/api/basket?id=
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);

            if (basket == null)
            {
                basket = new CustomerBasket(id);
            }
            else
            {
                string currentCulture = CultureInfo.CurrentCulture.Name;

                foreach (var item in basket.BasketItems)
                {
                    if (item.Type == "item")
                    {
                        var product = await _productService.GetItemById(item.ProductId);
                        item.ProductName = currentCulture.StartsWith("ar") ? product.NameArabic : product.NameEnglish;
                    }
                    else if (item.Type == "set")
                    {
                        var product = await _productService.GetSetById(item.ProductId);
                        item.ProductName = currentCulture.StartsWith("ar") ? product.NameArabic : product.NameEnglish;
                    }
                }


            }

            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost] // {{BaseUrl}}/api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(createdOrUpdatedBasket);
        }

        [HttpDelete] // {{BaseUrl}}/api/basket?id=
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }

    }
}
