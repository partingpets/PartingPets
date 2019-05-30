using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartingPets.Models;

namespace PartingPets.Validators
{
    public class PartnerRequestValidator
    {
        public bool Validate(CreatePartnerRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name)
                || string.IsNullOrEmpty(requestToValidate.Description)
                || string.IsNullOrEmpty(requestToValidate.Street)
                || string.IsNullOrEmpty(requestToValidate.State)
                || string.IsNullOrEmpty(requestToValidate.City)
                || string.IsNullOrEmpty(requestToValidate.Zipcode));
        }
    }
}
