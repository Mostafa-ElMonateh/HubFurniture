using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;
using HubFurniture.Core.Specifications.SetItemSpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class SetItemController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetItemController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int id)
        {

            var setItemSpecifications = new SetItemOfSetSpecifiction(id);

            var sets = await _unitOfWork.Repository<SetItem>().GetAllWithSpecAsync(setItemSpecifications);

            var mappedsets = _mapper.Map<IReadOnlyList<SetItem>, IReadOnlyList<SetItemViewModel>>(sets);
            ViewBag.CategorySetId = id;

            return View(mappedsets);
        }

        public IActionResult Create(int id)
        {
            var setItemViewModel = new SetItemViewModel
            {
                CategorySetId = id 
            };

            return View(setItemViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SetItemViewModel setItemViewModel)
        {
            if (ModelState.IsValid)
            {
                setItemViewModel.Id = 0;
                var mappedItem = _mapper.Map<SetItemViewModel, SetItem>(setItemViewModel);

                await _unitOfWork.Repository<SetItem>().AddAsync(mappedItem);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index), new { id = setItemViewModel.CategorySetId });
            }
            return View(setItemViewModel);
        }


        public async Task<IActionResult> Details(int id, string view = "Details")
        {
            var item = await _unitOfWork.Repository<SetItem>().GetByIdAsync(id);
            var mappedSet = _mapper.Map<SetItem, SetItemViewModel>(item);

            return View(view, mappedSet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new { id, view = "Edit" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SetItemViewModel setItemViewModel)
        {
            if (id != setItemViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var mappedItem = _mapper.Map<SetItemViewModel, SetItem>(setItemViewModel);

                _unitOfWork.Repository<SetItem>().Update(mappedItem);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index), new { id = setItemViewModel.CategorySetId });
                }
            }
            return View(setItemViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.Repository<SetItem>().GetByIdAsync(id);
            var mappedItem = _mapper.Map<SetItem, SetItemViewModel>(item);
            return View(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, SetItemViewModel setItemViewModel)
        {
            if (id != setItemViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var set = await _unitOfWork.Repository<SetItem>().GetByIdAsync(id);
                if (set != null)
                {
                    _unitOfWork.Repository<SetItem>().Delete(set);

                    await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index), new { id = setItemViewModel.CategorySetId });
                }
            }
            catch (Exception e)
            {
                return View(setItemViewModel);
            }

            return View(setItemViewModel);
        }
    }
}

