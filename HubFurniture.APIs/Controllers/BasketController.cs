﻿using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasketController(IBasketRepository basketRepository,
            IProductService productService,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _basketRepository = basketRepository;
            _productService = productService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet] // {{BaseUrl}}/api/basket
        public async Task<ActionResult<CustomerBasket>> GetBasket()
        {
            string currentCulture = CultureInfo.CurrentCulture.Name;
            bool checkCulture = currentCulture.StartsWith("ar");

            var currentUser = await _userManager.GetUserAsync(User);

            CustomerBasket? basket = null;

            if (currentUser is not null && currentUser.BasketId is not null)
            {
                basket = await _basketRepository.GetBasketAsync(currentUser.BasketId);
                if (basket is null)
                {
                    basket = await _basketRepository.UpdateBasketAsync(new CustomerBasket(){BasketId = currentUser.BasketId});
                    basket.BasketItems = new List<BasketItem>();
                }
                else if (basket.BasketItems is not null && basket.BasketItems.Count > 0)
                {
                    foreach (var item in basket.BasketItems)
                    {
                        if (item.Type == "item")
                        {
                            var product = await _productService.GetItemById(item.ProductId);
                            item.ProductName = checkCulture ? product.NameArabic : product.NameEnglish;
                        }
                        else if (item.Type == "set")
                        {
                            var product = await _productService.GetSetById(item.ProductId);
                            item.ProductName = checkCulture ? product.NameArabic : product.NameEnglish;
                        }
                    }
                }

            }

            return Ok(basket?? new CustomerBasket());

        }

        [HttpPost("userBasket")] // {{BaseUrl}}/api/basket/userBasket?basketId=123asd
        public async Task UpdateCustomerBasketId([FromQuery] string basketId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is not null)
            {
                currentUser.BasketId = basketId;
            }
            await _userManager.UpdateAsync(currentUser);
        }

        [HttpPost] // {{BaseUrl}}/api/basket
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {

            var currentUser = await _userManager.GetUserAsync(User);

            currentUser.BasketId ??= basket?.BasketId;

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
