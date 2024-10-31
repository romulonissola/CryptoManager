using System;
namespace CryptoManager.Domain.DTOs
{
    public class SimpleObjectResult
    {
        public bool HasSucceded => string.IsNullOrWhiteSpace(ErrorMessage);
        public string ErrorMessage { get; protected set; }
        public string SuccessMessage { get; protected set; }

        public static SimpleObjectResult Success(string successMessage = null)
        {
            return new SimpleObjectResult
            {
                SuccessMessage = successMessage
            };
        }

        public static SimpleObjectResult Error(string errorMessage)
        {
            return new SimpleObjectResult
            {
                ErrorMessage = errorMessage
            };
        }
    }
}
