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
    internal class CategorySetTypeConfigurations : IEntityTypeConfiguration<CategorySetType>
    {
        public void Configure(EntityTypeBuilder<CategorySetType> builder)
        {
           

            builder.Property(cst => cst.NameArabic)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cst => cst.NameEnglish)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(cst => cst.CategorySets)
                .WithOne()
                .HasForeignKey(cs => cs.CategorySetTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
