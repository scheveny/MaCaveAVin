using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using Dal.IServices;
using Dal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        private readonly CellarContext context;
        private readonly IPositionService positionService;
        private readonly IPeakService _peakService;

        public BottleController(CellarContext context, IPositionService positionService, IPeakService bottleService) // Dependency injection
        {
            this.context = context;
            this.positionService = positionService;
            this._peakService = bottleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Bottle>> GetBottles()
        {
            var bottles = context.Bottles.ToList();
            return Ok(bottles);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Bottle> GetBottle(int id)
        {
            if (id <= 0)
                return BadRequest();

            var bottle = context.Bottles.Find(id);

            if (bottle == null)
                return NotFound();

            return Ok(bottle);
        }

        [HttpGet("cellar/{cellarId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Bottle>> GetBottlesFromCellar(int cellarId)
        {
            if (cellarId <= 0)
                return BadRequest();

            var bottles = context.Bottles.Where(b => b.CellarId == cellarId).ToList();

            if (!bottles.Any())
                return NotFound();

            return Ok(bottles);
        }

        // Nouvelle méthode pour récupérer toutes les bouteilles d'un utilisateur
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Bottle>> GetAllUserBottles(int userId)
        {
            if (userId <= 0)
                return BadRequest();

            var userCellars = context.Cellars.Where(c => c.User.UserId == userId).Select(c => c.CellarId).ToList();
            if (!userCellars.Any())
                return NotFound();

            var bottles = context.Bottles.Where(b => userCellars.Contains(b.CellarId)).ToList();
            if (!bottles.Any())
                return NotFound();

            return Ok(bottles);
        }





        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Bottle> AddBottle([FromBody] Bottle bottle)
        {
            _peakService.CalculateIdealPeak(bottle);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Find the first available position using the position service
            var position = positionService.FindFirstAvailablePosition(bottle.CellarId);
            if (position == null)
                return BadRequest("No available position found.");

            bottle.DrawerNb = position.Value.Item1;
            bottle.StackInDrawerNb = position.Value.Item2;

            context.Bottles.Add(bottle);
            context.SaveChanges();

            return CreatedAtAction(nameof(GetBottle), new { id = bottle.BottleId }, bottle);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateBottle(int id, [FromBody] Bottle bottle)
        {
            if (id <= 0 || id != bottle.BottleId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            context.Bottles.Update(bottle);
            context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Bottle> RemoveBottle(int id)
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

        [CustomExceptionFilter]
        [HttpGet("customerror")]
        public IActionResult CustomError()
        {
            throw new NotImplementedException("Method not implemented");
        }
    }
}
