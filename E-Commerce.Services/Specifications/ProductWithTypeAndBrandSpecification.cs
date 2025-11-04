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
     public ProductWithTypeAndBrandSpecification() :base()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p=>p.ProductBrand);
        }

    }
}
