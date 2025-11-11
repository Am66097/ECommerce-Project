using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entities.BasketModule
{
    public class CustomerBasket
    {
        public String Id { get; set; } = default!; // Will Be GUID : Created By Client [FrontEnd]
        public ICollection<BasketItem> Items { get; set; } = [];
    }
}
