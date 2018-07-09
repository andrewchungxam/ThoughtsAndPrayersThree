//TODO - ADD TRY CATCH AND LOGGING TO THE LOWER METHODS
//TODO - MAKE SURE TO GET AS RETURN VALUETYPE HTTP RESPONSES MESSAGES

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.Documents.Client;

using ThoughtsAndPrayersThree.Models;
using System.Net;
using System.Net.Http;
using ThoughtsAndPrayersThree.CosmosDB;

using ThoughtsAndPrayersThree.Functions.CosmosDBPrayerServiceFromFunction;

namespace ThoughtsAndPrayersThree.Functions.CosmosDB
{
    public class CosmosDBPrayerService
    {
        //DBS - Collections - Documents
        static readonly string DatabaseId = "Xamarin";
        static readonly string CollectionId = "PrayerRequests";

        //CLIENT
        static readonly DocumentClient myDocumentClient = new DocumentClient(new Uri(CosmosDBConstants.myEndPoint), CosmosDBConstants.myKey);
        public static List<CosmosDBPrayerRequest> MyListOfPrayerRequests;

        //GETALL
        public static async Task<List<CosmosDBPrayerRequest>> GetAllCosmosPrayerRequests()
        {
            MyListOfPrayerRequests = new List<CosmosDBPrayerRequest>();
            try
            {
                var query = myDocumentClient
                    .CreateDocumentQuery<CosmosDBPrayerRequest>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                    .AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    MyListOfPrayerRequests.AddRange(await query.ExecuteNextAsync<CosmosDBPrayerRequest>());
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
            return MyListOfPrayerRequests;
        }

        //GETALL and CONVERT TO PRAYER REQUESTS
        public static async Task<List<PrayerRequest>> GetAllCosmosPrayerRequestsConvertedToPrayerRequests()
        {
            MyListOfPrayerRequests = new List<CosmosDBPrayerRequest>();
            try
            {

                var query = myDocumentClient
                    .CreateDocumentQuery<CosmosDBPrayerRequest>(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                    .AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    MyListOfPrayerRequests.AddRange(await query.ExecuteNextAsync<CosmosDBPrayerRequest>());
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

            var listOfConvertedPrayerRequests = new List<PrayerRequest>();

            foreach (var cosmosDBItem in MyListOfPrayerRequests)
            {
                var tempConvertedItem = ThoughtsAndPrayersThree.Functions.CosmosDBPrayerServiceFromFunction.PrayerRequestConverter.ConvertToPrayerRequest(cosmosDBItem);
                listOfConvertedPrayerRequests.Add(tempConvertedItem);

            }
            return listOfConvertedPrayerRequests;
        }

        //GET
        public static async Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsync(string id)
        {
            var result = await myDocumentClient.ReadDocumentAsync<CosmosDBPrayerRequest>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            List<CosmosDBPrayerRequest> returnedListCosmosPrayer = new List<CosmosDBPrayerRequest>();
            returnedListCosmosPrayer.Add(result);

            return returnedListCosmosPrayer;

        }

        //GET BY OBJECT
        public static async Task<List<CosmosDBPrayerRequest>> GetSpecificCosmosPrayerRequestsAsync(CosmosDBPrayerRequest cosmosDBPrayerRequest)
        {
            var result = await myDocumentClient.ReadDocumentAsync<CosmosDBPrayerRequest>(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cosmosDBPrayerRequest.Id));

            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            List<CosmosDBPrayerRequest> returnedListCosmosPrayer = new List<CosmosDBPrayerRequest>();
            returnedListCosmosPrayer.Add(result);

            return returnedListCosmosPrayer;
        }

        //POST
        public static async Task<HttpStatusCode> PostCosmosPrayerRequestsAsync(CosmosDBPrayerRequest cosmosDBPrayerRequest)
        {
            var result = await myDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), cosmosDBPrayerRequest);
            return result?.StatusCode ?? throw new HttpRequestException("Post Failed");

        }

        //CONVERT AND POST

        public static async Task<HttpStatusCode> PostAndConvertPrayerRequestsAsync(PrayerRequest prayerRequest)
        {
            var cosmosDBPrayerRequest = ThoughtsAndPrayersThree.Functions.CosmosDBPrayerServiceFromFunction.PrayerRequestConverter.ConvertToCosmosPrayerRequest(prayerRequest);
            var result = await myDocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), cosmosDBPrayerRequest);
            return result?.StatusCode ?? throw new HttpRequestException("Post Failed");
        }

        //PUT
        public static async Task<HttpStatusCode> PutCosmosPrayerRequestsAsync(CosmosDBPrayerRequest cosmosDBPrayerRequest)
        {
            var result = await myDocumentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cosmosDBPrayerRequest.Id), cosmosDBPrayerRequest);
            return result?.StatusCode ?? throw new HttpRequestException("Put Failed");
        }

        //CONVERT + PUT
        public static async Task<HttpStatusCode> PatchAndConvertPrayerRequestsAsync(PrayerRequest prayerRequest)
        {
            var cosmosDBPrayerRequest = ThoughtsAndPrayersThree.Functions.CosmosDBPrayerServiceFromFunction.PrayerRequestConverter.ConvertToCosmosPrayerRequest(prayerRequest);
            var result = await myDocumentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, cosmosDBPrayerRequest.Id), cosmosDBPrayerRequest);
            return result?.StatusCode ?? throw new HttpRequestException("Put Failed");
        }

        //DELETE
        public static async Task<HttpStatusCode> DeleteCosmosPrayerRequestsAsync(CosmosDBPrayerRequest deleteCosmosDBPrayerRequest)
        {
            var result = await myDocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, deleteCosmosDBPrayerRequest.Id));
            return result?.StatusCode ?? throw new HttpRequestException("Delete Failed");
        }

        //DELETE-ID
        public static async Task<HttpStatusCode> DeleteCosmosPrayerRequestsByIdAsync(string id)
        {
            var result = await myDocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
            return result?.StatusCode ?? throw new HttpRequestException("Delete Failed");
        }

    }
}