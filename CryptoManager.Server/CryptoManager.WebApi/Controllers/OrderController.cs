using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoManager.Domain.Contracts.Business;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CryptoManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/order")]
    public class OrderController : BaseController
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderBusiness _business;

        public OrderController(IOrderRepository repository,
                               IOrderBusiness business,
                               IMapper mapper) : base(mapper)
        {
            _repository = repository;
            _business = business;
        }

        /// <summary>
        /// gets all orders by logged user from database
        /// </summary>
        /// <returns>list of all orders</returns>
        /// <response code="200">if success</response>
        [HttpGet]
        [Route("GetOrderDetailsByApplicationUser")]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> GetOrderDetailsByApplicationUser()
        {
            return Ok(await _business.GetOrdersDetailsByApplicationUserAsync(GetUserId()));
        }

        /// <summary>
        /// Save an order in database
        /// </summary>
        /// <param name="entity">order to save in database</param>
        /// <response code="200">if success</response>
        [HttpPost]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Post([FromBody]OrderDTO entity)
        {
            var order = _mapper.Map<Order>(entity);
            order.ApplicationUserId = GetUserId();
            return Ok(_mapper.Map<OrderDTO>(await _repository.InsertAsync(order)));
        }
    }
}
