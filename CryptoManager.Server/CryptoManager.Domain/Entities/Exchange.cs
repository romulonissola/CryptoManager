using CryptoManager.Domain.Contracts.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using System;
using System.Collections.Generic;

namespace CryptoManager.Domain.Entities
{
    public class Exchange : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string APIUrl { get; set; }
        public bool IsExcluded { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }
        public ExchangesIntegratedType ExchangeType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}