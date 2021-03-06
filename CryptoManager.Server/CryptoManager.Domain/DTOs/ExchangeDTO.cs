﻿using CryptoManager.Domain.Contracts.Entities;
using CryptoManager.Domain.IntegrationEntities.Exchanges;
using System;
using System.Collections.Generic;

namespace CryptoManager.Domain.DTOs
{
    public class ExchangeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string APIUrl { get; set; }
        public ExchangesIntegratedType ExchangeType { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}