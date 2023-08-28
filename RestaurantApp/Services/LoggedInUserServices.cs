using RestaurantApp.Model;

namespace RestaurantApp.Services
{
    public interface ILoggedInUserServices
    {
        void Logout();
        void Login(User user);
        User? GetUser();
        string GetUserAccess();
    }

    public class LoggedInUserServices : ILoggedInUserServices
    {
        public LoggedInUserServices()
        {

        }

        private User? _loggedInUser;

        public User? LoggedInUser
        {
            get
            {
                return _loggedInUser;
            }
            set
            {
                _loggedInUser = value;
            }
        }

        public User? GetUser()
        {
            return LoggedInUser;
        }

        public string GetUserAccess()
        {
            return LoggedInUser?.Access ?? string.Empty;
        }

        public void Login(User user)
        {
            LoggedInUser = user;
        }

        public void Logout()
        {
            LoggedInUser = null;
        }
    }
}
