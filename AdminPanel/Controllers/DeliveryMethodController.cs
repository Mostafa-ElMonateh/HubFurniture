using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class DeliveryMethodController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeliveryMethodController(IUnitOfWork unitOfWork,
             IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            var mappedDeliveryMethods = _mapper.Map<IReadOnlyList<DeliveryMethod>, IReadOnlyList<DeliveryMethodViewModel>>(deliveryMethods);
            return View(mappedDeliveryMethods);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeliveryMethodViewModel deliveryMethodViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedDeliveryMethods = _mapper.Map<DeliveryMethodViewModel, DeliveryMethod>(deliveryMethodViewModel);

                await _unitOfWork.Repository<DeliveryMethod>().AddAsync(mappedDeliveryMethods);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(deliveryMethodViewModel);
        }

        public async Task<IActionResult> Details(int id, string view="Details")
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(id);
            var mappedDeliveryMethod = _mapper.Map<DeliveryMethod, DeliveryMethodViewModel>(deliveryMethod);
            return View(view, mappedDeliveryMethod);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new{id, view="Edit"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DeliveryMethodViewModel deliveryMethodViewModel)
        {
            if (id != deliveryMethodViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedDeliveryMethodViewModel = _mapper.Map<DeliveryMethodViewModel, DeliveryMethod>(deliveryMethodViewModel);

                _unitOfWork.Repository<DeliveryMethod>().Update(mappedDeliveryMethodViewModel);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(deliveryMethodViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(id);
            var mappedDeliveryMethod = _mapper.Map<DeliveryMethod, DeliveryMethodViewModel>(deliveryMethod);
            return View(mappedDeliveryMethod);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, DeliveryMethodViewModel deliveryMethodViewModel)
        {
            if (id != deliveryMethodViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(id);

                _unitOfWork.Repository<DeliveryMethod>().Delete(deliveryMethod);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(deliveryMethodViewModel);
            }
        }
    }
}
