﻿using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace CryptoManager.Domain.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid BaseAssetId { get; set; }
        public Guid QuoteAssetId { get; set; }
        public DateTime Date { get; set; }
        public Guid ExchangeId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}