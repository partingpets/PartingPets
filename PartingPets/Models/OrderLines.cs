using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class OrderLines
    {

        public int OrdersId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
