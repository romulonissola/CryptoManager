using System;

namespace CryptoManager.Domain.Contracts.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsExcluded { get; set; }
        bool IsEnabled { get; set; }
        DateTime RegistryDate { get; set; }
    }
}
