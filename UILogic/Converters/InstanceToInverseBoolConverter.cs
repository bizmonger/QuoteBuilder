using System;
using System.Diagnostics;
using System.Globalization;
using Xamarin.Forms;

namespace UILogic.Converters
{
    [DebuggerNonUserCode]
    public class InstanceToInverseBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value == null;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Notifications", "RECS0083:Shows NotImplementedException throws in the quick task bar", Justification = "<Pending>")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}