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
                var newOrder = db.QueryFirstOrDefault<Orders>(@"");
                return;
            }
        }
    }
}
