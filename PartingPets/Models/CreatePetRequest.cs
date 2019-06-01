using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class CreatePetRequest
    {
        public string Name { get; set; }
        public string Breed { get; set; }
        public int UserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
    }
}
