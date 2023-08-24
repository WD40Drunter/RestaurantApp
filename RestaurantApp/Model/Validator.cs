using System;
using System.Windows;

namespace RestaurantApp.Model
{
    public static class Validator
    {
        public static bool IsStringNotNull(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Upewnij się że wszystko uzupełniłeś");
                return false;
            }
            return true;
        }

        public static bool IsHourValid(string value)
        {
            if (Convert.ToInt32(value[..2]) >= 24 || Convert.ToInt32(value.Substring(3, 2)) >= 60)
            {
                MessageBox.Show("Błędna godzina (godzine 24:00 zapisz jako 00:00)");
                return false;
            }
            return true;
        }

        public static bool IsHouseNumberValid(string value)
        {
            foreach (var item in value)
            {
                if (!char.IsDigit(item) && item != '/')
                {
                    MessageBox.Show("Numer domu może zawierać tylko cyfry bądź znak /");
                    return false;
                }
            }
            return true;
        }
    }
}
