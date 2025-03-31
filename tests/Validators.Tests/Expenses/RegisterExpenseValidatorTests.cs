using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Exception;

namespace Validators.Tests.Expenses
{
    public class RegisterExpenseValidatorTests
    {
        private readonly RegisterExpenseValidator _validator;
        private RequestExpenseJson _requestJson;
        public RegisterExpenseValidatorTests()
        { 
            _validator = new RegisterExpenseValidator();
            _requestJson = new RequestExpenseJson
            {
                Title = "Validate Title",
                Description = "Test expense",
                Date = DateTime.Now,
                Amount = 100,
                PaymentType = PaymentType.Cash
            }; ;

        }
        [Fact]
        public void Validate_All_Fields_Are_Valid()
        {
            // When
            var response = _validator.Validate(_requestJson);

            // Then
            Assert.True(response.IsValid);
            Assert.Empty(response.Errors);
        }

        [Fact]
        public void Validate_Title_Is_Required()
        {
            // Given
            _requestJson.Title = "";

            // When
            var response = _validator.Validate(_requestJson);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Title" && e.ErrorMessage == ResourceErrorMessages.REQUIRED_TITLE);
        }
        [Fact]
        public void Validate_Amount_Is_Greater_Than_Zero()
        { 
            // Given
            _requestJson.Amount = 0;

            // When
            var response = _validator.Validate(_requestJson);
            
            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Amount" && e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        }
        [Fact]
        public void Validate_Date_Is_Not_In_The_Future()
        { 
            // Given
            _requestJson.Date = DateTime.Now.AddDays(1);

            // When
            var response = _validator.Validate(_requestJson);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Date" && e.ErrorMessage == ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
        }

        [Fact]
        public void Validate_Payment_Type_Is_Valid()
        {
            // Given
            _requestJson.PaymentType = (PaymentType)999;

            // When
            var response = _validator.Validate(_requestJson);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "PaymentType" && e.ErrorMessage == ResourceErrorMessages.INVALID_PAYMENT_TYPE);
        }

    }
}
