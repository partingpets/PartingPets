using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartingPets.Data;
using PartingPets.Models;
using PartingPets.Validators;

namespace PartingPets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase

    {
        readonly ProductsRepository _productRepository;
        readonly ProductRequestValidator _validator;

        public ProductsController()
        {
            _validator = new ProductRequestValidator();
            _productRepository = new ProductsRepository();
        }


        // GET: api/Products
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            var products = _productRepository.GetProducts();

            return Ok(products);
        }


        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            return Ok(_productRepository.GetProductById(id));
        }



        // POST: api/Products
        [HttpPost]
        public ActionResult AddProduct(CreateProductRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "We Need More Info For Your Product?" });
            }

            var newProduct = _productRepository.AddProduct(createRequest.Name, createRequest.UnitPrice, createRequest.CategoryId, createRequest.Description, createRequest.IsOnSale);

            return Created($"api/products/{newProduct.ID}", newProduct);
        }


        //// PUT: api/Products/Update/5
        [HttpPut("update/{id}")]
        public ActionResult UpdateProduct(int id, Product productToUpdate)
        {
            if (id != productToUpdate.ID)
            {
                return BadRequest(new { Error = "Parting Pets Needs A Bit More Product Information" });
            }
            var updatedProduct = _productRepository.UpdateProduct(productToUpdate);

            return Ok(updatedProduct);
        }




        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);

            return Ok("Your Parting Pets Product Has Been Deleted");
        }
    }
}
