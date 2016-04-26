using System;
using System.Globalization;
using System.Windows;

namespace Demo.Admin.Client.Support
{
    public class DateTime2VizConverterApprovedReverse : System.Windows.Data.IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var returnValue = Visibility.Visible;

            if (values[0] != null)  // canceled
            {
                returnValue = Visibility.Collapsed;
            }
            else
            {
                if (values[1] != null)  // approved
                {
                    returnValue = Visibility.Visible;
                }
                else
                {
                    returnValue = Visibility.Collapsed;
                }

                if (values[2] != null)  // shipped
                {
                    returnValue = Visibility.Collapsed;
                }
            }

            return returnValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
