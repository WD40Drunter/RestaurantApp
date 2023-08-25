using Microsoft.EntityFrameworkCore;
using RestaurantApp.Model;
using System.Collections.Generic;

namespace RestaurantApp.Services
{
    public interface IRestaurantsService
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant AddRestaurant(Restaurant restaurant);
        void EditRestaurant(Restaurant oldValue, Restaurant newValue);
    }
    public class RestaurantsServices : IRestaurantsService
    {
        public RestaurantsServices(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.Include(x => x.Adress);
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public void EditRestaurant(Restaurant oldValue, Restaurant newValue)
        {
            oldValue.Name = newValue.Name;
            oldValue.Rating = newValue.Rating;
            oldValue.OpeningHour = newValue.OpeningHour;
            oldValue.ClosingHour = newValue.ClosingHour;

            _context.SaveChanges();
        }
    }
}
