using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartingPets.Models;

namespace PartingPets.Validators
{
    public class PetRequestValidator
    {
        public bool Validate(CreatePetRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name)
                || string.IsNullOrEmpty(requestToValidate.Breed)
                || string.IsNullOrEmpty(requestToValidate.DateOfBirth.ToString()));
        }
    }
}
