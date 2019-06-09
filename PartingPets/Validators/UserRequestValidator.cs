using PartingPets.Models;

namespace PartingPets.Validators
{
    public class UserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return !(string.IsNullOrEmpty(requestToValidate.FireBaseUid)
                || string.IsNullOrEmpty(requestToValidate.FirstName)
                || string.IsNullOrEmpty(requestToValidate.LastName)
                || string.IsNullOrEmpty(requestToValidate.Street1)
                || string.IsNullOrEmpty(requestToValidate.State)
                || string.IsNullOrEmpty(requestToValidate.City)
                || string.IsNullOrEmpty(requestToValidate.Zipcode)
                || string.IsNullOrEmpty(requestToValidate.Email));
        }
    }
}