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
    internal class CategorySetConfigurations: IEntityTypeConfiguration<CategorySet>
    {
        public void Configure(EntityTypeBuilder<CategorySet> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(cs => cs.ProductCollections)
                .WithOne(pc => pc.CategorySet)
                .HasForeignKey(pc => pc.CategorySetId);
        }
    }
}
