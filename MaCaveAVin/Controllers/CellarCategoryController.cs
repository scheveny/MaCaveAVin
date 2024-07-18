
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DomainModel.DTO.cellarCat;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    [Authorize]
    public class CellarCategoryController : ControllerBase
    {
        private readonly ICellarCategoryRepository _cellarCategoryRepository;
        //private readonly UserManager<AppUser> _userManager;

        public CellarCategoryController(ICellarCategoryRepository cellarCategoryRepository) // Dependency injection
        {
            _cellarCategoryRepository = cellarCategoryRepository;
            //_userManager = userManager;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<CellarCategoryDto>))]
        [AllowAnonymous]
        public async Task<IActionResult> GetCellarCategories()
        {
            List<CellarCategoryDto> cellarCategories = new List<CellarCategoryDto>();

            foreach (CellarCategory cellarCategory in await _cellarCategoryRepository.GetCellarCategoriesAsync())
            {
                cellarCategories
                    .Add(new CellarCategoryDto
                    {
                        CellarCategoryId = cellarCategory.CellarCategoryId,
                        CategoryName = cellarCategory.CategoryName

                    });
            }
            return Ok(cellarCategories);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(CellarCategoryDto))]
        public async Task<IActionResult> AddCellarCategory([FromBody] CreateCellarCategoryDto createCellarCategoryDto)
        {
            if (createCellarCategoryDto == null)
                return BadRequest();

            //var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID

            var cellarCategory = new CellarCategory
            {
                CategoryName = createCellarCategoryDto.CategoryName,
                //UserId = userId
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

            //var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID

            var existingCategory = await _cellarCategoryRepository.GetCellarCategoryAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.CategoryName = updateCellarCategoryDto.CategoryName;

            await _cellarCategoryRepository.UpdateCellarCategoryAsync(existingCategory);

            return NoContent();
        }

        //[HttpDelete("{id}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(200)]
        //[Produces(typeof(CellarCategoryDto))]
        //public async Task<IActionResult> RemoveCellarCategory([FromRoute] int id)
        //{
        //    if (id <= 0)
        //        return BadRequest();

        //    //var userId = _userManager.GetUserId(User); // Get the currently authenticated user's ID
        //    var category = await _cellarCategoryRepository.GetCellarCategoryByIdAndUserIdAsync(id, userId);

        //    if (category == null)
        //        return NotFound();

        //    await _cellarCategoryRepository.RemoveCellarCategoryAsync(category);

        //    var categoryDto = new CellarCategoryDto
        //    {
        //        CellarCategoryId = category.CellarCategoryId,
        //        CategoryName = category.CategoryName
        //    };

        //    return Ok(categoryDto);
        //}
    }
}
