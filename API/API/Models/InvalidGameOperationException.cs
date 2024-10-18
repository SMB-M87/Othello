namespace API.Models
{
    public class InvalidGameOperationException : Exception
    {
        public InvalidGameOperationException(string message) : base(message) { }

        public InvalidGameOperationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
