using System;
using System.Windows.Data;

namespace Demo.Admin.Client.Support
{
    public class ProductStatusConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool currentlyRented = (bool)value;

            return (currentlyRented ? "Currently Rented" : "Available");
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}