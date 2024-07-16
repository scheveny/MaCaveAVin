using Microsoft.AspNetCore.Mvc;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DomainModel.DTO.cellar;
using DomainModel.DTO.User;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize]
    public class CellarController : ControllerBase
    {
        private readonly ICellarRepository _cellarRepository;
        private readonly UserManager<AppUser> _userManager;

        public CellarController(ICellarRepository cellarRepository, UserManager<AppUser> userManager) // Dependency injection
        {
            _cellarRepository = cellarRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> GetCellars()
        {
            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID

            var cellars = await _cellarRepository.GetCellarsByUserIdAsync(userId);
            return Ok(cellars);
        }

        [HttpGet("cellarbyname")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(List<Cellar>))]
        public async Task<IActionResult> SearchCellars([FromQuery] string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest("Name parameter is required.");

            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var cellars = await _cellarRepository.SearchCellarsByNameAsync(userId, name);

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
            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var cellars = await _cellarRepository.GetCellarsByModelAsync(userId, modelId);

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
            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var cellars = await _cellarRepository.GetCellarsByCategoryAsync(userId, categoryId);

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

            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Invalid UserId.");

            var cellarCategory = await _cellarRepository.GetCellarCategoryByIdAsync(cellarDto.CellarCategoryId);
            if (cellarCategory == null)
                return BadRequest("Invalid CellarCategoryId.");

            var cellarModel = await _cellarRepository.GetCellarModelByIdAsync(cellarDto.CellarModelId);
            if (cellarModel == null)
                return BadRequest("Invalid CellarModelId.");

            var cellar = new Cellar
            {
                CellarName = cellarDto.CellarName,
                NbRow = cellarDto.NbRow,
                NbStackRow = cellarDto.NbStackRow,
                UserId = user.Id,
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
                    UserId = user.Id,
                },
                CellarCategoryId = cellar.CellarCategoryId,
                CellarModelId = cellar.CellarModelId
            };

            return Created($"cellar/{result.CellarId}", result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellar([FromRoute] int id, [FromBody] UpdateCellarDto updateCellarDto)
        {
            if (id <= 0)
                return BadRequest();

            // Retrieve the existing cellar
            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var existingCellar = await _cellarRepository.GetCellarByIdAsync(id);
            if (existingCellar == null || existingCellar.UserId != userId)
                return NotFound();

            // Update the properties
            existingCellar.CellarName = updateCellarDto.CellarName;
            existingCellar.NbRow = updateCellarDto.NbRow;
            existingCellar.NbStackRow = updateCellarDto.NbStackRow;
            existingCellar.CellarCategoryId = updateCellarDto.CellarCategoryId;
            existingCellar.CellarModelId = updateCellarDto.CellarModelId;

            // Verify if the provided CellarCategoryId and CellarModelId are valid
            var cellarCategory = await _cellarRepository.GetCellarCategoryByIdAsync(updateCellarDto.CellarCategoryId);
            if (cellarCategory == null)
                return BadRequest("Invalid CellarCategoryId.");

            var cellarModel = await _cellarRepository.GetCellarModelByIdAsync(updateCellarDto.CellarModelId);
            if (cellarModel == null)
                return BadRequest("Invalid CellarModelId.");

            // Save the changes
            await _cellarRepository.UpdateCellarAsync(existingCellar);

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

            var userId = _userManager.GetUserId(User);  // Get the currently authenticated user's ID
            var cellar = await _cellarRepository.GetCellarByIdAsync(id);

            if (cellar == null || cellar.UserId != userId)
                return NotFound();

            await _cellarRepository.RemoveCellarAsync(cellar);

            return Ok(cellar);
        }

        [CustomExceptionFilter]
        [HttpGet("customerror")]
        public IActionResult CustomError()
        {
            throw new NotImplementedException("Méthode non implementé");
        }
    }
}
