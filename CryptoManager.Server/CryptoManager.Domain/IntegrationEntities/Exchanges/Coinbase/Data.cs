using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Domain.IntegrationEntities.Exchanges.Coinbase
{
    public class Data
    {
        public string Base { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
