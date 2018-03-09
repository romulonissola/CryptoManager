using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.Entities;
using CryptoManager.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoManager.Repository.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IORM<Order> orm) : base(orm)
        {

        }
    }
}
