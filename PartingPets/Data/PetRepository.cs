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

        public Pet AddPet(string name, int userId, string breed, DateTime dateOfBirth, DateTime dateOfDeath)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var newPet = connection.QueryFirstOrDefault<Pet>(@"Insert into pets (name, userId, breed, dateOfBirth, dateOfDeath)
                                                        Output inserted.*
                                                        Values(@name,@userId,@breed,@dateOfBirth,@dateOfDeath)",
                    new { name, userId, breed, dateOfBirth, dateOfDeath });
                if (newPet != null)
                {
                    return newPet;
                }

                throw new Exception("No Pet Found");
            }
        }

        public IEnumerable<Pet> GetAllPets()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<Pet>("select * from pets");
            }
        }
    }
}
