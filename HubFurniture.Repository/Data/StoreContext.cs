using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;
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
        public DbSet<CategoryItemType> CategoryItemsTypes { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<CustomerReview> CustomerReviews { get; set; } 
        public DbSet<ProductPicture> ProductPictures { get; set; }
    }
}
