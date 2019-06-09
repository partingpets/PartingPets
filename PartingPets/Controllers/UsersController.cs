using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;
using PartingPets.Validators;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : SecureControllerBase
    {
        readonly UsersRepository _repo;
        readonly UserRequestValidator _validator;

        public UsersController(UsersRepository repo)
        {
            _repo = repo;
            _validator = new UserRequestValidator();

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

            try
            {
                var selectedUser = _repo.GetUserById(id);
                return Ok(selectedUser);
            }
            catch(System.Exception)
            {

                return NotFound();
            }

        }

        // POST: api/User
        [HttpPost]
        public ActionResult<CreateUserRequest> CreateUser([FromBody] CreateUserRequest newUserObject)
        {
            newUserObject.FireBaseUid = UserId;
            if(!_validator.Validate(newUserObject))
            {
                return BadRequest(new { error = "User object validation failed " });
            }

            var newUser = _repo.AddNewUser(newUserObject);

            return Ok(newUser);
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
