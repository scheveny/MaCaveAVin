using Dal;
using DomainModel;
using Dal.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BottleController : ControllerBase
    {
        private readonly CellarContext _context;
        private readonly IPositionService _positionService;
        private readonly IPeakService _peakService;
        private readonly UserManager<AppUser> _userManager;

        public BottleController(CellarContext context, IPositionService positionService, IPeakService bottleService, UserManager<AppUser> userManager) // Dependency injection
        {
            _context = context;
            _positionService = positionService;
            _peakService = bottleService;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Bottle>> GetBottles()
        {
            var bottles = _context.Bottles.ToList();
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

            var bottle = _context.Bottles.Find(id);

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

            var bottles = _context.Bottles.Where(b => b.CellarId == cellarId).ToList();

            if (!bottles.Any())
                return NotFound();

            return Ok(bottles);
        }

        // Nouvelle méthode pour récupérer toutes les bouteilles d'un utilisateur
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Bottle>>> GetAllUserBottles(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            var userCellars = _context.Cellars.Where(c => c.User.Id == userId).Select(c => c.CellarId).ToList();
            if (!userCellars.Any())
                return NotFound();

            var bottles = _context.Bottles.Where(b => userCellars.Contains(b.CellarId)).ToList();
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
            var position = _positionService.FindFirstAvailablePosition(bottle.CellarId);
            if (position == null)
                return BadRequest("No available position found.");

            bottle.DrawerNb = position.Value.Item1;
            bottle.StackInDrawerNb = position.Value.Item2;

            _context.Bottles.Add(bottle);
            _context.SaveChanges();

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

            _context.Bottles.Update(bottle);
            _context.SaveChanges();

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

            var bottle = _context.Bottles.Find(id);

            if (bottle == null)
                return NotFound();

            _context.Bottles.Remove(bottle);
            _context.SaveChanges();

            return Ok(bottle);
        }
    }
}
