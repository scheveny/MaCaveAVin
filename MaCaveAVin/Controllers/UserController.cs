using Dal;
using DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaCaveAVin.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CellarContext _context;

        public UserController(CellarContext context)
        {
            _context = context;
        }

        // GET: /User
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        // GET: /User/5
        [HttpGet("{id}")]
        public IActionResult GetUser([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("id incorrect");

            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: /Student
        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            return Created($"student/{user.UserId}", user);
        }

        // PUT: /Student/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromRoute] int id, [FromBody] User user)
        {
            if (id != user.UserId || user == null)
            {
                return BadRequest();
            }

            var existingUser = _context.Users.Find(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: /Student/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute] int id)
        {
            var student = _context.Users.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Users.Remove(student);
            _context.SaveChanges();

            return NoContent();
        }
    }
}

