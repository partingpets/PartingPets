using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartingPets.Models;
using PartingPets.Data;
using System.Data.SqlClient;
using Dapper;

namespace PartingPets.Data
{
    public class ProductsRepository
    {
        const string ConnectionString = "Server = localhost; Database = PartingPets; Trusted_Connection = True;";

        // Add Product Command //

        public Product AddProduct(string name, decimal unitPrice, int categoryId, string description, bool isOnSale)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newProduct = db.QueryFirstOrDefault<Product>(@"
                    Insert into products (name, unitPrice, categoryId, description, isOnSale)
                    Output inserted.*
                    Values(@name,@unitPrice,@categoryId, @description,@isOnSale)",
                    new { name, unitPrice, categoryId, description, isOnSale });

                if (newProduct != null)
                {
                    return newProduct;
                }
            }

            throw new Exception("No Parting Pets Product Created");
        }


        // Get All Products Command //

        public IEnumerable<Product> GetProducts()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var products = db.Query<Product>("select id, name, unitPrice, description, isOnSale from products").ToList();


                return products;
            }
        }

        // Get Single Product By ID //

        public IEnumerable<Product> GetProductById(int iD)
        {
            using (var db = new SqlConnection(ConnectionString))
            {


                var selectedProduct = db.Query<Product>("select id, name, description, isOnSale from products where Id = @id", new { iD });


                return selectedProduct;
            }
        }

        // Delete Product Command //

        public void DeleteProduct(int ID)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = ID };
                var deleteQuery = "Delete from Products where ID = @id";

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("there was an error. you're parting pets product wasn't deleted.");
                }
            }
        }

        // Update Product Command //

        public Product UpdateProduct(Product productToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updateQuery = @"Update Products
                                Set Name = @name,
                                CategoryId = @categoryId,
                                UnitPrice = @unitPrice,
                                Description = @description,
                                IsOnSale = @isOnSale
                            Where ID = @id";

                var rowsAffected = db.Execute(updateQuery, productToUpdate);

                if (rowsAffected == 1)
                {
                    return productToUpdate;
                }

                throw new Exception("There Was An Error. Your Parting Pets Product Wasn't Updated");
            }

        }
    }
}


