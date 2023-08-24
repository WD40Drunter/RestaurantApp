using System;
using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    internal class UserLoggedInButtonVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return "Hidden";
            }
            return "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
