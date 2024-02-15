namespace Stock.Domain.Exceptions
{
    

    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public Exception UnknownException { get; }

        public CustomException(string message, int statusCode, Exception unknownException) : base(message)
        {
            StatusCode = statusCode;
            UnknownException = unknownException;
        }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException(string message, Exception ex) : base(message, 404, ex)
        {
        }
    }

    public class BadRequestException : CustomException
    {
        public BadRequestException(string message, Exception ex) : base(message, 400, ex)
        {
        }
    }

}
