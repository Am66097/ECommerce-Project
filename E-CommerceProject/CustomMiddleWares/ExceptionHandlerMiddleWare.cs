using Azure;
using E_Commerce.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.CustomMiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next,ILogger<ExceptionHandlerMiddleWare> logger)
        {
            _next = Next;
            this._logger = logger;
        }

        public async Task Invoke(HttpContext httpcontext)
        {
            try
            {
                await _next.Invoke(httpcontext);
                await HandleNotFoundEndPointAsync(httpcontext);
            }
            catch (Exception ex)
            {
                //1.Logging
               
                _logger.LogError(ex, "Something Went Wrong !");

                //2.Return Custom Error Respone
              
                var Problem = new ProblemDetails()
                {
                    Title = "An Unexpected Error Occurred !",
                    Detail = ex.Message,
                    Instance = httpcontext.Request.Path,
                    Status = ex switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    }
                };
                httpcontext.Response.StatusCode = Problem.Status.Value;
                await httpcontext.Response.WriteAsJsonAsync(Problem);
            }

        }

        private static async Task HandleNotFoundEndPointAsync(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound && !httpcontext.Response.HasStarted)
            {
                var Response = new ProblemDetails()
                {
                    Title = "Error Will Processing The Http Request - EndPoint Not Found !",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"EndPoint {httpcontext.Request.Path} Not Found",
                    Instance = httpcontext.Request.Path
                };
                await httpcontext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
