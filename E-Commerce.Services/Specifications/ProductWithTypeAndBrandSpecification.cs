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

        // And Addition a Filter for it :-
        // p=>p.BrandId == brandId -> brandId is not null
        // p=>p.TypeId == typeId -> typeId is not null
        // p=>p.TypeId == typeId &&  p=>p.BrandId == brandId -> typeId and brandId are not null

        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId) : base(
            p => (!brandId.HasValue || p.BrandId == brandId.Value)
            && (!typeId.HasValue || p.TypeId == typeId.Value))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        // Get Single Product By Id + Includes
        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
    }
}
