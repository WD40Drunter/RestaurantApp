﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RestaurantApp.Converters
{
    internal class ComboBoxValue2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "Standard";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ComboBoxItem)value).Content;
        }
    }
}