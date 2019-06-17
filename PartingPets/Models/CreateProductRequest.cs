using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class CreateProductRequest
    {
        public string Name { get; set; } 
        public decimal UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public bool IsOnSale { get; set; }
        public bool IsDeleted { get; set; }
        public int PartnerId { get; set; }
    }
}
