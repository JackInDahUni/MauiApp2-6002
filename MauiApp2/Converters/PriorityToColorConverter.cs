using System.Globalization;
using MauiApp2.Models;

namespace MauiApp2.Converters { 
public class PriorityToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Priority priority)
            {
                return priority switch
                {
                    Priority.Low => Colors.Green,
                    Priority.Medium => Colors.Blue,
                    Priority.High => Colors.Orange,
                    Priority.Urgent => Colors.Red,
                    _ => Colors.Gray
                };
            }

            return Colors.Gray;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}