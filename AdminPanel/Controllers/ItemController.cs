using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ItemController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            // GET all Items

            var itemSpecifications = new ItemWithItsPicturesItsReviewsSpecifications();

            var items = await _unitOfWork.Repository<CategoryItem>().GetAllWithSpecAsync(itemSpecifications);

            var mappedItems = _mapper.Map<IReadOnlyList<CategoryItem>, IReadOnlyList<ItemViewModel>>(items);

            return View(mappedItems);
        }

        public IActionResult Create()
        {
            var availabilities = Enum.GetValues(typeof(Availability))
                .Cast<Availability>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

            var suitabilities = Enum.GetValues(typeof(Suitability))
                .Cast<Suitability>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

    
            ViewBag.Availabilities = availabilities;
            ViewBag.Suitabilities = suitabilities;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel itemViewModel)
        {
            if (ModelState.IsValid)
            {
                if (itemViewModel.Image != null)
                {
                    string pictureUrl = PictureSettings.UploadFile(itemViewModel.Image, "categoryProducts");
                    itemViewModel.ProductPictures.Add(new ProductPicture()
                    {
                        PictureUrl = pictureUrl
                    });
                }
                else
                {
                    itemViewModel.ProductPictures[0].PictureUrl = "images/categoryProducts/noImage.png";
                }

                var mappedItem = _mapper.Map<ItemViewModel, CategoryItem>(itemViewModel);

                await _unitOfWork.Repository<CategoryItem>().AddAsync(mappedItem);

                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }

            var availabilities = Enum.GetValues(typeof(Availability))
                .Cast<Availability>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

            var suitabilities = Enum.GetValues(typeof(Suitability))
                .Cast<Suitability>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();

    
            ViewBag.Availabilities = availabilities;
            ViewBag.Suitabilities = suitabilities;

            return View(itemViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTypesByCategoryId(int categoryId)
        {
            var typesSpecifications = new ItemsTypesOfCategorySpecifications(categoryId);
            var types = await _unitOfWork.Repository<CategoryItemType>().GetAllWithSpecAsync(typesSpecifications);
            var mappedTypes =
                _mapper.Map<IReadOnlyList<CategoryItemType>, IReadOnlyList<ItemsTypesInCategoryViewModel>>(types);
            return Json(mappedTypes);
        }
    }
}
