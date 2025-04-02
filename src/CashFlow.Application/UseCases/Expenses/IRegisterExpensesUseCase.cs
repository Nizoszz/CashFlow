using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses
{
    public interface IRegisterExpensesUseCase
    {
        ResponseExpenseJson Execute(RequestExpenseJson request);
      
    }
}
