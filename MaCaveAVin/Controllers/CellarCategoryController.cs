using Dal.IRepositories;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CellarCategoryController : ControllerBase
    {
        private readonly ICellarCategoryRepository _cellarCategoryRepository;
        private readonly UserManager<AppUser> _userManager;

        public CellarCategoryController(ICellarCategoryRepository cellarCategoryRepository, UserManager<AppUser> userManager) // Dependency injection
        {
            _cellarCategoryRepository = cellarCategoryRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarCategory>))]
        public async Task<IActionResult> GetCellarCategories()
        {
            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var categories = await _cellarCategoryRepository.GetCellarCategoriesByUserIdAsync(userId);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarCategory))]
        public async Task<IActionResult> GetCellarCategory([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAndUserIdAsync(id, userId);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarCategory))]
        public async Task<IActionResult> AddCellarCategory([FromBody] CellarCategory cellarCategory)
        {
            if (cellarCategory == null)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            cellarCategory.UserId = userId; // Assign the current user's ID to the cellarCategory

            await _cellarCategoryRepository.AddCellarCategoryAsync(cellarCategory);

            return Created($"cellar/{cellarCategory.CellarCategoryId}", cellarCategory);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellarCategory([FromRoute] int id, [FromBody] CellarCategory cellarCategory)
        {
            if (id <= 0 || id != cellarCategory.CellarCategoryId)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID

            var existingCategory = await _cellarCategoryRepository.GetCellarCategoryByIdAndUserIdAsync(id, userId);
            if (existingCategory == null)
                return NotFound();

            // Ensure the updated category belongs to the current user
            if (existingCategory.UserId != userId)
                return Forbid();

            existingCategory.CategoryName = cellarCategory.CategoryName;

            await _cellarCategoryRepository.UpdateCellarCategoryAsync(existingCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarCategory))]
        public async Task<IActionResult> RemoveCellarCategory([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAndUserIdAsync(id, userId);

            if (category == null)
                return NotFound();

            await _cellarCategoryRepository.RemoveCellarCategoryAsync(category);

            return Ok(category);
        }
    }
}
