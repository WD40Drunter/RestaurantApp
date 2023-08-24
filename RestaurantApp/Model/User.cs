using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public User(string login, string password, string? access)
        {
            Login = login;
            Password = password;
            Access = access ?? "Standard";

        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Access { get; set; }
    }
}
