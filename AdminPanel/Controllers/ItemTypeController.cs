using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using HubFurniture.Core.Specifications.ItemTypeSpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class ItemTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemTypeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var itemTypeSpecifications = new ItemTypeSpecifications();
            var itemTypes = await _unitOfWork.Repository<CategoryItemType>().GetAllWithSpecAsync(itemTypeSpecifications);
            var mappedItemTypes = _mapper.Map<IReadOnlyList<CategoryItemType>, IReadOnlyList<ItemTypeViewModel>>(itemTypes);
            return View(mappedItemTypes);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemTypeViewModel itemTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedItemType = _mapper.Map<ItemTypeViewModel, CategoryItemType>(itemTypeViewModel);

                await _unitOfWork.Repository<CategoryItemType>().AddAsync(mappedItemType);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(itemTypeViewModel);
        }

        public async Task<IActionResult> Details(int id, string view="Details")
        {
            var itemTypeSpecifications = new ItemTypeSpecifications(id);
            var itemType = await _unitOfWork.Repository<CategoryItemType>().GetEntityWithSpecAsync(itemTypeSpecifications);
            var mappedItem = _mapper.Map<CategoryItemType, ItemTypeViewModel>(itemType);
            return View(view, mappedItem);
        }
        
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new{id, view="Edit"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ItemTypeViewModel itemTypeViewModel)
        {
            if (id != itemTypeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedItemType = _mapper.Map<ItemTypeViewModel, CategoryItemType>(itemTypeViewModel);

                _unitOfWork.Repository<CategoryItemType>().Update(mappedItemType);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(itemTypeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var itemTypeSpecifications = new ItemTypeSpecifications(id);
            var itemType = await _unitOfWork.Repository<CategoryItemType>().GetEntityWithSpecAsync(itemTypeSpecifications);
            var mappedItem = _mapper.Map<CategoryItemType, ItemTypeViewModel>(itemType);
            return View(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ItemTypeViewModel itemTypeViewModel)
        {
            if (id != itemTypeViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var itemTypeSpecifications = new ItemTypeSpecifications(id);
                var itemType = await _unitOfWork.Repository<CategoryItemType>().GetEntityWithSpecAsync(itemTypeSpecifications);

                _unitOfWork.Repository<CategoryItemType>().Delete(itemType);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(itemTypeViewModel);
            }
        }

    }
}
