using System;
using System.Globalization;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Helpers
{
    public class NumberOfThoughtsIntToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;
			var numberOfThoughtsInt = (int)value;
            var numberOfThoughtString = numberOfThoughtsInt.ToString();
            var concantenatedNumberOfThoughts = ($"{numberOfThoughtString} Thoughts");
            //var concantenatedNumberOfThoughts = ($"{numberOfThoughtString} Thoughts & 5 Prayers");
			return concantenatedNumberOfThoughts;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}


