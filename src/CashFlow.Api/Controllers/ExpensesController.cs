using CashFlow.Application.UseCases.Expenses.DeleteExpenseById;
using CashFlow.Application.UseCases.Expenses.GetAllExpenses;
using CashFlow.Application.UseCases.Expenses.GetExpenseById;
using CashFlow.Application.UseCases.Expenses.RegisterExpenses;
using CashFlow.Application.UseCases.Expenses.UpdateExpense;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterExpenses([FromServices] IRegisterExpensesUseCase useCase, [FromBody] RequestExpenseJson request)
        {
            var response = await useCase.Execute(request);
            return Created(String.Empty, response);
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase) 
        {
            var response = await useCase.Execute();
            if(response.Expenses.Count != 0)
            {
                return Ok(response);
            }
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] long id) 
        {
            var response = await useCase.Execute(id);
            return Ok(response);        
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExpenseById([FromServices] IDeleteExpenseByIdUseCase useCase, [FromRoute] long id) 
        {
            await useCase.Execute(id);
            return NoContent();
        }
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExpense([FromServices] IUpdateExpenseUseCase useCase, [FromRoute] long id, [FromBody] RequestExpenseJson request)
        {
            await useCase.Execute(id, request);
            return NoContent();
        }
    }
}
