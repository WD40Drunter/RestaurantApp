using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Model
{
    public class Dish
    {
        [Key]
        public long DishId { get; set; }

        public Dish(string name, string status, int restaurantId)
        {
            Name = name;
            Status = status;
            RestaurantId = restaurantId;
        }
        public string Name { get; set; }
        public string Status { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }
    }
}
