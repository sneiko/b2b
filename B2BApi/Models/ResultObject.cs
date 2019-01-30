using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B2BApi.Models
{
    public class ResultObject
    {
        public Exception Exception { get; set; }
        public object Result { get; set; }
        public string Message { get; set; }
    }
    
    public class IResultObject : IActionResult
    {
        private readonly ResultObject _result;

        public IResultObject(ResultObject result)
        {
            _result = result;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result.Exception ?? new JsonResult(_result.Result).Value ?? _result.Message)
            {
                StatusCode = _result.Exception != null
                    ? StatusCodes.Status500InternalServerError
                    : StatusCodes.Status200OK
            };
            
            await objectResult.ExecuteResultAsync(context);
        }
    }
}