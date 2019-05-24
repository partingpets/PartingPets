using Dapper;
using PartingPets.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Data
{
    public class PartnersRepository
    {
        const string ConnectionString = "Server=localhost;Database=PartingPets;Trusted_Connection=True;";

        public IEnumerable<Partners> GetAll()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var partners = db.Query<Partners>("select id, name, description, street, city, state, zipcode from partners").ToList();
                return partners;
            }
        }
    }
}
