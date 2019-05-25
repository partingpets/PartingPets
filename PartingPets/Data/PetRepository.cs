using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PartingPets.Models;

namespace PartingPets.Data
{
    public class PetRepository
    {
        const string ConnectionString = "Server=localhost; Database=PartingPets; Trusted_Connection=True;";

        public IEnumerable<Pet> GetAllPets()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<Pet>("select name,breed from pets");
            }
        }
    }
}
