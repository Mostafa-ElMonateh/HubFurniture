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
    internal class CategoryItemConfigurations: IEntityTypeConfiguration<CategoryItem>
    {
        public void Configure(EntityTypeBuilder<CategoryItem> builder)
        {

            builder.HasIndex(ci => ci.NameEnglish).IsUnique();
            builder.HasIndex(ci => ci.NameArabic).IsUnique();

            builder.Property(pi => pi.NameArabic)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pi => pi.NameEnglish)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pi => pi.Availability)
                .IsRequired();

            builder.Property(pi => pi.Suitability)
                .IsRequired();

            builder.Property(pi => pi.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Discount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Color)
                .IsRequired();

            builder.Property(pi => pi.StyleArabic)
                .IsRequired();

            builder.Property(pi => pi.StyleEnglish)
                .IsRequired();

            builder.Property(pi => pi.RoomArabic)
                .IsRequired();

            builder.Property(pi => pi.RoomEnglish)
                .IsRequired();

            builder.Property(pi => pi.Height)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Depth)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Width)
                .HasColumnType("decimal(18,2)");

            builder.HasMany(pi => pi.ProductPictures)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pi => pi.CustomerReviews)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
