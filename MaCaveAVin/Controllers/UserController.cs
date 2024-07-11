using Dal.Interfaces;
using Dal.IRepositories;
using DomainModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAgeValidationService _ageValidationService;

        public UserController(IUserRepository userRepository, IAgeValidationService ageValidationService)
        {
            _userRepository = userRepository;
            _ageValidationService = ageValidationService;
        }

        // GET: /User
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces(typeof(List<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAllUsers();
            return Ok(users);
        }

        // GET: /User/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(User))]
        public IActionResult GetUser([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("id incorrect");

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /User
        [HttpPost]
        [ProducesResponseType(201)]
        [Produces(typeof(User))]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var ageValidationResult = _ageValidationService.ValidateAge(user.Birthday);
            if (ageValidationResult != ValidationResult.Success)
            {
                return BadRequest(ageValidationResult.ErrorMessage);
            }

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context, results, true);

            if (!isValid)
            {
                return BadRequest(results);
            }

            _userRepository.AddUser(user);

            return Created($"user/{user.UserId}", user);
        }

        // PUT: /User/5
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [Produces(typeof(User))]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            if (id != user.UserId || user == null)
            {
                return BadRequest();
            }

            var existingUser = _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            var ageValidationResult = _ageValidationService.ValidateAge(user.Birthday);
            if (ageValidationResult != ValidationResult.Success)
            {
                return BadRequest(ageValidationResult.ErrorMessage);
            }

            var context = new ValidationContext(user);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, context, results, true);

            if (!isValid)
            {
                return BadRequest(results);
            }

            _userRepository.UpdateUser(user);

            return NoContent();
        }

        // DELETE: /User/5
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [Produces(typeof(User))]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user);

            return NoContent();
        }
    }
}
