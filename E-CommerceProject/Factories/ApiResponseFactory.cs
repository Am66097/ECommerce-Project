using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace E_CommerceProject.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() > 0)
                                                                             .ToDictionary(x => x.Key,
                                                                             x => x.Value.Errors.Select(x => x.ErrorMessage.ToArray()));
            var Problem = new ProblemDetails
            {
                Title = "Validation Errors",
                Detail = "One or More Validation Errors Occurred.",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            {"Errors",Errors }
                        }
            };
            return new BadRequestObjectResult(Problem);

        }

    }
}
