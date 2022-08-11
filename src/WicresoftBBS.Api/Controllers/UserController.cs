using Microsoft.AspNetCore.Mvc;
using WicresoftBBS.Api.Models;
using WicresoftBBS.Api.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WicresoftBBS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IBBSRepo _BBSRepo;
        public UserController(IBBSRepo BBSRepo)
        {
            _BBSRepo = BBSRepo;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _BBSRepo.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _BBSRepo.GetUser(id);
        }
    }
}
