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
    public class SetController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SetController(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            // GET all Items

            var setSpecifications = new SetWithItsPicturesItsReviewsSpecifications();

            var sets = await _unitOfWork.Repository<CategorySet>().GetAllWithSpecAsync(setSpecifications);

            var mappedsets = _mapper.Map<IReadOnlyList<CategorySet>, IReadOnlyList<SetViewModel>>(sets);

            return View(mappedsets);
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
        public async Task<IActionResult> Create(SetViewModel setViewModel)
        {
            if (ModelState.IsValid)
            {
                if (setViewModel.Image != null)
                {
                    string pictureUrl = PictureSettings.UploadFile(setViewModel.Image, "categoryProducts");
                    setViewModel.ProductPictures.Add(new ProductPicture()  
                    {
                        PictureUrl = pictureUrl
                    });
                }
                else
                {
                    setViewModel.ProductPictures[0].PictureUrl = "images/categoryProducts/noImage.png";
                }

                var mappedSet = _mapper.Map<SetViewModel, CategorySet>(setViewModel);

                await _unitOfWork.Repository<CategorySet>().AddAsync(mappedSet);

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

            return View(setViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetTypesByCategoryId(int categoryId)
        {
            var setsSpecifications = new SetsTypesOfCategorySpecifications(categoryId);
            var sets = await _unitOfWork.Repository<CategorySetType>().GetAllWithSpecAsync(setsSpecifications);
            var mappedTypes =
                _mapper.Map<IReadOnlyList<CategorySetType>, IReadOnlyList<SetsTypesInCategoryViewModel>>(sets);
            return Json(mappedTypes);
        }

        public async Task<IActionResult> Details(int id, string view = "Details")
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

            var setSpecifications = new SetWithItsPicturesItsReviewsSpecifications(id);
            var set = await _unitOfWork.Repository<CategorySet>().GetEntityWithSpecAsync(setSpecifications);
            var mappedSet = _mapper.Map<CategorySet, SetViewModel>(set);

            return View(view, mappedSet);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new { id, view = "Edit" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SetViewModel setViewModel)
        {
            if (id != setViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (setViewModel.Image != null)
                {
                    PictureSettings.DeleteFile("categoryProducts", setViewModel.ProductPictures[0].PictureUrl);

                    setViewModel.ProductPictures[0].PictureUrl =
                        PictureSettings.UploadFile(setViewModel.Image, "categoryProducts");
                }
                else
                {
                    setViewModel.ProductPictures[0].PictureUrl =
                        PictureSettings.UploadFile(setViewModel.Image, "categoryProducts");
                }

                var mappedSet = _mapper.Map<SetViewModel, CategorySet>(setViewModel);

                _unitOfWork.Repository<CategorySet>().Update(mappedSet);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(setViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var setSpecifications = new SetWithItsPicturesItsReviewsSpecifications(id);
            var set = await _unitOfWork.Repository<CategorySet>().GetEntityWithSpecAsync(setSpecifications);
            var mappedSet = _mapper.Map<CategorySet, SetViewModel>(set);
            return View(mappedSet);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, SetViewModel setViewModel)
        {
            if (id != setViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var setSpecifications = new SetWithItsPicturesItsReviewsSpecifications(id);
                var set = await _unitOfWork.Repository<CategorySet>().GetEntityWithSpecAsync(setSpecifications);
                if (set.ProductPictures.Any())
                {
                    foreach (var picture in set.ProductPictures)
                    {
                        PictureSettings.DeleteFile("categoryProducts", picture.PictureUrl);
                    }

                    _unitOfWork.Repository<CategorySet>().Delete(set);

                    await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                return View(setViewModel);
            }

            return View(setViewModel);
        }
    }
}
