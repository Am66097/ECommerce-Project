using E_Commerce.Services.Abstraction;
using E_Commerce.Shared.DTOs.BasketDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            this._basketService = basketService;
        }

        // GET : BaseUrl/api/Baskets?id= => Id Will Be Token As A Query String
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket(string id)
        {
            var Basket = await _basketService.GetBasketAsync(id);
            return Ok(Basket);
        }

        // POST : BaseUrl/api/Baskets
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdateBasket(BasketDTO basket)
        {
            var Basket = await _basketService.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }

        // Delete : BaseUrl/api/Bakets/{id} => Id Will Be Token From Route
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return Ok(result);
        }

    }
}
