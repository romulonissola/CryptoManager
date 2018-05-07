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
        ///// <summary>
        ///// gets all orders by logged user from database
        ///// </summary>
        ///// <returns>list of all orders</returns>
        ///// <response code="200">if success</response>
        //[HttpGet]
        //[ProducesResponseType(typeof(ObjectResult), 200)]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok(_mapper.Map<List<OrderDTO>>(await _repository.GetAll()));
        //}

        /// <summary>
        /// gets an order by id
        /// </summary>
        /// <param name="id">Guid with id from an order</param>
        /// <returns>order that match with a parameter</returns>
        /// <response code="200">if success</response>
        /// <response code="404">if parameter don't match with an order</response>
        //[HttpGet("{id}")]
        //[ProducesResponseType(typeof(ObjectResult), 404)]
        //[ProducesResponseType(typeof(ObjectResult), 200)]
        //public async Task<IActionResult> Get(Guid id)
        //{
        //    var entity = await _repository.GetAsync(id);
        //    if(entity == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<OrderDTO>(entity));
        //}

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

        /// <summary>
        /// delete an order from database
        /// </summary>
        /// <param name="id">id of an order to delete</param>
        /// <response code="404">if parameter don't match with an order</response>
        /// <response code="200">if success</response>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(ObjectResult), 404)]
        //[ProducesResponseType(typeof(ObjectResult), 200)]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var entity = await _repository.GetAsync(id);
        //    if(entity == null)
        //    {
        //        return NotFound();
        //    }
        //    await _repository.DeleteAsync(entity);
        //    return Ok();
        //}
    }
}
