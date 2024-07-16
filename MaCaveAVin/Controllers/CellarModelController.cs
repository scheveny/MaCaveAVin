using Dal.Interfaces;
using Dal.IRepositories;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CellarModelController : ControllerBase
    {
        private readonly ICellarModelRepository _cellarModelRepository;

        public CellarModelController(ICellarModelRepository cellarModelRepository) // Injection de dépendances
        {
            _cellarModelRepository = cellarModelRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarModel>))]
        public async Task<IActionResult> GetCellarModels()
        {
            var models = await _cellarModelRepository.GetAllCellarModelsAsync();
            return Ok(models);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarModel))]
        public async Task<IActionResult> GetCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var model = await _cellarModelRepository.GetCellarModelByIdAsync(id);

            if (model == null)
                return NotFound();

            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarModel))]
        public async Task<IActionResult> AddCellarModel([FromBody] CellarModel cellarModel)
        {
            if (cellarModel == null)
                return BadRequest();

            await _cellarModelRepository.AddCellarModelAsync(cellarModel);

            return Created($"cellar/{cellarModel.CellarModelId}", cellarModel);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellarModel([FromRoute] int id, [FromBody] CellarModel cellarModel)
        {
            if (id <= 0 || id != cellarModel.CellarModelId)
                return BadRequest();

            await _cellarModelRepository.UpdateCellarModelAsync(cellarModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarModel))]
        public async Task<IActionResult> RemoveCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var model = await _cellarModelRepository.GetCellarModelByIdAsync(id);

            if (model == null)
                return NotFound();

            await _cellarModelRepository.RemoveCellarModelAsync(model);

            return Ok(model);
        }
    }
}
