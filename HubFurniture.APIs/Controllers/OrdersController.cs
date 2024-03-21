using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubFurniture.APIs.Controllers
{
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

        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost] // {{BaseUrl}}/api/orders
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, address);
        
            if (order is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return Ok(order);
        }
    }
}
