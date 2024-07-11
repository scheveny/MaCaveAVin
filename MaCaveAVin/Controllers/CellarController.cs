using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using DomainModel.DTO;
using Dal.IRepositories;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CellarController : ControllerBase
    {
        private readonly ICellarRepository cellarRepository;
        private readonly IUserRepository userRepository;

        public CellarController(ICellarRepository cellarRepository, IUserRepository userRepository) // Injection de dépendances
        {
            this.cellarRepository = cellarRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult GetCellars()
        {
            return Ok(cellarRepository.GetAllCellars());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult GetCellarById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellar = cellarRepository.GetCellarById(id);

            if (cellar == null)
                return NotFound();

            return Ok(cellar);
        }

        [HttpGet("cellarbyname")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult SearchCellars([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name parameter is required.");

            var cellars = cellarRepository.SearchCellarsByName(name);

            if (!cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpGet("cellarbymodel/{modelId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult GetAllCellarsByModel([FromRoute] int modelId)
        {
            var cellars = cellarRepository.GetCellarsByModel(modelId);

            if (cellars == null || !cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpGet("cellarbycategory/{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult GetAllCellarsByCategory([FromRoute] int categoryId)
        {
            var cellars = cellarRepository.GetCellarsByCategory(categoryId);

            if (cellars == null || !cellars.Any())
                return NotFound();

            return Ok(cellars);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Produces(typeof(Cellar))]
        public IActionResult AddCellar([FromBody] CreateCellarDto cellarDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cellarDto.NbRow <= 0)
                return BadRequest("The number of rows (NbRow) must be greater than 0.");

            if (cellarDto.NbStackRow <= 0)
                return BadRequest("The number of stacks per row (NbStackRow) must be greater than 0.");

            var user = userRepository.GetUserById(cellarDto.UserId);
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

            cellarRepository.AddCellar(cellar);

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
        public IActionResult UpdateCellar(
            [FromRoute] int id,
            [FromBody] Cellar cellar)
        {
            if (id <= 0 || id != cellar.CellarId)
                return BadRequest();

            // Vérifie si le modèle est valide
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // mise à jour
            cellarRepository.UpdateCellar(cellar);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult RemoveCellar([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellar = cellarRepository.GetCellarById(id);

            if (cellar == null)
                return NotFound();

            cellarRepository.RemoveCellar(cellar);

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
