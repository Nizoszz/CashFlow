using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        public IActionResult RegisterExpenses([FromBody] RequestExpenseJson request )
        {
           try {
                var useCase = new RegisterExpensesUseCase();
                var response = useCase.Execute(request);

                return Created(String.Empty, response);
            }
            catch (ArgumentException ex) {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unkown error");
            }
    }
}
