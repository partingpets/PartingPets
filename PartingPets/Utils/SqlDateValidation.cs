using System;

namespace PartingPets.Utils
{
    public class SqlDateValidation
    {
        // https://stackoverflow.com/questions/7054782/validate-datetime-before-inserting-it-into-sql-server-database
        /// <summary>
        /// An method to verify whether a value is 
        /// kosher for SQL Server datetime. 
        /// </summary>
        /// <param name="dateToCheck">A date string that may parse</param>
        /// <returns>True if the parameter is valid for SQL Sever datetime</returns>
        public bool IsValidSqlDateTime(DateTime dateToCheck)
        {
            bool valid = false;
            DateTime testDate = DateTime.MinValue;
            System.Data.SqlTypes.SqlDateTime sqlDateTime;

            try
            {
                // take advantage of the native conversion
                sqlDateTime = new System.Data.SqlTypes.SqlDateTime(dateToCheck);
                valid = true;
            }
            catch(System.Data.SqlTypes.SqlTypeException ex)
            {

                // no need to do anything, this is the expected out of range error
            }

            return valid;
        }
    }
}
