using Dapper;
using Microsoft.Extensions.Options;
using PartingPets.Models;
using PartingPets.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PartingPets.Data
{
    public class UsersRepository
    {
        readonly string _connectionString;
        readonly SqlDateValidation _sqlDateValidator;

        public UsersRepository(IOptions<DbConfiguration> dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
            _sqlDateValidator = new SqlDateValidation();
        }

        public List<User> GetAllUsers()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var getAllUsersQuery = @"
                        SELECT
                            id,
                            FireBaseUid,
                            FirstName,
                            LastName,
                            Street1,
                            Street2,
                            City,
                            State,
                            Zipcode,
                            Email,
                            IsPartner,
                            PartnerID
                        FROM [User]";

                var allUsers = db.Query<User>(getAllUsersQuery).ToList();

                if (allUsers != null)
                {
                    return allUsers;
                }
            }
            throw new Exception("No users found");
        }

        public User GetUserById(string id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var getUserByIdQuery = @"
                        SELECT 
                            *
                        FROM [User] u
                        WHERE u.FireBaseUid = @id";

                var selectedUser = db.QueryFirstOrDefault<User>(getUserByIdQuery, new { id });

                if (selectedUser != null)
                {
                    return selectedUser;
                }
            }
            throw new Exception("User not found");
        }

        public User AddNewUser(CreateUserRequest newUserObj)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var newUserQuery = @"
                        INSERT INTO [User] (FireBaseUid, FirstName, LastName, Street1, Street2, City, State, ZipCode, Email, IsPartner, PartnerId, IsAdmin, IsDeleted, DateCreated)
                        OUTPUT Inserted.*
                            VALUES(@FireBaseUid, @FirstName, @LastName, @Street1, @Street2, @City, @State, @ZipCode, @Email, @IsPartner, @PartnerId, @IsAdmin, @IsDeleted, GETUTCDATE())";

                var newUser = db.QueryFirstOrDefault<User>(newUserQuery, new
                {
                    newUserObj.FireBaseUid,
                    newUserObj.FirstName,
                    newUserObj.LastName,
                    newUserObj.Street1,
                    newUserObj.Street2,
                    newUserObj.City,
                    newUserObj.State,
                    newUserObj.Zipcode,
                    newUserObj.Email,
                    newUserObj.IsPartner,
                    newUserObj.PartnerId,
                    newUserObj.IsAdmin,
                    newUserObj.IsDeleted
                });

                if (newUser != null)
                {
                    return newUser;
                }
            }
            throw new Exception("User not Created");
        }

        public EditUserRequest UpdateUser(EditUserRequest updatedUserObj)
        {
            // Handle the deleted user field sa we set it to Null in the DB if not set. Dot Net will pass in 1/1/0001 if set to null and SQL do not like
            if (updatedUserObj.IsPartner == false)
            {
                updatedUserObj.PartnerId = null;
            }

            // Validate our dates to check that they are valid ranges of dates for SQL server
            if (!_sqlDateValidator.IsValidSqlDateTime(updatedUserObj.DateDeleted))
            {
                // Value is not valid or null so set to a valid SQL date
                // Struggled with setting this to Null so this will work for now
                updatedUserObj.DateDeleted = DateTime.Parse("1800-01-01T00:00:00");
                //0001-01-01T00: 00:00
            }

            using (var db = new SqlConnection(_connectionString))
            {
                var editUserQuery = @"
                    UPDATE 
                      [User] 
                    SET 
                      [FireBaseUid] = @firebaseUid, 
                      [FirstName] = @firstName, 
                      [LastName] = @lastName, 
                      [Street1] = @street1, 
                      [Street2] = @street2, 
                      [City] = @city, 
                      [State] = @state, 
                      [ZipCode] = @zipCode, 
                      [Email] = @email, 
                      [IsPartner] = @isPartner, 
                      [PartnerID] = @partnerId, 
                      [IsAdmin] = @isAdmin, 
                      [DateCreated] = @dateCreated, 
                      [DateDeleted] = @dateDeleted, 
                      [IsDeleted] = @isDeleted 
                    WHERE 
                      id = @id";

                var rowsAffected = db.Execute(editUserQuery, updatedUserObj);

                if (rowsAffected == 1)
                {
                    return updatedUserObj;
                }
                throw new Exception("Could not update user");
            }
        }

        public User GetUserByDbId(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var getUserByIdQuery = @"
                        SELECT 
                            *
                        FROM [User] u
                        WHERE u.Id = @id";

                var selectedUser = db.QueryFirstOrDefault<User>(getUserByIdQuery, new { id });

                if (selectedUser != null)
                {
                    return selectedUser;
                }
            }
            throw new Exception("User not found");
        }

        public void DeleteUser(int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var deleteUserQuery = @"
                    UPDATE 
                      [User] 
                    SET 
                      [DateDeleted] = GETUTCDATE(), 
                      [IsDeleted] = 1 
                    WHERE 
                      id = @id";

                var rowsAffected = db.Execute(deleteUserQuery, new { id });

                if (rowsAffected != 1)
                {
                    throw new Exception("Error deleting the user");
                }
            }
        }
    }
}
