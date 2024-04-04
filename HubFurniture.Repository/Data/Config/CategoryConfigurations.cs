using HubFurniture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Repository.Data.Config
{
    internal class CategoryConfigurations: IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.NameEnglish)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(c => c.CategorySetsTypes)
                .WithOne()
                .HasForeignKey(cst => cst.CategoryId);

            builder.HasMany(c => c.CategoryItemsTypes)
                .WithOne(ci => ci.Category)
                .HasForeignKey(cit => cit.CategoryId);

            builder.HasMany(c => c.CategorySets)
                .WithOne(cs => cs.Category)
                .HasForeignKey(cs => cs.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.CategoryItems)
                .WithOne(ci => ci.Category)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
