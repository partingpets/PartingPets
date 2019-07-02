using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PurchaseDate { get; set; }

        public List<OrderLines> OrderLines { get; set; }
    }
}
