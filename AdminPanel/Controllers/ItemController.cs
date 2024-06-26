﻿using AdminPanel.Helpers;
using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using HubFurniture.Core.Specifications.ItemTypeSpecifications;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdminPanel.Controllers
{
    [Authorize(Roles = "admin")]
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
                    var picture = new ProductPicture(){ PictureUrl = "images/categoryProducts/noImage.png"};
                    itemViewModel.ProductPictures.Add(picture);
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

        public async Task<IActionResult> Details(int id, string view="Details")
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

            var itemSpecifications = new ItemWithItsPicturesItsReviewsSpecifications(id);
            var item = await _unitOfWork.Repository<CategoryItem>().GetEntityWithSpecAsync(itemSpecifications);
            var mappedItem = _mapper.Map<CategoryItem, ItemViewModel>(item);

            return View(view, mappedItem);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return RedirectToAction(nameof(Details), new{id, view="Edit"});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ItemViewModel itemViewModel)
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

            if (id != itemViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (itemViewModel.Image != null)
                {
                    PictureSettings.DeleteFile("categoryProducts", itemViewModel.ProductPictures[0].PictureUrl);
                    
                    itemViewModel.ProductPictures[0].PictureUrl =
                        PictureSettings.UploadFile(itemViewModel.Image, "categoryProducts");
                }

                var mappedItem = _mapper.Map<ItemViewModel, CategoryItem>(itemViewModel);

                _unitOfWork.Repository<CategoryItem>().Update(mappedItem);

                var result = await _unitOfWork.CompleteAsync();

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(itemViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var itemSpecifications = new ItemWithItsPicturesItsReviewsSpecifications(id);
            var item = await _unitOfWork.Repository<CategoryItem>().GetEntityWithSpecAsync(itemSpecifications);
            var mappedItem = _mapper.Map<CategoryItem, ItemViewModel>(item);
            return View(mappedItem);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ItemViewModel itemViewModel)
        {
            if (id != itemViewModel.Id)
            {
                return NotFound();
            }

            try
            {
                var itemSpecifications = new ItemWithItsPicturesItsReviewsSpecifications(id);
                var item = await _unitOfWork.Repository<CategoryItem>().GetEntityWithSpecAsync(itemSpecifications);
                if (item.ProductPictures.Any())
                {
                    foreach (var picture in item.ProductPictures)
                    {
                        PictureSettings.DeleteFile("categoryProducts", picture.PictureUrl);
                    }

                    _unitOfWork.Repository<CategoryItem>().Delete(item);

                    await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                return View(itemViewModel);
            }

            return View(itemViewModel);
        }
    }
}
