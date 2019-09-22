using System;
using Windows.UI.Xaml.Data;

namespace MyMvvmLightApp.Converters
{
	public class StringToValueConverter : IValueConverter
	{
		public object ValueIfNull { get; set; }
		public object ValueIfNotEmpty { get; set; }
		public object ValueIfEmpty { get; set; }
		public object DefaultValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var stringValue = value as string;
			var convertedValue = default(object);

			if (stringValue == null)
			{
				convertedValue = ValueIfNull;
			}
			else if (stringValue.Length > 0)
			{
				convertedValue = ValueIfNotEmpty;
			}
			else
			{
				convertedValue = ValueIfEmpty;
			}

			return convertedValue ?? DefaultValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
