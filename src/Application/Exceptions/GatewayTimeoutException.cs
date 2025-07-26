namespace Application.Exceptions
{
  
    public class GatewayTimeoutException : Exception
    {
        public GatewayTimeoutException()
            : base()
        {
        }

        public GatewayTimeoutException(string message)
            : base(message)
        {
        }

        public GatewayTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}
