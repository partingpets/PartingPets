using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PartingPets.Models;

namespace PartingPets.Validators
{
    public class PaymentTypeRequestValidator
    {
        public bool Validate(CreatePaymentTypeRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.Name)
                || string.IsNullOrEmpty(requestToValidate.UserId.ToString())
                || string.IsNullOrEmpty(requestToValidate.AccountNumber.ToString())
                || string.IsNullOrEmpty(requestToValidate.CVV.ToString())
                || string.IsNullOrEmpty(requestToValidate.ExpDate.ToString())
                || string.IsNullOrEmpty(requestToValidate.Type));
        }
    }
}
