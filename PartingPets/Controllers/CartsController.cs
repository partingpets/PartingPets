using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : SecureControllerBase
    {
        readonly CartsRepository _repo;
        
        public CartsController(CartsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Carts
        [HttpGet]
        public ActionResult<ShoppingCart> GetAllCarts()
        {
            var allCarts = _repo.GetAllCarts();
            return Ok(allCarts);
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public ActionResult<ShoppingCart> GetCartByUserId(int id)
        {
            try
            {
                var selectedCart = _repo.GetShoppingCartByUserId(id);
                return Ok(selectedCart);
            }
            catch (System.Exception)
            {
                return NotFound();
            }
        }

        // POST: api/Carts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Carts/5
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
