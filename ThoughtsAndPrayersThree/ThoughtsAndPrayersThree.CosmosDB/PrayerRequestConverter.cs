//TODO - UPDATE THIS ACCORDING TO NEW MODELS USED IN THIS THIS PROJECT

//USED FOR TRANSLATION BETWEEN FORMATS REQUIRED FOR LOCAL STORAGE VS REMOTE STORAGE

using System;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.CosmosDB
{
    public static class PrayerRequestConverter
    {
        public static CosmosDBPrayerRequest ConvertToCosmosPrayerRequest(PrayerRequest prayerRequest)
        {
            var myIdString = (prayerRequest.Id).ToString();

            var myCosmosDBPrayerRequest = new CosmosDBPrayerRequest()
            {
                Id = myIdString,
                CreatedDateTimeString = prayerRequest.CreatedDateTimeString,
                CreatedDateTime = prayerRequest.CreatedDateTime,
                FirstName = prayerRequest.FirstName,
                LastName = prayerRequest.LastName,
                FullName = prayerRequest.FullName,
                FullNameAndDate = prayerRequest.FullNameAndDate,

                FBProfileUrl = prayerRequest.FBProfileUrl,
                PrayerRequestText = prayerRequest.PrayerRequestText,
                NumberOfThoughts = prayerRequest.NumberOfThoughts,
                NumberOfPrayers = prayerRequest.NumberOfPrayers,
                StringTheNumberOfPrayers = prayerRequest.StringTheNumberOfPrayers
            };
            return myCosmosDBPrayerRequest;
        }

        public static PrayerRequest ConvertToPrayerRequest(CosmosDBPrayerRequest cosmosDBPrayerRequest)
        {
            var myIdInt = Int32.Parse(cosmosDBPrayerRequest.Id);

            var myPrayerRequest = new PrayerRequest()
            {
                Id = myIdInt,
                CreatedDateTimeString = cosmosDBPrayerRequest.CreatedDateTimeString,
                CreatedDateTime = cosmosDBPrayerRequest.CreatedDateTime,
                FirstName = cosmosDBPrayerRequest.FirstName,
                LastName = cosmosDBPrayerRequest.LastName,
                FullName = cosmosDBPrayerRequest.FullName,
                FullNameAndDate = cosmosDBPrayerRequest.FullNameAndDate,

                FBProfileUrl = cosmosDBPrayerRequest.FBProfileUrl,
                PrayerRequestText = cosmosDBPrayerRequest.PrayerRequestText,
                NumberOfThoughts = cosmosDBPrayerRequest.NumberOfThoughts,
                NumberOfPrayers = cosmosDBPrayerRequest.NumberOfPrayers,
                StringTheNumberOfPrayers = cosmosDBPrayerRequest.StringTheNumberOfPrayers
            };

            return myPrayerRequest;
        }
    }
}