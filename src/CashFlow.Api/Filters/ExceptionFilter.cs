using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BaseException)
            {
                HandleException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void HandleException(ExceptionContext context) 
        {
            if(context.Exception is ErrorOnValidationException) 
            {
                var errorOnValidationException = (ErrorOnValidationException)context.Exception;
                var errorResponse = new ResponseErrorJson(errorOnValidationException.Errors);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new BadRequestObjectResult(errorResponse);
            } else
            {
                var errorResponse = new ResponseErrorJson("An error occurred");
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(errorResponse);
            }
        }
        private void ThrowUnknowError(ExceptionContext context)
        {
            var errorResponse = new ResponseErrorJson(ResourceErrorMessages.UNKNOWN_ERROR);
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(errorResponse);
        }
    }
}
