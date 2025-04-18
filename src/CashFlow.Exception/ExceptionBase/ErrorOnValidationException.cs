namespace CashFlow.Exception.ExceptionBase
{
    public class ErrorOnValidationException : BaseException
    {
        public List<string> Errors { get; set; }
        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            Errors = errorMessages;

        }
    }
}
