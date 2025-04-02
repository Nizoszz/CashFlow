using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult RegisterExpenses([FromServices] IRegisterExpensesUseCase useCase, [FromBody] RequestExpenseJson request)
        {
            var response = useCase.Execute(request);
            return Created(String.Empty, response);
        }
    }
}
