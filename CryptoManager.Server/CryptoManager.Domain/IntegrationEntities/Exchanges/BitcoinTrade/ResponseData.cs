using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Domain.IntegrationEntities.Exchanges.BitcoinTrade
{
    public class ResponseData<T>
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public T Data { get; set; }
    }
}
