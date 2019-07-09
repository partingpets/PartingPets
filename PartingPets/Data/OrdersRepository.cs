using Dapper;
using PartingPets.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Data
{
    public class OrdersRepository
    {
        const string ConnectionString = "Server=localhost;Database=PartingPets;Trusted_Connection=True;";

        public Orders CreateOrder(Orders newOrderObj)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newOrder = db.QueryFirstOrDefault<Orders>(@"
                    Insert into orders(userid, paymenttypeid, purchasedate)
                    Output inserted.*
                    Values(@UserId, @PaymentTypeId, GETUTCDATE())",
                    new
                    {
                        newOrderObj.UserId,
                        newOrderObj.PaymentTypeId, 
                        newOrderObj.PurchaseDate,
                    });

                if (newOrder != null)
                {
                    return newOrder;
                }
            }
            throw new Exception("Unfortunatley, a new order was not created");
        }

        public OrderLines CreateOrderLines(OrderLines OrderItem)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
        var newOrderLine = db.QueryFirstOrDefault<OrderLines>(@"
                    Insert into ordersline(OrdersId, ProductId, Quantity, UnitPrice)
                    Output inserted.*
                    Values(@OrdersId, @ProductId, @Quantity, @UnitPrice)",
                    new
                    {
                        OrderItem.OrdersId,
                        OrderItem.ProductId,
                        OrderItem.Quantity,
                        OrderItem.UnitPrice
                    });

                if (newOrderLine != null)
                {
                    return newOrderLine;
                }
            }
            throw new Exception("Unfortunatley, a new order was not created");
        }

        public IEnumerable<userOrder> getAllUserOrders()
        {
            using (var db = new SqlConnection(ConnectionString))
            {

                var userOrderInfo = db.Query<userOrder>("select o.Id, o.PurchaseDate, u.FirstName, u.LastName, u.Street1, u.City, u.State, u.ZipCode, u.Email from Orders o join [User] u on o.UserID = u.Id").ToList();

                var userOrderLines = db.Query<OrderLines>("select ol.OrdersId, ol.Quantity, p.Name, ol.UnitPrice, p.Id from Orders o join OrdersLine ol on o.Id = ol.OrdersId join Products p on ol.ProductId = p.Id join [User] u on o.UserID = u.Id").ToList();

                foreach (var userInfo in userOrderInfo)
                {

                    var matchingOrderLines = userOrderLines.Where(x => x.OrdersId == userInfo.Id).ToList();
                    userInfo.OrderItems = matchingOrderLines;
                }

                return userOrderInfo; 
            }
        }

        public IEnumerable<userOrder> getUsersOrders(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "select o.Id, o.PurchaseDate, u.FirstName, u.LastName, u.Street1, u.City, u.State, u.ZipCode, u.Email from Orders o join [User] u on o.UserID = u.Id where o.UserID = @id";
                
                var userOrderInfo = db.Query<userOrder>(sql, new { id }).ToList();

                var userOrderLines = db.Query<OrderLines>("select ol.OrdersId, ol.Quantity, p.Name, ol.UnitPrice, p.Id from Orders o join OrdersLine ol on o.Id = ol.OrdersId join Products p on ol.ProductId = p.Id join [User] u on o.UserID = u.Id").ToList();

                foreach (var userInfo in userOrderInfo)
                {
                    
                    var matchingOrderLines = userOrderLines.Where(x => x.OrdersId == userInfo.Id).ToList();
                    userInfo.OrderItems = matchingOrderLines;
                }

                return userOrderInfo;
            }
        }

        public userOrder getUserOrderByOrderId(int orderid)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "select o.Id, o.PurchaseDate, u.FirstName, u.LastName, u.Street1, u.City, u.State, u.ZipCode, u.Email from Orders o join [User] u on o.UserID = u.Id where o.Id = @orderid";

                var userOrderInfo = db.QueryFirstOrDefault<userOrder>(sql, new { orderid });

                var userOrderLines = db.Query<OrderLines>("select ol.OrdersId, ol.Quantity, p.Name, ol.UnitPrice, p.Id from Orders o join OrdersLine ol on o.Id = ol.OrdersId join Products p on ol.ProductId = p.Id join [User] u on o.UserID = u.Id").ToList();

                // foreach (var userInfo in userOrderInfo)
                {

                    var matchingOrderLines = userOrderLines.Where(x => x.OrdersId == userOrderInfo.Id).ToList();
                    userOrderInfo.OrderItems = matchingOrderLines;
                }

                return userOrderInfo;
            }
        }

    }
}
