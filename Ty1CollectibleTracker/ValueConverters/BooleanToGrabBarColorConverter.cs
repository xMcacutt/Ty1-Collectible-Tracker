using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Ty1CollectibleTracker;

public class BooleanToGrabBarColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return !(bool)value ? new SolidColorBrush(Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF)) 
            : new SolidColorBrush(Color.FromArgb(0x1, 0x0, 0x0, 0x0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}