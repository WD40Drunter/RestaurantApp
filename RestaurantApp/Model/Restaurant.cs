using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }

        public Restaurant(string name, decimal rating, string openingHour, string closingHour, int adressId)
        {
            Name = name;
            Rating = rating;
            OpeningHour = openingHour;
            ClosingHour = closingHour;
            AdressId = adressId;
        }

        public string Name { get; set; }
        public decimal Rating { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }

        public int AdressId { get; set; }
        public virtual Adress? Adress { get; set; }
    }
}
