namespace CashFlow.Exception.ExceptionBase
{
    public abstract class BaseException : SystemException
    {
        protected BaseException(string message) : base(message)
        {
        }
    }
}
