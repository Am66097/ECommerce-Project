using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentaion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {

            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);

        }

        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);

        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // if no errors are provided ,return 500 error
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Unexpected Error");

            // if all errors are validation errors , handle them as validation problem
            if (errors.All(e => e.Type == ErrorType.Validation))
                return HandleValidationProblem(errors);

            // if there id only one error , handle it as a single error problem
            return HandleSingleErrorProblem(errors[0]);

        }
        private ActionResult HandleSingleErrorProblem(Error error)
        {

            return Problem(
                title: error.Code,
                detail: error.Descreption,
                type: error.Type.ToString(),
                statusCode: MapErrorTypeTostatusCode(error.Type)
                );
        }
        private static int MapErrorTypeTostatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.InvalidCredentials => StatusCodes.Status400BadRequest,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError

        };

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelstate = new ModelStateDictionary();
            foreach (var error in errors)
                modelstate.AddModelError(error.Code, error.Descreption);
            return ValidationProblem(modelstate);
        }
    }
}
