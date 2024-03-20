using HubFurniture.Core.Entities;
using HubFurniture.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.Repository.DataSeed
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.Categories.Any()
                && !dbContext.CategorySetsTypes.Any()
                && !dbContext.CategoryItemsTypes.Any()
                && !dbContext.CategorySets.Any()
                && !dbContext.CategoryItems.Any()
                && !dbContext.CustomerReviews.Any()
                && !dbContext.ProductPictures.Any()
                && !dbContext.DeliveryMethods.Any())
            {
                // Adding Categories.
                var categoriesData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    foreach (var category in categories)
                        dbContext.Categories.Add(category);
                    await dbContext.SaveChangesAsync();
                }

                // Adding CategoriesSetsTypes.
                var categoriesSetsTypesData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categorySetsTypes.json");
                var categoriesSetsTypes = JsonSerializer.Deserialize<List<CategorySetType>>(categoriesSetsTypesData);

                if (categoriesSetsTypes?.Count() > 0)
                {
                    foreach (var categorySetsType in categoriesSetsTypes)
                        dbContext.CategorySetsTypes.Add(categorySetsType);
                    await dbContext.SaveChangesAsync();
                }

                // Adding CategoriesItemsTypes.
                var categoriesItemsTypesData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categoryItemsTypes.json");
                var categoriesItemsTypes = JsonSerializer.Deserialize<List<CategoryItemType>>(categoriesItemsTypesData);

                if (categoriesItemsTypes?.Count() > 0)
                {
                    foreach (var categoryItemType in categoriesItemsTypes)
                        dbContext.CategoryItemsTypes.Add(categoryItemType);
                    await dbContext.SaveChangesAsync();
                }

                // Adding CategoryItems.
                var categoryItemsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categoryItems.json");
                var categoryItems = JsonSerializer.Deserialize<List<CategoryItem>>(categoryItemsData);

                if (categoryItems?.Count() > 0)
                {
                    foreach (var categoryItem in categoryItems)
                        dbContext.CategoryItems.Add(categoryItem);
                    await dbContext.SaveChangesAsync();
                }

                // Adding CategorySets.
                var categorySetsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categorySets.json");
                var categorySets = JsonSerializer.Deserialize<List<CategorySet>>(categorySetsData);

                if (categorySets?.Count() > 0)
                {
                    foreach (var categorySet in categorySets)
                        dbContext.CategorySets.Add(categorySet);
                    await dbContext.SaveChangesAsync();
                }

                // Adding CustomerReviews.
                var customerReviewsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/customerReviews.json");
                var customerReviews = JsonSerializer.Deserialize<List<CustomerReview>>(customerReviewsData);

                if (customerReviews?.Count() > 0)
                {
                    foreach (var customerReview in customerReviews)
                        dbContext.CustomerReviews.Add(customerReview);
                    await dbContext.SaveChangesAsync();
                }

                // Adding picturesUrls.
                var picturesUrlsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/productPictures.json");
                var picturesUrls = JsonSerializer.Deserialize<List<ProductPicture>>(picturesUrlsData);

                if (picturesUrls?.Count() > 0)
                {
                    foreach (var picturesUrl in picturesUrls)
                        dbContext.ProductPictures.Add(picturesUrl);
                    await dbContext.SaveChangesAsync();
                }

                // Adding picturesUrls.
                var deliveryMethodsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count() > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                        dbContext.DeliveryMethods.Add(deliveryMethod);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
