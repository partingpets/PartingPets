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
    }
}
