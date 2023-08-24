using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class Adress
    {
        [Key]
        public int AdressId { get; set; }

        public Adress(string country, string city, string street, string houseNumber, string postalCode)
        {
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
        }

        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string FullAdress
        {
            get
            {
                return $"{Country} {City} ul.{Street} {HouseNumber} Kod pocztowy:{PostalCode}";
            }
        }
    }
}
