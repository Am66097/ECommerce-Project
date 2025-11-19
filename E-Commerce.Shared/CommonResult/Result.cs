using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Shared.CommonResult
{
    public class Result
    {
        private readonly List<Error> _errors = [];
        public IReadOnlyList<Error> Errors => _errors;
        public bool IsSuccess => _errors.Count == 0; // true
        public bool IsFailure => !IsSuccess; // false

        //Ok - Success
        protected Result() { }
        //Fail With Error
        public Result(Error error)
        {
            _errors.Add(error);
        }
        //Fail With List<Error>
        public Result(List<Error> errors)
        {
            _errors = errors;
        }






    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Can Not Access To The Value");

        //Ok - Success With Value 
        protected Result(TValue value) : base()
        {
            _value = value;
        }
        //Fail With Error With Value 
        public Result(Error error) : base()
        {
            _value = default!;
        }
        //Fail With List<Error> With Value 
        public Result(List<Error> errors) : base()
        {
            _value = default!;

        }

    }
}
