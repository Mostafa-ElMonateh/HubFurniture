using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ItemTypeSpecifications;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.Repository<Category>().GetAllAsync();
            var mappedCategories = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryViewModel>>(categories);
            return View(mappedCategories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var mappedCategory = _mapper.Map<CategoryViewModel, Category>(categoryViewModel);

                await _unitOfWork.Repository<Category>().AddAsync(mappedCategory);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(categoryViewModel);
        }

        public async Task<IActionResult> Details(int id, string view="Details")
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            var mappedCategory = _mapper.Map<Category, CategoryViewModel>(category);
            return View(view, mappedCategory);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new{id, view="Edit"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedCategory = _mapper.Map<CategoryViewModel, Category>(categoryViewModel);

                _unitOfWork.Repository<Category>().Update(mappedCategory);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(categoryViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            var mappedCategory = _mapper.Map<Category, CategoryViewModel>(category);
            return View(mappedCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);

                _unitOfWork.Repository<Category>().Delete(category);

                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(categoryViewModel);
            }
        }
    }
}
