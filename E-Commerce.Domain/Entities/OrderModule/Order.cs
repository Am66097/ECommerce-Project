using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.OrderModule
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public OrderAddress Address { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!; 
        public int DeliveryMethodId { get; set; } // Foreign Key
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>(); // Navigation Property =  []
        public decimal Subtotal { get; set; } // Total Price of items before delivery
        public decimal GetTotal() => Subtotal + DeliveryMethod.Price; // Total Price including delivery --> Subtotal + DeliveryMethod.Price

    }
}
