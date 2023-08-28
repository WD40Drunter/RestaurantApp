using Microsoft.EntityFrameworkCore;
using RestaurantApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp.Services
{
    public interface IDishService
    {
        IEnumerable<Dish> GetAll();
        IEnumerable<Dish> GetSelected(int? restaurantId);
        IEnumerable<Dish> GetSelectedForStandard(int? restaurantId);
        Dish AddDish(Dish dish);
        void ChangeStatus();
    }
    public class DishService : IDishService
    {
        public DishService(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public IEnumerable<Dish> GetAll()
        {
            return _context.Dishes.Include(x => x.Restaurant);
        }

        public IEnumerable<Dish> GetSelected(int? restaurantId)
        {
            return _context.Dishes.Where(x => x.RestaurantId == restaurantId).Include(x => x.Restaurant);
        }

        public IEnumerable<Dish> GetSelectedForStandard(int? restaurantId)
        {
            return _context.Dishes.Where(x => x.RestaurantId == restaurantId && x.StatusId == 1).Include(x => x.Restaurant);
        }

        public Dish AddDish(Dish dish)
        {
            _context.Add(dish);
            _context.SaveChanges();
            return dish;
        }

        public void ChangeStatus()
        {
            _context.SaveChanges();
        }
    }
}
