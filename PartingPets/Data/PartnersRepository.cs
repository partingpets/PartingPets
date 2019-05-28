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

        public Partners GetPartner(int id)
        {
            var sql = "select id, name, description, street, city, state, zipcode from partners where Id = @id;";
            using (var db = new SqlConnection(ConnectionString))
            {
                var partner = db.QueryFirstOrDefault<Partners>(sql, new { id });
                return partner;
            }
        }

        public Partners AddPartner(string name, string description, string street, string city, string state, string zipcode)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newPartner = db.QueryFirstOrDefault<Partners>(@"
                    Insert into partners(name, description, street, city, state, zipcode)
                    Output inserted.*
                    Values(@name, @description, @street, @city, @state, @zipcode)",
                    new { name, description, street, city, state, zipcode});

                if (newPartner != null)
                {
                    return newPartner;
                }
            }

            throw new Exception("Unfortunatley, a Parting Pets Partner was not created");
               
        }

        public void DeletePartner(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var parameter = new { Id = id };

                var deleteQuery = "Delete from Partners where Id = @id";

                var rowsAffected = db.Execute(deleteQuery, parameter);

                if (rowsAffected != 1)
                {
                    throw new Exception("We couldn't delete the user at this time");
                }
            }
        }

        public Partners UpdatePartner(Partners partnerToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updateQuery = @"Update Partners
                                    Set Name = @name,
                                    Description = @description,
                                    Street = @street,
                                    City = @city,
                                    State = @state,
                                    Zipcode = @zipcode
                                    Where Id = @id";

                var rowsAffected = db.Execute(updateQuery, partnerToUpdate);

                if (rowsAffected == 1)
                {
                    return partnerToUpdate;
                }
                throw new Exception("We could not update the partner");
            }
        }
    }
}
