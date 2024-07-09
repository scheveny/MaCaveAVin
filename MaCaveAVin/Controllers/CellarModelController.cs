using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CellarModelController : ControllerBase
    {
        private readonly CellarContext context;

        public CellarModelController(CellarContext context) //injection de dépendances
        {
            this.context = context;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Bottle>))]
        public IActionResult GetCellarModels()
        {
            return Ok(context.CellarModels.ToList());
        }

        // GET : /class/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult GetCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellarModel = context.CellarModels.Find(id);

            if (cellarModel == null)
                return NotFound();

            return Ok(cellarModel);
        }

        // POST : /class (avec body)
        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarModel))]
        public IActionResult AddCellarModel([FromBody] CellarModel cellarModel)
        {
            context.CellarModels.Add(cellarModel);
            context.SaveChanges();

            return Created($"cellar/{cellarModel.CellarModelId}", cellarModel);
        }

        // PUT : /class/5 (avec body)
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateCellarModel(
            [FromRoute] int id,
            [FromBody] CellarModel cellarModel)
        {
            if (id <= 0 || id != cellarModel.CellarModelId)
                return BadRequest();

            // mise à jour
            context.CellarModels.Update(cellarModel);
            context.SaveChanges();

            return NoContent();
        }

        // DELETE : /class/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult RemoveCellarModel([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellarModel = context.CellarModels.Find(id);

            if (cellarModel == null)
                return NotFound();

            context.CellarModels.Remove(cellarModel);
            context.SaveChanges();

            return Ok(cellarModel);
        }

        // GET : class/customerror
        [CustomExceptionFilter]
        [HttpGet("customerror")]
        public IActionResult CustomError()
        {
            throw new NotImplementedException("Méthode non implementé");
            return Ok();
        }
    }
}
