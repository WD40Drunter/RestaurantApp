using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class Status
    {
        [Key]
        public int StatusId { get; set; }
        public Status(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
