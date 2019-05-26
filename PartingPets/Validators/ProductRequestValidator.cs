using System;
using PartingPets.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartingPets.Validators
{
    public class ProductRequestValidator
    {
        public bool Validate(CreateProductRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name));
        }
    }
}
