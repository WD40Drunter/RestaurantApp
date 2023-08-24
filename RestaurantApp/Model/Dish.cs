using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class Dish
    {
        [Key]
        public long DishId { get; set; }

        public Dish(string name, int statusId, int restaurantId)
        {
            Name = name;
            StatusId = statusId;
            RestaurantId = restaurantId;
        }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public virtual Status? Status { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
    }
}
