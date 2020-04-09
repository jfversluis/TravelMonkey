using System;
using System.Globalization;
using Xamarin.Forms;

namespace TravelMonkey.Converters
{
    public class LanguageCodeToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return "";

            var cultureInfo = new CultureInfo((string)value);

            if (cultureInfo == null)
                return "";

            return cultureInfo.DisplayName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not needed
            throw new NotImplementedException();
        }
    }
}