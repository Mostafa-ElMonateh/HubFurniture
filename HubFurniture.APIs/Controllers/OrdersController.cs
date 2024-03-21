using System.Security.Claims;
using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // {{BaseUrl}}/api/orders
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
        
            if (order is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet] // {{BaseUrl}}/api/orders
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            
            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // {{BaseUrl}}/api/orders/1
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if (order is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
        }

        [HttpGet("deliveryMethods")] // {{BaseUrl}}/api/deliveryMethod
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }


    }
}
