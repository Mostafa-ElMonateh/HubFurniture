using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ItemTypeSpecifications;
using HubFurniture.Core.Specifications.SetTypeSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetTypeController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var setTypeSpecifications = new SetTypeSpecifications();
            var setTypes = await _unitOfWork.Repository<CategorySetType>().GetAllWithSpecAsync(setTypeSpecifications);
            var mappedSetItemTypes = _mapper.Map<IReadOnlyList<CategorySetType>, IReadOnlyList<SetTypeViewModel>>(setTypes);
            return View(mappedSetItemTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SetTypeViewModel setTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedIsetType = _mapper.Map<SetTypeViewModel, CategorySetType>(setTypeViewModel);

                await _unitOfWork.Repository<CategorySetType>().AddAsync(mappedIsetType);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(setTypeViewModel);
        }

        public async Task<IActionResult> Details(int id, string view = "Details")
        {
            var setTypeSpecifications = new SetTypeSpecifications(id);
            var setType = await _unitOfWork.Repository<CategorySetType>().GetEntityWithSpecAsync(setTypeSpecifications);
            var mappedSet = _mapper.Map<CategorySetType, SetTypeViewModel>(setType);
            return View(view, mappedSet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new { id, view = "Edit" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SetTypeViewModel setTypeViewModel)
        {
            if (id != setTypeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedSetType = _mapper.Map<SetTypeViewModel, CategorySetType>(setTypeViewModel);

                _unitOfWork.Repository<CategorySetType>().Update(mappedSetType);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(setTypeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var setTypeSpecifications = new SetTypeSpecifications(id);
            var setType = await _unitOfWork.Repository<CategorySetType>().GetEntityWithSpecAsync(setTypeSpecifications);
            var mappedSet = _mapper.Map<CategorySetType, SetTypeViewModel>(setType);
            return View(mappedSet);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, SetTypeViewModel setTypeViewModel)
        {
            if (id != setTypeViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var setTypeSpecifications = new SetTypeSpecifications(id);
                var setType = await _unitOfWork.Repository<CategorySetType>().GetEntityWithSpecAsync(setTypeSpecifications);

                _unitOfWork.Repository<CategorySetType>().Delete(setType);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(setTypeViewModel);
            }
        }
    }
}