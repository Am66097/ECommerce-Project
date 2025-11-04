using E_Commerce.Domain.Entities.ProductModule;
using E_Commerce.Shared;
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

        // search 
        public ProductWithTypeAndBrandSpecification(ProductQueryParams queryParams) : base(
            p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value)
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrEmpty(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(x => x.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PagerIndex);
        }

        // Get Single Product By Id + Includes
        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }


    }
}
