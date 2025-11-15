using E_Commerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentaion.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMinutes;

        public RedisCacheAttribute(int DurationInMinutes = 5)
        {
            _durationInMinutes = DurationInMinutes;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1.Get Cache Service From Dependancy Ingection Container
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            // 2.Create Cache Key Based On Request Path And Query String
            var CacheKey = CreateCacheKey(context.HttpContext.Request);

            // 3.Check If Cached Data Exists
            var CahceValue = await CacheService.GetAsync(CacheKey);

            // 4.If Exists , Return Cached Data and Skip Executing Of Endpoint
            if (CahceValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = CahceValue,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            // 5.If Not Exists , Excute Endpoint And Store The Data In Cache if 200 OK Response
            var ExcutedContext = await next.Invoke();
            if(ExcutedContext.Result is OkObjectResult result)
            {
                await CacheService.SetAsync(CacheKey, result.Value!,TimeSpan.FromMinutes(_durationInMinutes));
            }

        }

        // /api/Products
        // /api/Products?brandId=2&typeId=1
        // /api/Products?typeId=1
        // /api/Products?typeId=1&brandId=2



        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append(request.Path); // /api/Products
            foreach (var item in request.Query.OrderBy(x => x.Key))
                key.Append($"| {item.Key}-{item.Value}");
            return key.ToString();
        }


    }
}
