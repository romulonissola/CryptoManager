using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using CryptoManager.WebApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Exchange")]
    public class ExchangeController : BaseController
    {
        private readonly IExchangeRepository _repository;

        public ExchangeController(IExchangeRepository repository, 
                                  IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }
        /// <summary>
        /// gets all exchanges from database
        /// </summary>
        /// <returns>list of all exchanges</returns>
        /// <response code="200">if success</response>
        [HttpGet]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<ExchangeDTO>>(await _repository.GetAll()));
        }

        /// <summary>
        /// gets an exchange by id
        /// </summary>
        /// <param name="id">Guid with id from an exchange</param>
        /// <returns>Exchange that match with a parameter</returns>
        /// <response code="200">if success</response>
        /// <response code="404">if parameter don't match with an exchange</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ObjectResult), 404)]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if(entity == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ExchangeDTO>(entity));
        }

        /// <summary>
        /// Save an exchange in database
        /// </summary>
        /// <param name="entity">exchange to save in database</param>
        /// <response code="200">if success</response>
        [HttpPost]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        [Authorize(Roles = WebUtil.ADMINISTRATOR_ROLE_NAME)]
        public async Task<IActionResult> Post([FromBody]ExchangeDTO entity)
        {
            return Ok(_mapper.Map<ExchangeDTO>(await _repository.InsertAsync(_mapper.Map<Exchange>(entity))));
        }

        /// <summary>
        /// update an exchange in database
        /// </summary>
        /// <param name="entity">exchange to update</param>
        /// <response code="200">if success</response>
        [HttpPut]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        [Authorize(Roles = WebUtil.ADMINISTRATOR_ROLE_NAME)]
        public async Task<IActionResult> Put([FromBody]ExchangeDTO entity)
        {
            await _repository.UpdateAsync(_mapper.Map<Exchange>(entity));
            return Ok();
        }

        /// <summary>
        /// delete an exchange from database
        /// </summary>
        /// <param name="id">id of an exchange to delete</param>
        /// <response code="404">if parameter don't match with an exchange</response>
        /// <response code="200">if success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ObjectResult), 404)]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        [Authorize(Roles = WebUtil.ADMINISTRATOR_ROLE_NAME)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if(entity == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(entity);
            return Ok();
        }
    }
}
