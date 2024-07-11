using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using DomainModel.DTO;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CellarController : ControllerBase
    {
        private readonly CellarContext context;

        public CellarController(CellarContext context) //injection de dépendances
        {
            this.context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult GetCellars()
        {
            return Ok(context.Cellars.ToList());
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

            var cellar = context.Cellars.Find(id);

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

            var cellars = context.Cellars
                .Where(c => c.CellarName.Contains(name))
                .ToList();

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
            var cellars = context.Cellars.Where(c => c.CellarModelId == modelId).ToList();

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
            var cellars = context.Cellars.Where(c => c.CellarCategoryId == categoryId).ToList();

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

            var user = context.Users.Find(cellarDto.UserId);
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

            context.Cellars.Add(cellar);
            context.SaveChanges();

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
            context.Cellars.Update(cellar);
            context.SaveChanges();

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

            var cellar = context.Cellars.Find(id);

            if (cellar == null)
                return NotFound();

            context.Cellars.Remove(cellar);
            context.SaveChanges();

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
