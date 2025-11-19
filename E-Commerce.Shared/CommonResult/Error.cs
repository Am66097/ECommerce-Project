using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {

        public string Code { get; }
        public string Descreption { get; }
        public ErrorType Type { get; }
        private Error(string code, string descreption, ErrorType type)
        {
            Code = code;
            Descreption = descreption;
            Type = type;
        }

        #region Static Factory Methods

        public static Error Failure(string Code = "General.Failure", string Descreption = "An unexpected error occurred. Please try again later")
        {
            return new Error(Code, Descreption, ErrorType.Failure);
        }
        public static Error Validation(string Code = "General.Validation", string Descreption = "Validation Error Has Occured")
        {
            return new Error(Code, Descreption, ErrorType.Validation);
        }
        public static Error NotFound(string Code = "General.NotFound", string Descreption = "The Requested Resource Was Not Found")
        {
            return new Error(Code, Descreption, ErrorType.NotFound);
        }
        public static Error Unauthorized(string Code = "General.Unauthorized", string Descreption = "You Are Not Authorized To This Process")
        {
            return new Error(Code, Descreption, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string Code = "General.Forbidden", string Descreption = "You Do Not Have Permission To This Process")
        {
            return new Error(Code, Descreption, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string Code = "General.InvalidCredentials", string Descreption = "The provided credentials are incorrect")
        {
            return new Error(Code, Descreption, ErrorType.InvalidCredentials);
        }
        #endregion

    }
}
