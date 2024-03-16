using HubFurniture.Core.Entities;
using HubFurniture.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HubFurniture.Repository.DataSeed
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.Categories.Any()
                && !dbContext.CategorySets.Any()
                && !dbContext.ProductCollections.Any()
                && !dbContext.ProductItems.Any()
                && !dbContext.CustomerReviews.Any()
                && !dbContext.ProductPictures.Any())
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

                // Adding CategorySets.
                var categorySetsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/categorySets.json");
                var categorySets = JsonSerializer.Deserialize<List<CategorySet>>(categorySetsData);

                if (categorySets?.Count() > 0)
                {
                    foreach (var categorySet in categorySets)
                        dbContext.CategorySets.Add(categorySet);
                    await dbContext.SaveChangesAsync();
                }

                // Adding ProductItems.
                var productItemsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/productItems.json");
                var productItems = JsonSerializer.Deserialize<List<ProductItem>>(productItemsData);

                if (productItems?.Count() > 0)
                {
                    foreach (var productItem in productItems)
                        dbContext.ProductItems.Add(productItem);
                    await dbContext.SaveChangesAsync();
                }

                // Adding ProductCollections.
                var productCollectionsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/productCollections.json");
                var productCollections = JsonSerializer.Deserialize<List<ProductCollection>>(productCollectionsData);

                if (productCollections?.Count() > 0)
                {
                    foreach (var productCollection in productCollections)
                        dbContext.ProductCollections.Add(productCollection);
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
                var picturesUrlsData = File.ReadAllText("../HubFurniture.Repository/DataSeed/picturesUrls.json");
                var picturesUrls = JsonSerializer.Deserialize<List<ProductPicture>>(picturesUrlsData);

                if (picturesUrls?.Count() > 0)
                {
                    foreach (var picturesUrl in picturesUrls)
                        dbContext.ProductPictures.Add(picturesUrl);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
