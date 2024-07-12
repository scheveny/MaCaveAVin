using Dal;
using DomainModel;
using MaCaveAVin.Filters;
using Dal.IServices;
using Dal.IRepositories;
using Dal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DomainModel.DTOs.Bottle;


namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        private readonly CellarContext context;
        private readonly IPositionService positionService;
        private readonly IPeakService _peakService;
        private readonly IBottleRepository bottleRepository;

        public BottleController(CellarContext context, IBottleRepository bottleRepository, IPositionService positionService, IPeakService bottleService) // Dependency injection
        {
            this.context = context;
            this.positionService = positionService;
            this._peakService = bottleService;
            this.bottleRepository = bottleRepository;
        }


        /* GetBottles without DTO
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Bottle>> GetBottles()
        {
            var bottles = context.Bottles.ToList();
            return Ok(bottles);
        }
        */

        // GetBottles with DTO
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BottleDto>>> GetBottles()
        {
            List<BottleDto> bottles = new List<BottleDto>();

            foreach (Bottle bottle in await bottleRepository.GetAllAsync())
            {
                bottles
                    .Add(new BottleDto
                    {
                        BottleId = bottle.BottleId,
                        BottleName = bottle.BottleName
                    });
            }

            return Ok(bottles);
        }

        /* GetBottle without DTO
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BottleDto>> GetBottle(int id)
        {

            if (id <= 0)
                return BadRequest();

            var bottle = context.Bottles.Find(id);

            if (bottle == null)
                return NotFound();

            return Ok(bottle);
        }
        */

        //GetBottle with DTO
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BottleDto>> GetBottle(int id)
        {
            if (id <= 0)
                return BadRequest();

            var bottle = await bottleRepository.GetByIdAsync(id);

            if (bottle == null)
                return NotFound();

            var bottleDto = new BottleDto
            {
                BottleId = bottle.BottleId,
                BottleName = bottle.BottleName
            };

            return Ok(bottleDto);
        }


        /*GetBottlesFromCellar without DTO
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
        */

        // GetBottlesFromCellar with DTO
        public async Task<ActionResult<IEnumerable<BottleDto>>> GetBottlesFromCellar(int cellarId)
        {
            if (cellarId <= 0)
                return BadRequest();

            var bottles = await bottleRepository.GetBottlesByCellarIdAsync(cellarId);

            if (!bottles.Any())
                return NotFound();

            var bottleDtos = bottles.Select(b => new BottleDto
            {
                BottleId = b.BottleId,
                BottleName = b.BottleName
            }).ToList();

            return Ok(bottleDtos);
        }



        /* GetAllUserBottles without DTO
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
        */

        // GetAllUserBottles without DTO
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BottleDto>>> GetAllUserBottles(int userId)
        {
            if (userId <= 0)
                return BadRequest();

            var bottles = await bottleRepository.GetBottlesByUserIdAsync(userId);

            if (!bottles.Any())
                return NotFound();

            var bottleDtos = bottles.Select(b => new BottleDto
            {
                BottleId = b.BottleId,
                BottleName = b.BottleName
            }).ToList();

            return Ok(bottleDtos);
        }

        /* AddBottle without DTO
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
        */

        // AddBottle with DTO
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BottleDto>> AddBottle([FromBody] Bottle bottle)
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

            var newBottle = await bottleRepository.PostAsync(bottle);

            var bottleDto = new BottleDto
            {
                BottleId = newBottle.BottleId,
                BottleName = newBottle.BottleName
            };

            return CreatedAtAction(nameof(GetBottle), new { id = newBottle.BottleId }, bottleDto);
        }


        /* UpdateBottle without DTO
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
        */

        // UpdateBottle with DTO
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateBottle(int id, [FromBody] Bottle bottle)
        {
            if (id <= 0 || id != bottle.BottleId)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedBottle = await bottleRepository.UpdateAsync(bottle);

            return NoContent();
        }

        /* RemoveBottle without DTO
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
        */

        // RemoveBottle with DTO
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BottleDto>> RemoveBottle(int id)
        {
            if (id <= 0)
                return BadRequest();

            var bottle = await bottleRepository.RemoveAsync(id);

            if (bottle == null)
                return NotFound();

            var bottleDto = new BottleDto
            {
                BottleId = bottle.BottleId,
                BottleName = bottle.BottleName
            };

            return Ok(bottleDto);
        }

        [CustomExceptionFilter]
        [HttpGet("customerror")]
        public IActionResult CustomError()
        {
            throw new NotImplementedException("Method not implemented");
        }
    }
}
