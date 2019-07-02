using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;
using PartingPets.Utils;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : SecureControllerBase
    {
        readonly CartsRepository _cartRepo;
        readonly UsersRepository _userRepo;
        readonly UserAuthorization _userAuth;

        public CartsController(CartsRepository repo, UsersRepository userRepo)
        {
            _cartRepo = repo;
            _userRepo = userRepo;
            _userAuth = new UserAuthorization();
        }

        // GET: api/Carts
        [HttpGet]
        public ActionResult<ShoppingCart> GetAllCarts()
        {
            var allCarts = _cartRepo.GetAllCarts();
            return Ok(allCarts);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public ActionResult<ShoppingCart> GetCartByUserId(int id)
        {
            try
            {
                var selectedCart = _cartRepo.GetShoppingCartByUserId(id);
                return Ok(selectedCart);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Carts
        [HttpPost]
        public ActionResult CreatCartItem([FromBody] CreateCartRequest cartItem)
        {
            var newCartItem = _cartRepo.AddCartItem(cartItem);

            return Ok(newCartItem);
            
        }

        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{userId}/{id}")]
        public ActionResult Delete(int userId, int id)
        {
            var jwtFirebaseId = UserId;

            var authed = _userAuth.AuthorizeUserByUid(userId, jwtFirebaseId, _userRepo);
            if (!authed)
            {
                return Unauthorized(new { error = "User not authorized to perform operation" });
            }
            else
            {
                try
                {
                    _cartRepo.DeleteShoppingCartItem(id);
                }
                catch (System.Exception e)
                {

                    throw e;
                }
            }
            return NoContent();
        }
    }
}
