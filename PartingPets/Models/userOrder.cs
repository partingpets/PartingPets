using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class userOrder
    {
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }

        public List<OrderLines> OrderItems { get; set; }

    }
}
