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
    internal class SetItemConfigurations : IEntityTypeConfiguration<SetItem>
    {
        public void Configure(EntityTypeBuilder<SetItem> builder)
        {
            builder.Property(pi => pi.NameArabic)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pi => pi.NameEnglish)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(pi => pi.Height)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Depth)
                .HasColumnType("decimal(18,2)");

            builder.Property(pi => pi.Width)
                .HasColumnType("decimal(18,2)");
        }
    }
}
