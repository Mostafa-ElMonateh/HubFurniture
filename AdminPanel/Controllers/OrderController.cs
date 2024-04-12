using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using HubFurniture.Core.Enums;
using HubFurniture.Core.Specifications.OrderSpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var orderSpecifications = new OrderSpecifications();
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(orderSpecifications);
            var mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnViewModel>>(orders);
            return View(mappedOrders);
        }

        public async Task<IActionResult> Details(int id, string view = "Details")
        {
            var orderStatuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

    
            ViewBag.OrderStatuses = orderStatuses;

            var orderSpecifications = new OrderSpecifications(id);
            var order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(orderSpecifications);
            var mappedOrder = _mapper.Map<Order, OrderViewModel>(order);
            return View(view, mappedOrder);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new { id, view = "Edit" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OrderViewModel orderViewModel)
        {

            var orderStatuses = Enum.GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

    
            ViewBag.OrderStatuses = orderStatuses;

            if (id != orderViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedOrder = _mapper.Map<OrderViewModel, Order>(orderViewModel);

                _unitOfWork.Repository<Order>().Update(mappedOrder);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(orderViewModel);
        }
    }
}
