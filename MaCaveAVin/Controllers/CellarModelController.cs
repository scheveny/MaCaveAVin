
using Dal;
using Dal.IRepositories;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.DTO.cellarModel;
using DomainModel.DTOs.Bottle;
//using DomainModel.DTO;
using Microsoft.AspNetCore.Authorization;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class CellarModelController : ControllerBase
    {
        private readonly CellarContext _cellarContext;
        private readonly ICellarModelRepository _cellarModelRepository;
        private readonly ILogger _logger;
        private readonly UserManager<AppUser> _userManager;

        public CellarModelController(CellarContext context, ILogger<CellarModelController> logger, UserManager<AppUser> userManager,ICellarModelRepository cellarModelRepository) // Injection de dépendances
        {
            this._cellarContext = context;
            this._logger = logger;
            this._cellarModelRepository = cellarModelRepository;
            this._userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarModel>))]
        [AllowAnonymous]
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

        

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostMyCellarModel([FromBody] CreateCellarModelDto createCellarModelDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            CellarModel cellarModel = new CellarModel
            {
                CellarBrand = createCellarModelDto.CellarBrand,
                CellarTemperature = createCellarModelDto.CellarTemperature,
                //AppUserId = user.Id
            };

            await _cellarModelRepository.AddCellarModelAsync(cellarModel);

            return CreatedAtAction(nameof(GetCellarModel), new { id = cellarModel.CellarModelId }, cellarModel);
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
