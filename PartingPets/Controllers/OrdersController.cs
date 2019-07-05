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
        public ActionResult GetOrdersById(int id)
        {
            var userOrders = _ordersRepo.getUsersOrders(id);

            foreach (var order in userOrders)
            {
                decimal lineTotal = 0;
                decimal subTotal = 0;
                decimal taxRate = Convert.ToDecimal(0.095);

                foreach (var orderline in order.OrderItems)
                {
                    lineTotal = orderline.Quantity * orderline.UnitPrice;
                    orderline.LineTotal = lineTotal;
                    subTotal = subTotal + lineTotal;
                }

                order.Subtotal = subTotal;
                order.Tax = Decimal.Parse((subTotal * taxRate).ToString("0.00"));
                order.Total = order.Subtotal + order.Tax;
            }
            return Ok(userOrders);

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
            // For each object in the array of objects called OrderLines
            // Each object has productId and Quantity
            foreach (var orderItem in newOrderObj.OrderLines)
            {
                // Gets all the information for a single product since an ID is passed in
                var productItem = _productsRepo.GetProductById(orderItem.ProductId);
                // For the OrdersLine table 
                orderItem.OrdersId = newOrder.Id;
                // Also for the OrdersLine table
                orderItem.UnitPrice = productItem.UnitPrice;
                // Now that the orderItem has all the required 4 fields we're passing it into a method that inserts it into the OrdersLine table
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
