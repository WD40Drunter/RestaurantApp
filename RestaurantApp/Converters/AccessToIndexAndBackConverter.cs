using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    internal class AccessToIndexAndBackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zad = (string)value;
            if(zad == "Admin")
            {
                return "1";
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zad = (string)value;
            if (zad == "1")
            {
                return "Admin";
            }
            return "Standard";
        }
    }
}
