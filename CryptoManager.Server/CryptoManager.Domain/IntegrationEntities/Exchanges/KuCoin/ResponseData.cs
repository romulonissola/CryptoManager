namespace CryptoManager.Domain.IntegrationEntities.Exchanges.KuCoin
{
    public class ResponseData<T>
    {
        public string Code { get; set; }
        public T Data { get; set; }
    }
}
