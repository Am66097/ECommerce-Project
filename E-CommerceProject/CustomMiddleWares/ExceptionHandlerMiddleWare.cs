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
                if(httpcontext.Response.StatusCode==StatusCodes.Status404NotFound)
                {
                    var Problem = new ProblemDetails()
                    {
                        Title = "Error Will Processing The Http Request - EndPoint Not Found !",
                        Status = StatusCodes.Status404NotFound,
                        Detail = $"EndPoint {httpcontext.Request.Path} Not Found",
                        Instance = httpcontext.Request.Path
                    };
                    await httpcontext.Response.WriteAsJsonAsync(Problem);
                }
            }
            catch (Exception ex)
            {
                //1.Logging
                _logger.LogError(ex, "Something Went Wrong !");

                //2.Return Custom Error Respone
                httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var Problem = new ProblemDetails()
                {
                    Title = "An Unexpected Error Occurred !",
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = ex.Message,
                    Instance = httpcontext.Request.Path
                };
                await httpcontext.Response.WriteAsJsonAsync(Problem);
            }

        }
    }
}
