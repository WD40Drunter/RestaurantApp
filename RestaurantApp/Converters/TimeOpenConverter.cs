using System;
using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    public class TimeOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string result;
            if (values[0].ToString() == values[1].ToString())
            {
                result = "cała doba";
            }
            else
            {
                result = values[0] + " - " + values[1];
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
