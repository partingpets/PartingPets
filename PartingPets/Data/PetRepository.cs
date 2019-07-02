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

        public Pet AddPet(CreatePetRequest newPetObj)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newPetQuery = db.QueryFirstOrDefault<Pet>(@"
                    Insert into pets (name, userId, breed, dateOfBirth, dateOfDeath, burialStreet, burialCity, burialState, burialZipCode, burialPlot)
                    Output inserted.*
                    Values(@name,@userId,@breed,@dateOfBirth,@dateOfDeath,@burialStreet,@burialCity,@burialState,@burialZipCode,@burialPlot)",
                    new { newPetObj.Name,
                          newPetObj.UserId,
                          newPetObj.Breed,
                          newPetObj.DateOfBirth,
                          newPetObj.DateOfDeath,
                          newPetObj.BurialStreet,
                          newPetObj.BurialCity,
                          newPetObj.BurialState,
                          newPetObj.BurialZipCode,
                          newPetObj.BurialPlot });
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

        public IEnumerable<Pet> GetMyPets(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.Query<Pet>("select * from pets where pets.userId = @id", new { id });
            }
        }

        public Pet GetSinglePet(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                return db.QueryFirstOrDefault<Pet>("select * from pets where pets.id = @id", new { id });
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
                                    BurialStreet = @burialStreet,
                                    BurialCity = @burialCity,
                                    BurialState = @burialState,
                                    BurialZipCode = @burialZipCode,
                                    BurialPlot = @burialPlot
                                    Where id = @id"
;

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
