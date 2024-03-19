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

            

            builder.Property(cs => cs.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cs => cs.Availability)
                .IsRequired();

            builder.Property(cs => cs.Suitability)
                .IsRequired();

            builder.Property(cs => cs.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(cs => cs.Color)
                .IsRequired();

            builder.Property(cs => cs.Style)
                .IsRequired();

            builder.Property(cs => cs.Room)
                .IsRequired();

            builder.HasMany(cs => cs.ProductPictures)
                .WithOne();

            builder.HasMany(cs => cs.CustomerReviews)
                .WithOne();
        }
    }
}
