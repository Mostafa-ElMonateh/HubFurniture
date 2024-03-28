using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public BasketController(IBasketRepository basketRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet] // {{BaseUrl}}/api/basket
        public async Task<ActionResult<CustomerBasket>> GetBasket()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            CustomerBasket? basket = new();

            if (currentUser is not null && currentUser.BasketId is not null)
            {
                basket = await _basketRepository.GetBasketAsync(currentUser.BasketId);
            }

            return Ok(basket ?? new CustomerBasket(currentUser.BasketId));
        }

        [HttpPost("userBasket")] // {{BaseUrl}}/api/basket/userBasket?basketId=123asd
        public async Task UpdateCustomerBasketId([FromQuery]string basketId)
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
