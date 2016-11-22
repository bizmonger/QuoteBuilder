using System;
using System.Globalization;
using Entities;
using Xamarin.Forms;
using System.Diagnostics;

namespace UILogic.Converters
{
    [DebuggerNonUserCode]
    public class MaterialToCostConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;

            var material = value as Material;
            var materialCosts = 0.00m;
            materialCosts += (material.Quantity * material.MarkupPrice);

            return materialCosts;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Code Notifications", "RECS0083:Shows NotImplementedException throws in the quick task bar", Justification = "<Pending>")]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}