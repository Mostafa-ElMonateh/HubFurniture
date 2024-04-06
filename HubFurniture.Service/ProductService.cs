using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductCategorySpecifications;
using HubFurniture.Core.Specifications.ProductSpecifications;

namespace HubFurniture.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Category?> GetCategoryByIdAsync(ProductSpecParams specParams)
        {
            var specifications = new ProductCategorySpecifications(specParams.CategoryId);
            var category = await _unitOfWork.Repository<Category>().GetEntityWithSpecAsync(specifications);
            return category;
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            var specifications = new ProductCategorySpecifications();
            var categories = await _unitOfWork.Repository<Category>().GetAllWithSpecAsync(specifications);
            return categories;
        }

        public async Task<IReadOnlyList<CategorySet>> GetSetsAsync(ProductSpecParams specParams)
        {
            var specifications = new SetWithItsPicturesItsReviewsSpecifications(specParams);
            var sets = await _unitOfWork.Repository<CategorySet>().GetAllWithSpecAsync(specifications);
            return sets;
        }

        public async Task<IReadOnlyList<CategoryItem>> GetItemsAsync(ProductSpecParams specParams)
        {
            var specifications = new ItemWithItsPicturesItsReviewsSpecifications(specParams);
            var items = await _unitOfWork.Repository<CategoryItem>().GetAllWithSpecAsync(specifications);
            return items;
        }

        public async Task<int> GetCountOfSetsAsync(ProductSpecParams specParams)
        {
            var countSpecifications = new SetsWithFilterationForCountSpecifications(specParams);
            int count = await _unitOfWork.Repository<CategorySet>().GetCountAsync(countSpecifications);
            return count;
        }

        public async Task<int> GetCountOfItemsAsync(ProductSpecParams specParams)
        {
            var countSpecifications = new ItemsWithFilterationForCountSpecifications(specParams);
            int count = await _unitOfWork.Repository<CategoryItem>().GetCountAsync(countSpecifications);
            return count;
        }

        public async Task<CategorySet?> GetSetById(int setId)
        {
            var specifications = new SetWithItsPicturesItsReviewsSpecifications(setId);
            var set = await _unitOfWork.Repository<CategorySet>().GetEntityWithSpecAsync(specifications);
            return set;
        }

        public async Task<CategoryItem?> GetItemById(int itemId)
        {
            var specifications = new ItemWithItsPicturesItsReviewsSpecifications(itemId);
            var item = await _unitOfWork.Repository<CategoryItem>().GetEntityWithSpecAsync(specifications);
            return item;
        }

        public decimal GetMinimumPriceOfSets(IReadOnlyList<CategorySet> sets)
        {
            decimal minimumPrice = 0;
            if (sets.Any())
            {
                minimumPrice = sets.Min(cs => cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))));
            }

            return minimumPrice;
        }
        public decimal GetMinimumPriceOfItems(IReadOnlyList<CategoryItem> items)
        {
            decimal minimumPrice = 0;
            if (items.Any())
            {
                minimumPrice = items.Min(ci => ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100))));
            }

            return minimumPrice;
        }
        public decimal GetMaximumPriceOfSets(IReadOnlyList<CategorySet> sets)
        {
            decimal maximumPrice = 0;
            if (sets.Any())
            {
                maximumPrice = sets.Max(cs => cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))));
            }

            return maximumPrice;
        }
        public decimal GetMaximumPriceOfItems(IReadOnlyList<CategoryItem> items)
        {
            decimal maximumPrice = 0;
            if (items.Any())
            {
                maximumPrice = items.Max(ci => ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100))));
            }

            return maximumPrice;
        }
    }
}
