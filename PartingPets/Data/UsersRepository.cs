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
            using (var db = new SqlConnection(_connectionString))
            {
                var getAllUsersQuery = @"
                        SELECT id, FirstName, LastName, Street, City, State, Zipcode, Email
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
                        SELECT id, FireBaseUid, FirstName, LastName, Street, City, State, Zipcode, Email, IsPartner
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
    }
}