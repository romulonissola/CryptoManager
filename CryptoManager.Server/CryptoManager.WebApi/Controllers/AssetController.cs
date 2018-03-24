using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CryptoManager.Domain.Contracts.Repositories;
using CryptoManager.Domain.DTOs;
using CryptoManager.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CryptoManager.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Asset")]
    public class AssetController : BaseController
    {
        private readonly IAssetRepository _repository;

        public AssetController(IAssetRepository repository, IMapper mapper) : base(mapper)
        {
            _repository = repository;
        }
        /// <summary>
        /// gets all Assets from database
        /// </summary>
        /// <returns>list of all Assets</returns>
        /// <response code="200">if success</response>
        [HttpGet]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<AssetDTO>>(await _repository.GetAll()));
        }

        /// <summary>
        /// gets an Asset by id
        /// </summary>
        /// <param name="id">Guid with id from an Asset</param>
        /// <returns>Asset that match with a parameter</returns>
        /// <response code="200">if success</response>
        /// <response code="404">if parameter don't match with an Asset</response>
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
            return Ok(_mapper.Map<AssetDTO>(entity));
        }

        /// <summary>
        /// Save an Asset in database
        /// </summary>
        /// <param name="entity">Asset to save in database</param>
        /// <response code="200">if success</response>
        [HttpPost]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Post([FromBody]AssetDTO entity)
        {
            return Ok(_mapper.Map<AssetDTO>(await _repository.InsertAsync(_mapper.Map<Asset>(entity))));
        }

        /// <summary>
        /// update an Asset in database
        /// </summary>
        /// <param name="entity">Asset to update</param>
        /// <response code="200">if success</response>
        [HttpPut]
        [ProducesResponseType(typeof(ObjectResult), 200)]
        public async Task<IActionResult> Put([FromBody]AssetDTO entity)
        {
            await _repository.UpdateAsync(_mapper.Map<Asset>(entity));
            return Ok();
        }

        /// <summary>
        /// delete an Asset from database
        /// </summary>
        /// <param name="id">id of an Asset to delete</param>
        /// <response code="404">if parameter don't match with an Asset</response>
        /// <response code="200">if success</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ObjectResult), 400)]
        [ProducesResponseType(typeof(ObjectResult), 200)]
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
