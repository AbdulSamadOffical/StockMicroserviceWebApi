namespace Stock.Domain.Exceptions
{
    

    public class CustomException : Exception
    {
        public int StatusCode { get; }

        public CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message, 404)
        {
        }
    }

    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message, 400)
        {
        }
    }

}
