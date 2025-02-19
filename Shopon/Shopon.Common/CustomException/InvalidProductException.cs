namespace Shopon.Common.CustomException
{
    /// <summary>
    /// The InvalidProductException
    /// </summary>
    public class InvalidProductException: ApplicationException
    {
        
        public InvalidProductException() { }

        public InvalidProductException(string message) : base(message) { }

         public InvalidProductException(string message, Exception innerException) : base(message, innerException) { }

    }
}
