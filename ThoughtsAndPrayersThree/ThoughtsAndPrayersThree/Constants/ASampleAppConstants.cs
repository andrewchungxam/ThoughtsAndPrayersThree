using System;
namespace ThoughtsAndPrayersThree.Constants
	{
		static public class ASampleAppConstants
		{
			public static String MobileCenterIOS { get; } = "\"ios=294968e2-d712-43fc-a8bd-1492ed9ea29c;\"";
		}
	}

	namespace ThoughtsAndPrayersThree.BlobStorage
	{
		public static class AzureBlobConstants
		{
			public static String BlobUrlAndKey = "DefaultEndpointsProtocol=https;AccountName=asampleappfive;AccountKey=ACCOUNT_KEY==";
		}
	}

	namespace ThoughtsAndPrayersThree.CosmosDB
	{
		public static class CosmosDBStrings
		{
			//#error SIGN UP FOR AZURE - THEN SET THE BELOW KEYS AND THEN COMMENT OUT THIS LINE
			//static readonly String myEndPoint = "https://YOUR_AZURE_COSMODB_INSTANCE.documents.azure.com:44339283123/";
			//static readonly String myKey = "
			//#error Make sure to add this Nuget package to all your platform projects Microsoft.Azure.DocumentDb.Core
		}
	}