using System;
using System.Globalization;
using Xamarin.Forms;
namespace TD2
{
     public class NegateBooleanConverter : IValueConverter
        {
            public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return !(bool)value;
            }
            public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                return !(bool)value;
            }
        }
}