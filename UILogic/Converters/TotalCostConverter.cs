using System;
using System.Globalization;
using Entities;
using Entities.Utilities;
using Xamarin.Forms;
using System.Diagnostics;

namespace UILogic.Converters
{
    [DebuggerNonUserCode]
    public class TotalCostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            var service = value as Service;
            return service.TotalCost();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Notifications", "RECS0083:Shows NotImplementedException throws in the quick task bar", Justification = "<Pending>")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}