using Microsoft.EntityFrameworkCore;
using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IDishService
    {
        IEnumerable<Dish> GetAll();
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
    }
}
