namespace PartingPets.Models
{
    public class User
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
        public int PartnerId { get; set; }
    }
}
