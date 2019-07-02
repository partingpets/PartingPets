using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class CreatePaymentTypeRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public long AccountNumber { get; set; }
        public string Type { get; set; }
        public int CVV { get; set; }
        public DateTime ExpDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
