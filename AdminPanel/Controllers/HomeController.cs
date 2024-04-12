using AdminPanel.Models;
using Microsoft.AspNetCore.Authorization;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace AdminPanel.Controllers
{
    [Authorize(Roles = "admin")]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateDiscount()
        {
            var types = Enum.GetValues(typeof(ProductType))
                .Cast<ProductType>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();
            ViewBag.Types = types;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(DiscountViewModel discountViewModel)
        {
            if (ModelState.IsValid)
            {
                if (discountViewModel != null && !string.IsNullOrEmpty(discountViewModel.Type))
                {
                    if (discountViewModel.Type == "Set")
                    {
                        var setRepository = _unitOfWork.Repository<CategorySet>();
                        var sets = await setRepository.GetAllAsync();
                        foreach (var set in sets)
                        {
                            set.Discount = discountViewModel.Discount;
                            setRepository.Update(set);
                        }
                        await _unitOfWork.CompleteAsync();
                        return RedirectToAction("Index",controllerName:"Set");
                    }else if (discountViewModel.Type == "Item")
                    {
                        var itemRepository = _unitOfWork.Repository<CategoryItem>();
                        var items = await itemRepository.GetAllAsync();
                        foreach (var item in items)
                        {
                            item.Discount = discountViewModel.Discount;
                            itemRepository.Update(item);
                        }
                        await _unitOfWork.CompleteAsync();
                        return RedirectToAction("Index",controllerName:"Item");
                    }
                }
            }

            var types = Enum.GetValues(typeof(ProductType))
                .Cast<ProductType>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();
            ViewBag.Types = types;

            return View(discountViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
