using System.Globalization;
using MauiApp2.Models;

namespace MauiApp2.Converters
{
    public class BoolToStrikethroughConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isCompleted && isCompleted)
            {
                // Since FontAttributes does not have a 'Strikethrough' value, we need to handle this differently.
                // Assuming the application uses a TextDecorations property for strikethrough, we return the appropriate value.
                return TextDecorations.Strikethrough;
            }

            // Return None or Default if no strikethrough is needed.
            return TextDecorations.None;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
