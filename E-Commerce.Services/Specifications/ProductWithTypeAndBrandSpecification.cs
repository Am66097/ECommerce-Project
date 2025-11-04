using E_Commerce.Domain.Entities.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Specifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpecifications<Product, int>
    {

        // Get All Products + Includes
     public ProductWithTypeAndBrandSpecification() :base(null!) // ! => مش عارف هنا صح ولا لاء 
        {
            AddInclude(p => p.ProductType);
            AddInclude(p=>p.ProductBrand);
        }

        // Get Single Product By Id + Includes
        public ProductWithTypeAndBrandSpecification(int id) : base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p=>p.ProductBrand);

        }
    }
}
