using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dal;
using DomainModel;
using MaCaveAVin.Filters;

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

        // GET : /class
        /// <summary>
        /// Retourne les classrooms
        /// </summary>
        /// <returns>une liste de classroom</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Cellar>))]
        public IActionResult GetClassrooms()
        {
            return Ok(context.Cellars.ToList());
        }

        // GET : /class/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult GetCellar([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var cellar = context.Cellars
                                        .Find(id);

            if (cellar == null)
                return NotFound();

            return Ok(cellar);
        }

        // POST : /class (avec body)
        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(Cellar))]
        public IActionResult AddClassroom([FromBody] Cellar cellar)
        {
            context.Cellars.Add(cellar);
            context.SaveChanges();

            return Created($"cellar/{cellar.CellarId}", cellar);
        }

        // PUT : /class/5 (avec body)
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateClassroom(
            [FromRoute] int id,
            [FromBody] Cellar cellar)
        {
            if (id <= 0 || id != cellar.CellarId)
                return BadRequest();

            // mise à jour
            context.Cellars.Update(cellar);
            context.SaveChanges();

            return NoContent();
        }

        // DELETE : /class/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult RemoveClassroom([FromRoute] int id)
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
