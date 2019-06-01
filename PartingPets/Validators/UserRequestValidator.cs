using PartingPets.Models;

namespace PartingPets.Validators
{
    public class UserRequestValidator
    {
        public bool Validate(User requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.FirstName)
                || string.IsNullOrEmpty(requestToValidate.LastName)
                || string.IsNullOrEmpty(requestToValidate.Street)
                || string.IsNullOrEmpty(requestToValidate.State)
                || string.IsNullOrEmpty(requestToValidate.City)
                || string.IsNullOrEmpty(requestToValidate.Zipcode))
                || string.IsNullOrEmpty(requestToValidate.Email);
        }
    }
}