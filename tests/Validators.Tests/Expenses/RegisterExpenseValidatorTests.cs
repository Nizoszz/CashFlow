using CashFlow.Application.UseCases.Expenses;
using CashFlow.Domain.Enums;
using CashFlow.Exception;
using Utils.Tests.Factory;

namespace Validators.Tests.Expenses
{
    public class RegisterExpenseValidatorTests
    {
        private readonly RegisterExpenseValidator _validator = new RegisterExpenseValidator();

        [Fact]
        public void Validate_All_Fields_Are_Valid()
        {
            // Given
            var request = RegisterExpenseFactory.Build();
            // When
            var response = _validator.Validate(request);

            // Then
            Assert.True(response.IsValid);
            Assert.Empty(response.Errors);
        }

        [Theory]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData(null)]
        public void Validate_Title_Is_Required(string value)
        {
            // Given
            var request = RegisterExpenseFactory.Build();
            request.Title = value;

            // When
            var response = _validator.Validate(request);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Title" && e.ErrorMessage == ResourceErrorMessages.REQUIRED_TITLE);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Validate_Amount_Is_Greater_Than_Zero(int value)
        {
            // Given
            var request = RegisterExpenseFactory.Build();
            request.Amount = value;

            // When
            var response = _validator.Validate(request);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Amount" && e.ErrorMessage == ResourceErrorMessages.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        }
        [Fact]
        public void Validate_Date_Is_Not_In_The_Future()
        {
            // Given
            var request = RegisterExpenseFactory.Build();
            request.Date = DateTime.Now.AddDays(1);

            // When
            var response = _validator.Validate(request);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "Date" && e.ErrorMessage == ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);
        }

        [Fact]
        public void Validate_Payment_Type_Is_Valid()
        {
            // Given
            var request = RegisterExpenseFactory.Build();
            request.PaymentType = (PaymentType)999;

            // When
            var response = _validator.Validate(request);

            // Then
            Assert.NotNull(response);
            Assert.False(response.IsValid);
            Assert.Single(response.Errors, e => e.PropertyName == "PaymentType" && e.ErrorMessage == ResourceErrorMessages.INVALID_PAYMENT_TYPE);
        }

    }
}
