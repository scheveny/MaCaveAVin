using Dal.Interfaces;
using Dal.IRepositories;
using DomainModel;
using MaCaveAVin.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CellarCategoryController : ControllerBase
    {
        private readonly ICellarCategoryRepository _cellarCategoryRepository;

        public CellarCategoryController(ICellarCategoryRepository cellarCategoryRepository) // Injection de dépendances
        {
            _cellarCategoryRepository = cellarCategoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarCategory>))]
        public async Task<IActionResult> GetCellarCategories()
        {
            var categories = await _cellarCategoryRepository.GetAllCellarCategoriesAsync();
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

            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAsync(id);

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

            await _cellarCategoryRepository.UpdateCellarCategoryAsync(cellarCategory);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(Cellar))]
        public async Task<IActionResult> RemoveCellarCategory([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest();

            var category = await _cellarCategoryRepository.GetCellarCategoryByIdAsync(id);

            if (category == null)
                return NotFound();

            await _cellarCategoryRepository.RemoveCellarCategoryAsync(category);

            return Ok(category);
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
