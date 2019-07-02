using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using PartingPets.Models;

namespace PartingPets.Data
{
    public class PaymentRepository
    {
        const string ConnectionString = "Server=localhost; Database=PartingPets; Trusted_Connection=True;";

        public PaymentType AddPaymentType(CreatePaymentTypeRequest newPaymentTypeObj)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var newPaymentQuery = db.QueryFirstOrDefault<PaymentType>(@"
                    Insert into PaymentType (userId, name, accountNumber, type, CVV, expDate, isDeleted)
                    Output inserted.*
                    Values(@userId,@name,@accountNumber,@type,@CVV,@expDate,0)",
                    new
                    {
                        newPaymentTypeObj.UserId,
                        newPaymentTypeObj.Name,
                        newPaymentTypeObj.AccountNumber,
                        newPaymentTypeObj.Type,
                        newPaymentTypeObj.CVV,
                        newPaymentTypeObj.ExpDate,
                        newPaymentTypeObj.IsDeleted
                    });
                if (newPaymentQuery != null)
                {
                    return newPaymentQuery;
                }

                throw new Exception("No Payment Found");

            }
        }

        public IEnumerable<PaymentType> GetUserPT(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var userPTQuery = "select * from PaymentType where PaymentType.userId = @id";

                return db.Query<PaymentType>(userPTQuery, new { id });
            }
        }

        public PaymentType UpdatePT(PaymentType PTToUpdate)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updatePTQuery = @"Update PaymentType
                                    Set Name = @name,
                                    AccountNumber = @accountNumber,
                                    Type = @type,
                                    CVV = @cvv,
                                    ExpDate = @expDate
                                    Where id = @id";

                var rowsAffected = db.Execute(updatePTQuery, PTToUpdate);

                if (rowsAffected == 1)
                {
                    return PTToUpdate;
                }
                throw new Exception("Failed to update your payment option.");
            }
        }

        public void DeletePT(int id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var updatePTQuery = @"Update PaymentType
                                    Set isDeleted = 1
                                    Where id = @id";

                var rowsAffected = db.Execute(updatePTQuery, new { id } );

                if (rowsAffected != 1)
                {
                    throw new Exception("Failed to delete your payment option.");
                }
            }

        }
    }
}
