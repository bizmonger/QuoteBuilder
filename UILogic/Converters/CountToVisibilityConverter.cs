using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace UILogic.Converters
{
    [DebuggerNonUserCode]
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;

            return ((int)value) > 0;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Notifications", "RECS0083:Shows NotImplementedException throws in the quick task bar", Justification = "<Pending>")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}