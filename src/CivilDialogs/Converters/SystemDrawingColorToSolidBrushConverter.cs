using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CivilDialogs.Converters;

[ValueConversion(typeof(System.Drawing.Color), typeof(SolidColorBrush))]
public class SystemDrawingColorToSolidBrushConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		var drawingColor = (System.Drawing.Color)value;
		var brush = Color.FromRgb(drawingColor.R, drawingColor.G, drawingColor.B);
		return new SolidColorBrush(brush);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
