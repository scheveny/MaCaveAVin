using Dal;
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
    }
}
