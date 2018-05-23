using System;
using System.Globalization;
using ThoughtsAndPrayersThree.Services;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Helpers
{
    public class SentimentCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            var sentimentFloatValue = (float?)value;
            var sentimentCategory = SentimentService.GetSentimentCategory(sentimentFloatValue);
            return sentimentCategory;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


