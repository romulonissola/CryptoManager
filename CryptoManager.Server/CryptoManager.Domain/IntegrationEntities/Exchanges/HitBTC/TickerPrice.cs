using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Domain.IntegrationEntities.Exchanges.HitBTC
{
    public class TickerPrice
    {
        public string Symbol { get; set; }
        public decimal Last { get; set; }
    }
}
