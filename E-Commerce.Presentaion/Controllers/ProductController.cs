using E_Commerce.Presentaion.Attributes;
using E_Commerce.Services.Abstraction;
using E_Commerce.Shared;
using E_Commerce.Shared.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController :ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        //GetAllProducts
        //Get : BaseUrl/api/Products
        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProducts([FromQuery] ProductQueryParams queryParams) 
        {
            var Products = await _productService.GetAllProductAsync(queryParams);
            return Ok(Products);
        }

        //GetProductById
        //Get : BaseUrl/api/Products/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //throw new Exception();
            var Product = await _productService.GetProductByIdAsync(id);
            return Ok(Product);
        }

        //GetAllProductTypes
        //Get : BaseUrl/api/Products/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypes()
        {
            var Types = await _productService.GetAllTypesdAsync();
            return Ok(Types);
        }

        //GetAllProductBrands
        //Get : BaseUrl/api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {
            var Brands = await _productService.GetAllBransdAsync();
            return Ok(Brands);
        }

    }
}
