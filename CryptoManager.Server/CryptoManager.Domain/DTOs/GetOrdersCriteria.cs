using System;
using CryptoManager.Domain.Entities;

namespace CryptoManager.Domain.DTOs
{
    public class GetOrdersCriteria
    {
        public Guid ApplicationUserId { get; set; }
        public bool IsViaRoboTrader { get; set; } = false;
        public bool IsBackTest { get; set; } = false;
        public string SetupTraderId { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public DateTime? EndDate { get; set; } = null;
        public OrderType OrderType { get; set; } = OrderType.Buy;
    }
}