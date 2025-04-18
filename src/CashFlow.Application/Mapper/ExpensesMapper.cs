using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.Mapper
{
    public class ExpensesMapper : Profile
    {
        public ExpensesMapper() {
            toEntity();
            fromEntity();
        }
        private void toEntity() {
            CreateMap<RequestExpenseJson, ExpenseEntity>();
        }
        private void fromEntity() {
            CreateMap<ExpenseEntity, ResponseExpenseJson>();
            CreateMap<ExpenseEntity, ResponseShortExpenseJson>();
            CreateMap<ExpenseEntity, ResponseExpenseJson>();
        }
    }

}
