using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class SecureControllerBase : ControllerBase
    {
        protected string UserId => User.FindFirst(x => x.Type == "user_id").Value;
    }
}