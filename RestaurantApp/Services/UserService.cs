using RestaurantApp.Model;
using System.Linq;

namespace RestaurantApp.Services
{
    public interface IUserService
    {
        public bool Exists(string login);
        public User? GetUser(string login);
        public void AddUser(User user);
    }
    public class UserService : IUserService
    {
        public UserService(Context context)
        {
            _context = context;
        }
        private readonly Context _context;

        public bool Exists(string login)
        {
            return _context.Users.Any(x => x.Login == login);
        }

        public User? GetUser(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Login == login);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
