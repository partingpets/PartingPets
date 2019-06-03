using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : SecureControllerBase
    {
        readonly UsersRepository _repo;

        public UsersController(UsersRepository repo)
        {
            _repo = repo;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<User> GetAllUsers()
        {
            var allUsers = _repo.GetAllUsers();
            return Ok(allUsers);
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<User> Get(string id)
        {
            var selectedUser = _repo.GetUserById(id);
            return Ok(selectedUser);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
