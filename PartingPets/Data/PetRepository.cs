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
            using (var db = new SqlConnection(ConnectionString))
            {
                var newPetQuery = db.QueryFirstOrDefault<Pet>(@"Insert into pets (name, userId, breed, dateOfBirth, dateOfDeath)
                                                        Output inserted.*
                                                        Values(@name,@userId,@breed,@dateOfBirth,@dateOfDeath)",
                    new { name, userId, breed, dateOfBirth, dateOfDeath });
                if (newPetQuery != null)
                {
                    return newPetQuery;
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

        public Pet UpdatePet(Pet petToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updatePetQuery = @"Update Pets
                                    Set Name = @name,
                                    Breed = @breed,
                                    DateOfBirth = @dateOfBirth,
                                    DateOfDeath = @dateOfDeath,
                                    BurialStreet = @buirialStreet,
                                    BurialCity = @burialCity,
                                    BurialState = @burialState,
                                    BurialZipCode = @burialZipCode,
                                    BurialPlot = @buiralPlot";

                var rowsAffected = db.Execute(updatePetQuery, petToUpdate);

                if (rowsAffected == 1)
                {
                    return petToUpdate;
                }
                throw new Exception("Failed to update pet.");
            }
        }

        public void DeletePet(int id)
        {
            using(var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = id };
                var deleteQuery = "Delete from Pets where Id = @id";
                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("There was an error in deleting your pet data.");
                }
            }
        }
    }
}
