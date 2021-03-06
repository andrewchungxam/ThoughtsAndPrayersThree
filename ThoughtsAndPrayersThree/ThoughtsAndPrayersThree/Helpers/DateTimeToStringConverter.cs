﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Helpers
{
    public class DateTimeToStringConverter : IValueConverter
    {
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value == null)
				return string.Empty;

            var dt = (DateTimeOffset)value;
			CultureInfo ci = new CultureInfo("en-US");
			string sampleDateTimeStringMonth = dt.ToString("MMM d", ci);
			string sampleDateTimeStringTime = dt.ToString("h:mm tt", ci);
			string sampleDateTimeStringConnected = ($"{sampleDateTimeStringMonth} at {sampleDateTimeStringTime}");
            return sampleDateTimeStringConnected;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}


