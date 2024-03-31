using Microsoft.EntityFrameworkCore;
using System.Reflection;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HubFurniture.Repository.Data
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySetType> CategorySetsTypes { get; set; } 
        public DbSet<CategorySet> CategorySets { get; set; } 
        public DbSet<SetItem> SetItems { get; set; } 
        public DbSet<CategoryItemType> CategoryItemsTypes { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; } 
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
    }
}
