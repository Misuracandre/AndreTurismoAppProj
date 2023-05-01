namespace AndreTurismoApp.Models
{
    public class Address
    {
        public Address() { }

        public Address(AddressDTO addressDTO)
        {
            this.Street = addressDTO.Logradouro;
            this.CEP = addressDTO.CEP;
            this.IdCity = new City() { Description = addressDTO.City };
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string CEP { get; set; }
        public City IdCity { get; set; }
    }
}