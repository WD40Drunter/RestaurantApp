using System.Windows;

namespace RestaurantApp.Model
{
    public static class UserValidator
    {
        public static bool IsLoggedIn(User? user)
        {
            if (user is null)
            {
                MessageBox.Show("Zaloguj się");
                return false;
            }
            return true;
        }

        public static bool IsAdmin(User? user)
        {
            if (user is null)
            {
                MessageBox.Show("Zaloguj się");
                return false;
            }
            if (user!.Access != "Admin")
            {
                MessageBox.Show("Brak uprawnień");
                return false;
            }
            return true;
        }
    }
}
