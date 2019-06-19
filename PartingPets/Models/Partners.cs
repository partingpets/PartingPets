using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class Partners
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RegistrationCode { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateDeleted { get; set; }

    }

}
