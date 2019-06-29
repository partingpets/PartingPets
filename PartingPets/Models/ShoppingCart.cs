using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
