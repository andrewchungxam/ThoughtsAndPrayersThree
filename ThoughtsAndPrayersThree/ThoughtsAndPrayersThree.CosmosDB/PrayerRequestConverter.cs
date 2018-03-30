//using System;

//using ThoughtsAndPrayersThree.Models;
//using ThoughtsAndPrayersThree.Models;

//namespace ThoughtsAndPrayersThree.CosmosDB
//{
//	public static class PrayerRequestConverter
//	{
//		public static CosmosDBPrayerRequest ConvertToCosmosPrayerRequest(PrayerRequest prayerRequest)
//		{
//			var myIdString = (prayerRequest.Id).ToString();

//			var myCosmosDBPrayerRequest = new CosmosDBPrayerRequest()
//			{
//				Id = myIdString,
//				CreatedDateTimeString = prayerRequest.CreatedDateTimeString,
//				CreatedDateTime = prayerRequest.CreatedDateTime,
//				FirstName = prayerRequest.FirstName,
//				LastName = prayerRequest.LastName,
//				FullName = prayerRequest.FullName,
//				FBProfileUrl = prayerRequest.FBProfileUrl,
//				PrayerRequestText = prayerRequest.PrayerRequestText
//			};
//			return myCosmosDBPrayerRequest;
//		}

//		public static PrayerRequest ConvertToPrayerRequest(CosmosDBPrayerRequest cosmosDBPrayerRequest)
//		{
//			var myIdInt = Int32.Parse(cosmosDBPrayerRequest.Id);

//			var myPrayerRequest = new PrayerRequest()
//			{
//				Id = myIdInt,
//				CreatedDateTimeString = cosmosDBPrayerRequest.CreatedDateTimeString,
//				CreatedDateTime = cosmosDBPrayerRequest.CreatedDateTime,
//				FirstName = cosmosDBPrayerRequest.FirstName,
//				LastName = cosmosDBPrayerRequest.LastName,
//				FullName = cosmosDBPrayerRequest.FullName,
//				FBProfileUrl = cosmosDBPrayerRequest.FBProfileUrl,
//				PrayerRequestText = cosmosDBPrayerRequest.PrayerRequestText
//			};
//			return myPrayerRequest;
//		}
//	}
//}
