using E_Commerce.Shared;
using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Abstraction
{
    public interface IProductService
    {
        // Get All Products Return ProuductDTO
        Task<PaginatedResult<ProductDTO>> GetAllProductAsync(ProductQueryParams queryParams);

        // Get Product By ID Return ProductDOT
        Task<Result<ProductDTO>> GetProductByIdAsync(int id);

        // Get All Brands Return IEumerable of  BrandDTO
        Task<IEnumerable<BrandDTO>> GetAllBransdAsync();

        // Get All Types Return IEumerable of  TypeDTO
        Task<IEnumerable<TypeDTO>> GetAllTypesdAsync();



    }
}
