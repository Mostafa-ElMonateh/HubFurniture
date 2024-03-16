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
    internal class ProductCollectionConfigurations: IEntityTypeConfiguration<ProductCollection>
    {
        public void Configure(EntityTypeBuilder<ProductCollection> builder)
        {
            builder.Property(pc => pc.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pc => pc.Availability)
                .IsRequired();

            builder.Property(pc => pc.Suitability)
                .IsRequired();

            builder.Property(pc => pc.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pc => pc.Color)
                .IsRequired();

            builder.Property(pc => pc.Style)
                .IsRequired();

            builder.Property(pc => pc.Room)
                .IsRequired();

            builder.Property(pc => pc.Height)
                .HasColumnType("decimal(18,2)");

            builder.Property(pc => pc.Depth)
                .HasColumnType("decimal(18,2)");

            builder.Property(pc => pc.Width)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(pc => pc.ProductPictures)
                .WithOne();

            builder.HasMany(pc => pc.CustomerReviews)
                .WithOne();
        }
    }
}
