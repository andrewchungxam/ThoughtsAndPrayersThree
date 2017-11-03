using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;

//using ASampleApp;
//using ASampleApp.Models;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.CosmosDB
{
	public class CosmosDBPrayerService
	{
		//DBS - Collections - Documents
		static readonly string DatabaseId = "Xamarin";
		static readonly string CollectionId = "PrayerRequests";

		//CLIENT
		static readonly DocumentClient myDocumentClient = new DocumentClient(new Uri(CosmosDB.CosmosDBPrayerRequestStrings.myEndPoint), CosmosDB.CosmosDBPrayerRequestStrings.myKey);

		public static List<CosmosDBPrayerRequest> MyListOfCosmosDogs;

		//GETALL
		public static async Task<List<CosmosDBPrayerRequest>> GetAllCosmosPrayerRequests()
		{
			MyListOfCosmosDogs = new List<CosmosDBPrayerRequest>();
			try
			{

				var query = myDocumentClient.CreateDocumentQuery<CosmosDBPrayerRequest>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
											.AsDocumentQuery();
				while (query.HasMoreResults)
				{
					MyListOfCosmosDogs.AddRange(await query.ExecuteNextAsync<CosmosDBPrayerRequest>());
				}
			}
			catch (DocumentClientException ex)
			{
				Debug.WriteLine("Error: ", ex.Message);
			}
			return MyListOfCosmosDogs;
		}

		//GET
		public static async Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsync(string id)
		{
			var result = await myDocumentClient.ReadDocumentAsync<CosmosDBPrayerRequest>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));

			if (result.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return null;
			}

			List<CosmosDBPrayerRequest> returnedListCosmosDog = new List<CosmosDBPrayerRequest>();
			returnedListCosmosDog.Add(result);

			return returnedListCosmosDog;

		}

		//POST
		public static async Task PostCosmosPrayerRequestsAsync(CosmosDBPrayerRequest cosmosDBPrayerRequest)
		{
			await myDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), cosmosDBPrayerRequest);

		}

		//PUT
		public static async Task PutCosmosPrayerRequestsAsync(CosmosDBPrayerRequest cosmosDBPrayerRequest)
		{
			await myDocumentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cosmosDBPrayerRequest.Id), cosmosDBPrayerRequest);
		}

		//DELETE
		public static async Task DeleteCosmosPrayerRequestsAsync(CosmosDBPrayerRequest deleteCosmosDBPrayerRequest)
		{
			await myDocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, deleteCosmosDBPrayerRequest.Id));
		}
	}
}
