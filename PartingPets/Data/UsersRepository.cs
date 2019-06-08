using Dapper;
using Microsoft.Extensions.Options;
using PartingPets.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Data
{
    public class UsersRepository
    {
        readonly string _connectionString;

        public UsersRepository(IOptions<DbConfiguration> dbConfig)
        {
            _connectionString = dbConfig.Value.ConnectionString;
        }

        public List<User> GetAllUsers()
        {
            using(var db = new SqlConnection(_connectionString))
            {
                var getAllUsersQuery = @"
                        SELECT
                            id,
                            FirstName,
                            LastName,
                            Street,
                            City,
                            State,
                            Zipcode,
                            Email
                        FROM [User]";

                var allUsers = db.Query<User>(getAllUsersQuery).ToList();

                if(allUsers != null)
                {
                    return allUsers;
                }
            }
            throw new Exception("No users found");
        }

        public User GetUserById(string id)
        {
            using(var db = new SqlConnection(_connectionString))
            {
                var getUserByIdQuery = @"
                        SELECT 
                            id,
                            FireBaseUid,
                            FirstName,
                            LastName,
                            Street,
                            City,
                            State,
                            Zipcode,
                            Email
                        FROM [User] u
                        WHERE u.FireBaseUid = @id";

                var selectedUser = db.QueryFirstOrDefault<User>(getUserByIdQuery, new { id });

                if(selectedUser != null)
                {
                    return selectedUser;
                }
            }
            throw new Exception("User not found");
        }

        public User AddNewUser(CreateUserRequest newUserObj)
        {
            using(var db = new SqlConnection(_connectionString))
            {
                var newUserQuery = @"
                        INSERT INTO [User] (FireBaseUid, FirstName, LastName, Street, City, State, ZipCode, Email, IsPartner, IsDeleted, DateCreated)
                        OUTPUT Inserted.*
                            VALUES(@FireBaseUid, @FirstName, @LastName, @Street, @City, @State, @ZipCode, @Email, @IsPartner, @IsDeleted, GETUTCDATE())";

                var newUser = db.QueryFirstOrDefault<User>(newUserQuery, new
                {
                    newUserObj.FireBaseUid,
                    newUserObj.FirstName,
                    newUserObj.LastName,
                    newUserObj.Street,
                    newUserObj.City,
                    newUserObj.State,
                    newUserObj.Zipcode,
                    newUserObj.Email,
                    newUserObj.IsPartner,
                    newUserObj.IsDeleted
                });

                if(newUser != null)
                {
                    return newUser;
                }
            }
            throw new Exception("User not Created");
        }
    }
}
