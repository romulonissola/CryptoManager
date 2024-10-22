namespace CryptoManager.Domain.DTOs
{
    public class ObjectResult<T> : SimpleObjectResult
    {
        public T Item { get; set; }

        public static ObjectResult<T> Success(T item, string successMessage = null)
        {
            return new ObjectResult<T>
            {
                Item = item,
                SuccessMessage = successMessage
            };
        }


        public new static ObjectResult<T> Error(string errorMessage)
        {
            return new ObjectResult<T>
            {
                ErrorMessage = errorMessage
            };
        }
    }
}