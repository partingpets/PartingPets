using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;
using PartingPets.Utils;
using PartingPets.Validators;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : SecureControllerBase
    {
        readonly UsersRepository _repo;
        readonly UserRequestValidator _validator;
        readonly EditUserValidator _editUserValidator;
        readonly UserAuthorization _userAuth;

        public UsersController(UsersRepository repo)
        {
            _repo = repo;
            _validator = new UserRequestValidator();
            _editUserValidator = new EditUserValidator();
            _userAuth = new UserAuthorization();

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
                if (id.Length < 5)
                {
                    var selectedUser = _repo.GetUserByUserId(id);
                    return Ok(selectedUser);
                }
                else
                {
                    var selectedUser = _repo.GetUserById(id);
                    return Ok(selectedUser);
                }
                
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
        public ActionResult<EditUserRequest> UpdateUser(int id, [FromBody] EditUserRequest updatedUserObj)
        {
            var jwtFirebaseId = UserId;
            if(updatedUserObj.FireBaseUid != jwtFirebaseId && updatedUserObj.IsAdmin == false)
            {
                // return 401 as the User they are passing in is not the same as the one making the request
                return Unauthorized();
            }

            if(!_editUserValidator.Validate(updatedUserObj))
            {
                return BadRequest(new { error = "User object validation failed " });
            }
            //return null;
            var updatedUser = _repo.UpdateUser(updatedUserObj);

            return Ok(updatedUser);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var jwtFirebaseId = UserId;

            // Check if the user is modifying thier own account or if they are Admin
            var authed = _userAuth.AuthorizeUserByUid(id, jwtFirebaseId, _repo);
            if(!authed)
            {
                return Unauthorized(new { error = "User not Admin" });
            }
            else
            {
                try
                {
                    _repo.DeleteUser(id);
                }
                catch(System.Exception e)
                {
                    throw e;
                }
            }
            return NoContent();
        }
    }
}
