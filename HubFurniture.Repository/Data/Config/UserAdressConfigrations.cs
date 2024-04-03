using HubFurniture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Repository.Data.Config
{
    internal class UserAdressConfigrations : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(a => a.StreetAdress)
                 .IsRequired();

            builder.Property(a => a.City)
                 .IsRequired();

            builder.Property(a => a.Country)
                 .IsRequired();

            builder.Property(a => a.PhoneNumber)
                 .IsRequired();

            builder.HasOne(a => a.User)
                    .WithMany(u => u.Addresses)
                    .HasForeignKey(a => a.UserId)
                    .IsRequired();
        }
    }
}
