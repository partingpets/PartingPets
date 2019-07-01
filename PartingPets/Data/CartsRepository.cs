﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PartingPets.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Data
{
    public class CartsRepository
    {
        readonly string _connectionString;

        public CartsRepository(IOptions<DbConfiguration> dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
        }

        public List<ShoppingCartAll> GetAllCarts()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var getAllCartsQuery = @"
                        SELECT
                            p.Id AS Id,
                            p.IsDeleted,
                            u.FirstName + ' ' + u.LastName AS UserName,
                            u.id AS UserId,
                            p.Name,
                            p.Description,
                            p.ImageUrl,
                            sc.Quantity,
                            p.UnitPrice
                        FROM [ShoppingCart] sc
                        JOIN [Products] p ON sc.ProductID = p.Id
                        JOIN [User] u ON sc.UserID = u.Id";

                var allCarts = db.Query<ShoppingCartAll>(getAllCartsQuery).ToList();

                if (allCarts != null)
                {
                    return allCarts;
                }
            }
            throw new Exception("No shopping carts found");
        }

        public IEnumerable<ShoppingCart> GetShoppingCartByUserId(int userId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var getCartsByUidQuery = @"
                        SELECT
                            sc.Id AS CartId,
                            p.Id AS ProductId,
                            p.IsDeleted,
                            p.Name,
                            p.Description,
                            p.ImageUrl,
                            sc.Quantity,
                            p.UnitPrice
                    FROM [ShoppingCart] sc
                    JOIN [Products] p ON sc.ProductID = p.Id
                    JOIN [User] u ON sc.UserID = u.Id
                    WHERE sc.UserID = @userId";

                var selectedCart = db.Query<ShoppingCart>(getCartsByUidQuery, new { userId });

                if (selectedCart != null)
                {
                    return selectedCart;
                }
            }
            throw new Exception("Shopping Cart not found");
        }

        public void DeleteShoppingCartItem(int itemId)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var deleteCartIytemQuery = @"
                        DELETE
                        FROM [ShoppingCart]
                        WHERE Id = @itemId";

                var rowsAffected = db.Execute(deleteCartIytemQuery, new { itemId });

                if (rowsAffected != 1)
                {
                    throw new Exception("Error deleteing the cart item");
                }
            }
        }
    }
}
