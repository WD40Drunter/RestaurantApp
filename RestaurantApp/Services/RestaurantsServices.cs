using Microsoft.EntityFrameworkCore;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IRestaurantsService
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant AddRestaurant(Restaurant restaurant);
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
    }
}
