using Dal.IRepositories;
using DomainModel;
using DomainModel.DTO;
using DomainModel.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    
    public class CellarCategoryController : ControllerBase
    {
        private readonly ICellarCategoryRepository _cellarCategoryRepository;

        public CellarCategoryController(ICellarCategoryRepository cellarCategoryRepository) // Dependency injection
        {
            _cellarCategoryRepository = cellarCategoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarCategoryDto>))]
        [Authorize]
        public async Task<IActionResult> GetCellarCategories()
        {
            var categories = await _cellarCategoryRepository.GetAllCellarCategoriesAsync();

            var categoryDtos = categories.Select(c => new CellarCategoryDto
            {
                CellarCategoryId = c.CellarCategoryId,
                CategoryName = c.CategoryName
            }).ToList();

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarCategoryDto))]
        [Authorize]
        public async Task<IActionResult> GetCellarCategory([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            var categoryDto = new CellarCategoryDto
            {
                CellarCategoryId = category.CellarCategoryId,
                CategoryName = category.CategoryName
            };

            return Ok(categoryDto);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarCategoryDto))]
        public async Task<IActionResult> AddCellarCategory([FromBody] CreateCellarCategoryDto createCellarCategoryDto)
        {
            if (createCellarCategoryDto == null)
                return BadRequest();

            var cellarCategory = new CellarCategory
            {
                CategoryName = createCellarCategoryDto.CategoryName
            };

            await _cellarCategoryRepository.AddCellarCategoryAsync(cellarCategory);

            var categoryDto = new CellarCategoryDto
            {
                CellarCategoryId = cellarCategory.CellarCategoryId,
                CategoryName = cellarCategory.CategoryName
            };

            return Created($"cellar/{categoryDto.CellarCategoryId}", categoryDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateCellarCategory([FromRoute] int id, [FromBody] UpdateCellarCategoryDto updateCellarCategoryDto)
        {
            if (id <= 0 || updateCellarCategoryDto == null)
                return BadRequest();

            var existingCategory = await _cellarCategoryRepository.GetCellarCategoryByIdAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.CategoryName = updateCellarCategoryDto.CategoryName;

            await _cellarCategoryRepository.UpdateCellarCategoryAsync(existingCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(CellarCategoryDto))]
        public async Task<IActionResult> RemoveCellarCategory([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            await _cellarCategoryRepository.RemoveCellarCategoryAsync(category);

            var categoryDto = new CellarCategoryDto
            {
                CellarCategoryId = category.CellarCategoryId,
                CategoryName = category.CategoryName
            };

            return Ok(categoryDto);
        }
    }
}
