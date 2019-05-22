using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Models
{
    public class Pet
    {
        public Pet(
                string name,
                string breed,
                DateTime dateOfBirth,
                DateTime dateOfDeath
                )
        {
            Name = name;
            Breed = breed;
            DateOfBirth = dateOfBirth;
            DateOfDeath = dateOfDeath;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Breed { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfDeath { get; set; }
        public string BurialStreet { get; set; }
        public string BurialCity { get; set; }
        public string BurialState { get; set; }
        public int BurialZipCode { get; set; }
        public string BurialPlot { get; set; }
    }
}
