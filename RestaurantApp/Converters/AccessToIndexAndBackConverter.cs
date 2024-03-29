﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    internal class AccessToIndexAndBackConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string zad = (string)value;
            if (zad == "Admin")
            {
                return "1";
            }
            return "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int zad = (int)value;
            if (zad == 1)
            {
                return "Admin";
            }
            return "Standard";
        }
    }
}
