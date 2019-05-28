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

        public User GetUserById(int id)
        {
            using(var db = new SqlConnection(_connectionString))
            {
                var getUserByIdQuery = @"
                    SELECT id, FirstName, LastName, Street, City, State, Zipcode, Email
                    FROM [User] u
                    WHERE u.Id = @id";

                var selectedUser = db.QueryFirstOrDefault<User>(getUserByIdQuery, new { id });

                if(selectedUser != null)
                {
                    return selectedUser;
                }
            }
            throw new Exception("User not found");
        }
    }
}
