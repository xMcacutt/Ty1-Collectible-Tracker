using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ty1CollectibleTracker;

public class FontFamilyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value as FontFamily).Source;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}