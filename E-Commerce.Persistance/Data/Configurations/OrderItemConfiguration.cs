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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Price)
                .HasPrecision(8, 2);


            builder.OwnsOne(X => X.Product, pb =>
            {
                pb.Property(p => p.ProductName).HasMaxLength(100);
                pb.Property(p => p.PictureUrl)
                    .HasMaxLength(200);
            });
        }
    }

}
