using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Specifications.ProductSpecifications;

namespace HubFurniture.Core.Contracts.Contracts.Services
{
    public interface IProductService
    {
        Task<Category?> GetCategoryByIdAsync(ProductSpecParams specParams);
        Task<IReadOnlyList<Category>> GetCategoriesAsync();
        Task<IReadOnlyList<CategorySet>> GetSetsAsync(ProductSpecParams specParams);
        Task<IReadOnlyList<CategoryItem>> GetItemsAsync(ProductSpecParams specParams);
        Task<CategorySet?> GetSetById(int setId);
        Task<CategoryItem?> GetItemById(int itemId);
        Task<int> GetCountOfSetsAsync(ProductSpecParams specParams);
        Task<int> GetCountOfItemsAsync(ProductSpecParams specParams);
        decimal GetMinimumPriceOfSets(IReadOnlyList<CategorySet> sets);
        decimal GetMinimumPriceOfItems(IReadOnlyList<CategoryItem> items);
        decimal GetMaximumPriceOfSets(IReadOnlyList<CategorySet> sets);
        decimal GetMaximumPriceOfItems(IReadOnlyList<CategoryItem> items);
    }
}
