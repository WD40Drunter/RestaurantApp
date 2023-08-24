using System;
using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    internal class UserLoggedOutButtonVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return "Visible";
            }
            return "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
