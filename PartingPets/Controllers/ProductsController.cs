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
    public class ProductsController : SecureControllerBase

    {
        readonly ProductsRepository _productRepository;
        readonly ProductRequestValidator _validator;

        public ProductsController()
        {
            _validator = new ProductRequestValidator();
            _productRepository = new ProductsRepository();
        }

        // ----- GET ALL PRODUCTS ---- //
        // GET: api/Products
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            var products = _productRepository.GetProducts();

            return Ok(products);
        }

        // ----- GET ALL PRODUCT CATEGORIES ---- //
        // GET: api/Products/Categories
        [HttpGet("categories")]
        public ActionResult GetProductCategories()
        {
            var productCategories = _productRepository.GetProductCategories();

            return Ok(productCategories);
        }

        // ----- GET ALL PRODUCTS BY CATEGORY ID ---- //
        // GET: api/Products/Category/3
        [HttpGet("category/{categoryId}")]
        public ActionResult GetProductsByCategory(int categoryId)
        {
            var selectedCategory = _productRepository.GetProductsByCategory(categoryId);

            return Ok(selectedCategory);
        }

        // ----- GET ALL PRODUCTS BY PARTNER ID ---- //
        // GET: api/Products/Partners/3
        [HttpGet("partner/{partnerId}")]
        public ActionResult GetProductsByPartner(int partnerId)
        {
            var selectedPartner = _productRepository.GetProductsByPartner(partnerId);

            return Ok(selectedPartner);
        }

        // ----- GET PRODUCT BY ID ---- //
        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {
            return Ok(_productRepository.GetProductById(id));
        }


        // ----- ADD NEW PRODUCT ---- //
        // POST: api/Products
        [HttpPost]
        public ActionResult AddProduct(CreateProductRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "Parting Pets Requests You Fill All Necessary Fields." });
            }

            var newProduct = _productRepository.AddProduct(createRequest);

            return Created($"api/products/{newProduct.ID}", newProduct);
        }


        // ----- UPDATE PRODUCT ---- //
        // PUT: api/Products/Update/5
        [HttpPut("update/{id}")]
        public ActionResult UpdateProduct(int id, Product productToUpdate)
        {
            if (id != productToUpdate.ID)
            {
                return BadRequest(new { Error = "Parting Pets Needs A Bit More Product Information." });
            }
            var updatedProduct = _productRepository.UpdateProduct(productToUpdate);

            return Ok(updatedProduct);
        }



        // ----- DELETE PRODUCT---- //
        // DELETE: api/Products/Delete/5
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);

            return Ok("Your Parting Pets Product Has Been Deleted. ;) Wink Wink.");
        }
    }
}
