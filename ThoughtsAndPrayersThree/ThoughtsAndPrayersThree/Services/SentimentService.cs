using System;
using ThoughtsAndPrayersThree.Constants;

namespace ThoughtsAndPrayersThree.Services
{
    public static class SentimentService
    {

        public static string GetSentimentCategory(float? sentimentScore)
        {
            switch (sentimentScore)
            {
                case float number when (number > 0.7):
                    return SentimentCategories.HappySentiment;

                case float number when (number >= 0.3 && number <= 0.7):
                    return SentimentCategories.NeutralSentiment;

                case float number when (number >= 0 && number < 0.3):
                    return SentimentCategories.SadSentiment;

                case null:
                    return SentimentCategories.UncategorizedSentiment;
                default:
                    return string.Empty;
            }
        }
    }
}



//public static string GetSentimentCategory(double? sentimentScore)
//{
//    switch (sentimentScore)
//    {
//        case double number when (number > 0.7):

//            return SentimentCategories.HappySentiment;

//        case double number when (number >= 0.3 && number <= 0.7):
//            return SentimentCategories.NeutralSentiment;

//        case double number when (number >= 0 && number < 0.3):
//            return SentimentCategories.SadSentiment;

//        case null:
//            return SentimentCategories.UncategorizedSentiment;
//        default:
//            return string.Empty;
//    }
//}