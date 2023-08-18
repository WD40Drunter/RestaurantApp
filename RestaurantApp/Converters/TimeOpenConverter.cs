using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    public class TimeOpenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string result;
            //MessageBox.Show(values[0]);
            //MessageBox.Show(values[1]);
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
