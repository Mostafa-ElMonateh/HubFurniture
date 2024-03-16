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
    internal class ProductItemConfigurations: IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.Property(pi => pi.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pi => pi.Availability)
                .IsRequired();

            builder.Property(pi => pi.Suitability)
                .IsRequired();

            builder.Property(pi => pi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Color)
                .IsRequired();

            builder.Property(pi => pi.Style)
                .IsRequired();

            builder.Property(pi => pi.Room)
                .IsRequired();

            builder.Property(pi => pi.Height)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Depth)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Width)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(pi => pi.ProductPictures)
                .WithOne();

            builder.HasMany(pi => pi.CustomerReviews)
                .WithOne();
        }
    }
}
