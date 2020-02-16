using System;
using System.Globalization;
using Xamarin.Forms;

namespace TD2
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int imageSource =(int)  value;

            return "https://td-api.julienmialon.com/images/" + imageSource;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}