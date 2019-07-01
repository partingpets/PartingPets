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
    public class OrdersController : SecureControllerBase
    {
        readonly OrdersRepository _ordersRepo;
        readonly ProductsRepository _productsRepo;

        public OrdersController(OrdersRepository ordersRepo, ProductsRepository productsRepo)
        {
            _ordersRepo = ordersRepo;
            _productsRepo = productsRepo;
        }

        // GET: api/Orders
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Orders
        [HttpPost]
        public ActionResult CreateOrder(Orders newOrderObj)
        {
        
            var newOrder = _ordersRepo.CreateOrder(newOrderObj);
            if (newOrder == null)
            {
                return NotFound();
            }

            foreach (var orderItem in newOrderObj.OrderLines)
            {
                var productItem = _productsRepo.GetProductById(orderItem.ProductId);
                orderItem.OrdersId = newOrder.Id;
                orderItem.UnitPrice = productItem.UnitPrice;
                var orderLineItem = _ordersRepo.CreateOrderLines(orderItem);
            }
 

            return Created($"api/orders/{newOrder.Id}", newOrder);
        }

        // PUT: api/Orders/5
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
