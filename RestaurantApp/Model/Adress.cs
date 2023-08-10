using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
