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
    internal class CustomerReviewConfigurations : IEntityTypeConfiguration<CustomerReview>
    {
        public void Configure(EntityTypeBuilder<CustomerReview> builder)
        {
            

            builder.Property(cr => cr.CustomerName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
