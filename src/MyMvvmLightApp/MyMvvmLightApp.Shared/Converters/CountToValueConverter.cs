using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyMvvmLightApp.Converters
{
	public class CountToValueConverter : IValueConverter
	{
		public object ValueIfPositive { get; set; }
		public object ValueIfZero { get; set; }
		public object ValueIfNegative { get; set; }
		public object DefaultValue { get; set; }

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var count = value as int?;
			var convertedValue = default(object);

			if (count > 0)
			{
				convertedValue = ValueIfPositive;
			}
			else if (count == 0)
			{
				convertedValue = ValueIfZero;
			}
			else
			{
				convertedValue = ValueIfNegative;
			}

			return convertedValue ?? DefaultValue;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
