using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Domain.IntegrationEntities.Exchanges.Binance
{
    public class TickerPrice
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
}
