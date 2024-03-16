using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Repository.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySet> CategorySets { get; set; } 
        public DbSet<ProductCollection> ProductCollections { get; set; } 
        public DbSet<ProductItem> ProductItems { get; set; } 
        public DbSet<CustomerReview> CustomerReviews { get; set; } 
        public DbSet<ProductPicture> ProductPictures { get; set; }
    }
}
