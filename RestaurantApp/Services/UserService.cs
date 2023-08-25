using RestaurantApp.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        bool Exists(string login);
        User? GetUser(string login);
        void AddUser(User user);
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

        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }
    }
}
