using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        private readonly CellarContext context;

        public BottleController(CellarContext context) //injection de dépendances
        {
            this.context = context;
        }


        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Bottle>))]
        public IActionResult GetBottles()
        {
            return Ok(context.Bottles.ToList());
        }

        // GET : /class/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult GetBottle([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var bottle = context.Bottles.Find(id);

            if (bottle == null)
                return NotFound();

            return Ok(bottle);
        }

        // POST : /class (avec body)
        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(Bottle))]
        public IActionResult AddBottle([FromBody] Bottle bottle)
        {
            context.Bottles.Add(bottle);
            context.SaveChanges();

            return Created($"cellar/{bottle.BottleId}", bottle);
        }

        // PUT : /class/5 (avec body)
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public IActionResult UpdateBottle(
            [FromRoute] int id,
            [FromBody] Bottle bottle)
        {
            if (id <= 0 || id != bottle.BottleId)
                return BadRequest();

            // mise à jour
            context.Bottles.Update(bottle);
            context.SaveChanges();

            return NoContent();
        }

        // DELETE : /class/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public IActionResult RemoveBottle([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var bottle = context.Bottles.Find(id);

            if (bottle == null)
                return NotFound();

            context.Bottles.Remove(bottle);
            context.SaveChanges();

            return Ok(bottle);
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
