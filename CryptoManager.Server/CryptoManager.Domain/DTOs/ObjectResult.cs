namespace CryptoManager.Domain.DTOs
{
    public class ObjectResult<T> : SimpleObjectResult
    {
        public T Item { get; set; }
    }
}