using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal.Interfaces;
using DomainModel;
using MaCaveAVin.Filters;
using DomainModel.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class CellarController : ControllerBase
    {
        private readonly ICellarRepository _cellarRepository;
        private readonly IUserRepository _userRepository;

        public CellarController(ICellarRepository cellarRepository, IUserRepository userRepository) // Injection de dépendances
        {
            _cellarRepository = cellarRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> GetCellars()
        {
            var cellars = await _cellarRepository.GetAllCellarsAsync();
            return Ok(cellars);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public async Task<IActionResult> GetCellarById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellar = await _cellarRepository.GetCellarByIdAsync(id);

            if (cellar == null)
                return NotFound();

            return Ok(cellar);
        }

        [HttpGet("cellarbyname")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> SearchCellars([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name parameter is required.");

            var cellars = await _cellarRepository.SearchCellarsByNameAsync(name);

            if (!cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpGet("cellarbymodel/{modelId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> GetAllCellarsByModel([FromRoute] int modelId)
        {
            var cellars = await _cellarRepository.GetCellarsByModelAsync(modelId);

            if (cellars == null || !cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpGet("cellarbycategory/{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> GetAllCellarsByCategory([FromRoute] int categoryId)
        {
            var cellars = await _cellarRepository.GetCellarsByCategoryAsync(categoryId);

            if (cellars == null || !cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces(typeof(Cellar))]
        public async Task<IActionResult> AddCellar([FromBody] CreateCellarDto cellarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cellarDto.NbRow <= 0)
                return BadRequest("The number of rows (NbRow) must be greater than 0.");

            if (cellarDto.NbStackRow <= 0)
                return BadRequest("The number of stacks per row (NbStackRow) must be greater than 0.");

            var user = await _userRepository.GetUserByIdAsync(cellarDto.UserId);
            if (user == null)
                return BadRequest("Invalid UserId.");

            var cellar = new Cellar
            {
                CellarName = cellarDto.CellarName,
                NbRow = cellarDto.NbRow,
                NbStackRow = cellarDto.NbStackRow,
                User = user,
                CellarCategoryId = cellarDto.CellarCategoryId,
                CellarModelId = cellarDto.CellarModelId
            };

            await _cellarRepository.AddCellarAsync(cellar);

            var result = new CellarDto
            {
                CellarId = cellar.CellarId,
                CellarName = cellar.CellarName,
                NbRow = cellar.NbRow,
                NbStackRow = cellar.NbStackRow,
                User = new UserDto
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                },
                CellarCategoryId = cellar.CellarCategoryId,
                CellarModelId = cellar.CellarModelId
            };

            return Created($"cellar/{result.CellarId}", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellar([FromRoute] int id, [FromBody] Cellar cellar)
        {
            if (id <= 0 || id != cellar.CellarId)
                return BadRequest();

            // Vérifie si le modèle est valide
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mise à jour
            await _cellarRepository.UpdateCellarAsync(cellar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public async Task<IActionResult> RemoveCellar([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellar = await _cellarRepository.GetCellarByIdAsync(id);

            if (cellar == null)
                return NotFound();

            await _cellarRepository.RemoveCellarAsync(cellar);

            return Ok(cellar);
        }

        [CustomExceptionFilter]
        [HttpGet("customerror")]
        public IActionResult CustomError()
        {
            throw new NotImplementedException("Méthode non implementé");
            return Ok();
        }
    }
}
