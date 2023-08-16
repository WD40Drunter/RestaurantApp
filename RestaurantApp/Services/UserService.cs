using RestaurantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Services
{
    public interface IUserService
    {
        public User? GetUser(string login);
    }
    public class UserService : IUserService
    {
        public UserService(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public User? GetUser(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Login == login);
        }
    }
}
