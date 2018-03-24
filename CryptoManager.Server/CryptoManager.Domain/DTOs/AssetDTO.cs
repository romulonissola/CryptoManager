using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace CryptoManager.Domain.DTOs
{
    public class AssetDTO
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime RegistryDate { get; set; }
    }
}