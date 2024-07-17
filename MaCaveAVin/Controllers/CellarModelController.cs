using Dal.IRepositories;
using DomainModel;
using DomainModel.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.DTO;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CellarModelController : ControllerBase
    {
        private readonly ICellarModelRepository _cellarModelRepository;
        private readonly UserManager<AppUser> _userManager;

        public CellarModelController(ICellarModelRepository cellarModelRepository, UserManager<AppUser> userManager) // Dependency injection
        {
            _cellarModelRepository = cellarModelRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarModelDto>))]
        public async Task<IActionResult> GetCellarModels()
        {
            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var models = await _cellarModelRepository.GetCellarModelsByUserIdAsync(userId);

            var modelDtos = models.Select(m => new CellarModelDto
            {
                CellarModelId = m.CellarModelId,
                CellarBrand = m.CellarBrand,
                CellarTemperature = m.CellarTemperature
            }).ToList();

            return Ok(modelDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarModelDto))]
        public async Task<IActionResult> GetCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var model = await _cellarModelRepository.GetCellarModelByIdAndUserIdAsync(id, userId);

            if (model == null)
                return NotFound();

            var modelDto = new CellarModelDto
            {
                CellarModelId = model.CellarModelId,
                CellarBrand = model.CellarBrand,
                CellarTemperature = model.CellarTemperature
            };

            return Ok(modelDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarModelDto))]
        public async Task<IActionResult> AddCellarModel([FromBody] CreateCellarModelDto createCellarModelDto)
        {
            if (createCellarModelDto == null)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID

            var cellarModel = new CellarModel
            {
                CellarBrand = createCellarModelDto.CellarBrand,
                CellarTemperature = createCellarModelDto.CellarTemperature,
                UserId = userId
            };

            await _cellarModelRepository.AddCellarModelAsync(cellarModel);

            var modelDto = new CellarModelDto
            {
                CellarModelId = cellarModel.CellarModelId,
                CellarBrand = cellarModel.CellarBrand,
                CellarTemperature = cellarModel.CellarTemperature
            };

            return Created($"cellar/{modelDto.CellarModelId}", modelDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellarModel([FromRoute] int id, [FromBody] UpdateCellarModelDto updateCellarModelDto)
        {
            if (id <= 0 || updateCellarModelDto == null)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID

            var existingModel = await _cellarModelRepository.GetCellarModelByIdAndUserIdAsync(id, userId);
            if (existingModel == null)
                return NotFound();

            existingModel.CellarBrand = updateCellarModelDto.CellarBrand;
            existingModel.CellarTemperature = updateCellarModelDto.CellarTemperature;

            await _cellarModelRepository.UpdateCellarModelAsync(existingModel);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarModelDto))]
        public async Task<IActionResult> RemoveCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var model = await _cellarModelRepository.GetCellarModelByIdAndUserIdAsync(id, userId);

            if (model == null)
                return NotFound();

            await _cellarModelRepository.RemoveCellarModelAsync(model);

            var modelDto = new CellarModelDto
            {
                CellarModelId = model.CellarModelId,
                CellarBrand = model.CellarBrand,
                CellarTemperature = model.CellarTemperature
            };

            return Ok(modelDto);
        }
    }
}
