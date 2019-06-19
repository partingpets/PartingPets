using System;

namespace PartingPets.Models
{
    public class EditUserRequest
    {
        public int Id { get; set; }
        public string FireBaseUid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Email { get; set; }
        public bool IsPartner { get; set; }
        public string PartnerId { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateDeleted { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
