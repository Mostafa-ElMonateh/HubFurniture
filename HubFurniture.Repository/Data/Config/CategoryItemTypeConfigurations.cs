using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubFurniture.Repository.Data.Config
{
    internal class CategoryItemTypeConfigurations : IEntityTypeConfiguration<CategoryItemType>
    {
        public void Configure(EntityTypeBuilder<CategoryItemType> builder)
        {

            builder.Property(cit => cit.NameArabic)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cit => cit.NameEnglish)
               .IsRequired()
               .HasMaxLength(50);

            builder.HasMany(cit => cit.CategoryItems)
                .WithOne(ci => ci.CategoryItemType)
                .HasForeignKey(ci => ci.CategoryItemTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
