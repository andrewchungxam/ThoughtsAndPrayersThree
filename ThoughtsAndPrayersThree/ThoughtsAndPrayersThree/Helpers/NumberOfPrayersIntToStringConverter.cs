using System;
using System.Globalization;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Helpers
{
	public class NumberOfPrayersIntToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var numberOfPrayersInt = (int)value;
			var numberOfPrayersString = numberOfPrayersInt.ToString();
			var concantenatedNumberOfPrayers = ($"{numberOfPrayersString} Prayers");
			return concantenatedNumberOfPrayers;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}


