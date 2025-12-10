using E_Commerce.Domain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistance.Data.Configurations
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(X => X.Subtotal)
                .HasPrecision(8, 2);

            builder.OwnsOne(o => o.Address, a =>
            {
                a.Property(a => a.FirstName).HasMaxLength(50);
                a.Property(a => a.LastName).HasMaxLength(50);
                a.Property(a => a.Street).HasMaxLength(50);
                a.Property(a => a.City).HasMaxLength(50);
                a.Property(a => a.Country).HasMaxLength(50);
            });



        }
    }
}
