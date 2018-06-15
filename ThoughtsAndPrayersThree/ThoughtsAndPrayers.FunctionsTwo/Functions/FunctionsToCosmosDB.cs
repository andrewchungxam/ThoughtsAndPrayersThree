
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
//using ThoughtsAndPrayersThree.CosmosDB;
using ThoughtsAndPrayersThree.Functions.CosmosDB;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.Functions
{
    public static class FunctionsToCosmosDB
    {
        
        [FunctionName("GetAllCosmosPrayerRequestsFunction")]
        public static async Task<HttpResponseMessage> RunGetAllCosmosPrayerRequestsFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllCosmosPrayerRequestsFunction")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var listCosmosPrayer = await CosmosDBPrayerService.GetAllCosmosPrayerRequests();

            if (listCosmosPrayer == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Did not return Get All Cosmos Prayers Request");

            //OPTION 1
            return req.CreateResponse(System.Net.HttpStatusCode.OK, listCosmosPrayer);

            //OPTION 2
            //var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            //responseMessage.Content = new StringContent(returnedSerializedListCosmosPrayer, Encoding.UTF8, "application/json");
            //return responseMessage;
        }


        [FunctionName("GetAllPrayerRequestsFunction")]
        public static async Task<HttpResponseMessage> RunGetAllPrayerRequestsFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllPrayerRequestsFunction")]HttpRequestMessage req, TraceWriter log)
        {
            //NOTE - RETURNED INT32 MUST BE SMALLER THAN 2,147,483,64 or larger if negative than -2,147,483,64

            log.Info("C# HTTP trigger function processed a request.");

            var listPrayer = await CosmosDBPrayerService.GetAllCosmosPrayerRequestsConvertedToPrayerRequests();

            if (listPrayer == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Did not return Get All Prayers Request");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, listPrayer);
        }

        [FunctionName("GetCosmosPrayerRequestsByIdAsyncFunction")]
        public static async Task<HttpResponseMessage> RunGetCosmosPrayerRequestsByIdAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCosmosPrayerRequestsByIdAsyncFunction/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            //NOTE - THIS URL REQUIRES ID AT THE END
            // http://localhost:7071/api/GetCosmosPrayerRequestsByIdAsyncFunction/92158276

            log.Info("C# HTTP trigger function processed a request.");

            var listCosmosPrayer = await CosmosDBPrayerService.GetCosmosPrayerRequestsByIdAsync(id);

            if (listCosmosPrayer == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {id}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, listCosmosPrayer);
        }



        [FunctionName("PostCosmosPrayerRequestsAsyncFunction")]
        public static async Task<HttpResponseMessage> RunPostCosmosPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "PostCosmosPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        {
            //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
            //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
            //  {
            //    "id": "921582760",
            //    "SharedStringId": "921582760",
            //    "CreatedDateTimeString": "May 14 11:10 AM",
            //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
            //    "StringOnlyDateTime": null,
            //    "UpdatedAtString": null,
            //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
            //    "FirstName": "Andrew",
            //    "LastName": "Kim",
            //    "FullName": "Andrew Kim",
            //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
            //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
            //    "PrayerRequestText": "From Postman",
            //    "NumberOfThoughts": 7,
            //    "NumberOfPrayers": 1,
            //    "StringTheNumberOfPrayers": "new and updated commanded",
            //    "IsDeleted": false
            //  }

            log.Info("C# HTTP trigger function processed a request.");

            var postCosmosPrayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var cosmosPrayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CosmosDBPrayerRequest>(postCosmosPrayerRequestResultJson);
            var cosmosPrayerPut = await CosmosDBPrayerService.PostCosmosPrayerRequestsAsync(cosmosPrayerRequestResultObject);
            var cosmosPrayerRequestResultObjectId = cosmosPrayerRequestResultObject.Id;

            if (cosmosPrayerPut == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {cosmosPrayerRequestResultObjectId}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, cosmosPrayerPut);
        }

        [FunctionName("PostAndConvertPrayerRequestsAsyncFunction")]
        public static async Task<HttpResponseMessage> RunPostAndConvertPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "PostAndConvertPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        {
            //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
            //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
            //id in this case should be an int (since it is a "PrayerRequest" not a "CosmosDBPrayerRequest")
            //{
            //     "id": 921582759,
            //    "SharedStringId": "921582759",
            //    "CreatedDateTimeString": "May 14 11:10 AM",
            //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
            //    "StringOnlyDateTime": null,
            //    "UpdatedAtString": null,
            //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
            //    "FirstName": "Andrew",
            //    "LastName": "Kim",
            //    "FullName": "Andrew Kim",
            //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
            //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
            //    "PrayerRequestText": "From Postman post",
            //    "NumberOfThoughts": 7,
            //    "NumberOfPrayers": 1,
            //    "StringTheNumberOfPrayers": "new and updated commanded",
            //    "IsDeleted": false
            //}

            log.Info("C# HTTP trigger function processed a request.");

            var postPrayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var prayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PrayerRequest>(postPrayerRequestResultJson);
            var cosmosPrayerPost = await CosmosDBPrayerService.PostAndConvertPrayerRequestsAsync(prayerRequestResultObject);
            var prayerRequestResultObjectId = prayerRequestResultObject.Id;

            if (cosmosPrayerPost == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {prayerRequestResultObjectId}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, cosmosPrayerPost);
        }

        [FunctionName("PutCosmosPrayerRequestsAsyncFunction")]
        public static async Task<HttpResponseMessage> RunPutCosmosPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "PutCosmosPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        {

            //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
            //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
            //PUT CAN BE USED TO MODIFY AN EXISITING ITEM
            //{
            //    id": "921582760",
            //    "SharedStringId": "921582760",
            //    "CreatedDateTimeString": "May 14 11:10 AM",
            //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
            //    "StringOnlyDateTime": null,
            //    "UpdatedAtString": null,
            //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
            //    "FirstName": "Andrew",
            //    "LastName": "Kim",
            //    "FullName": "Andrew Kim",
            //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
            //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
            //    "PrayerRequestText": "From Postman updated via Put",
            //    "NumberOfThoughts": 7,
            //    "NumberOfPrayers": 1,
            //    "StringTheNumberOfPrayers": "new and updated commanded",
            //    "IsDeleted": false
            //}

            log.Info("C# HTTP trigger function processed a request.");

            var putCosmosPrayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var cosmosPrayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CosmosDBPrayerRequest>(putCosmosPrayerRequestResultJson);
            var cosmosPrayerPut = await CosmosDBPrayerService.PutCosmosPrayerRequestsAsync(cosmosPrayerRequestResultObject);
            var cosmosPrayerRequestResultObjectId = cosmosPrayerRequestResultObject.Id;

            if (cosmosPrayerPut == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {cosmosPrayerRequestResultObjectId}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, cosmosPrayerPut);
        }


        //PATCH + CONVERT FUNCTION
        [FunctionName("PatchAndConvertPrayerRequestsAsyncFunction")]
        public static async Task<HttpResponseMessage> RunPatchAndConvertPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "PatchAndConvertPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        {
            //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
            //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
            //id in this case should be an Int (since it is a "PrayerRequest" not a "CosmosDBPrayerRequest")
            //{
            //    "id": 921582759,
            //    "SharedStringId": "921582759",
            //    "CreatedDateTimeString": "May 14 11:10 AM",
            //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
            //    "StringOnlyDateTime": null,
            //    "UpdatedAtString": null,
            //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
            //    "FirstName": "Andrew",
            //    "LastName": "Kim",
            //    "FullName": "Andrew Kim",
            //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
            //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
            //    "PrayerRequestText": "Patch - From Postman post",
            //    "NumberOfThoughts": 7,
            //    "NumberOfPrayers": 1,
            //    "StringTheNumberOfPrayers": "new and updated commanded",
            //    "IsDeleted": false
            //}

            log.Info("C# HTTP trigger function processed a request.");

            var prayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var prayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PrayerRequest>(prayerRequestResultJson);
            var contactPatched = await CosmosDBPrayerService.PatchAndConvertPrayerRequestsAsync(prayerRequestResultObject);
            var prayerRequestResultObjectId = prayerRequestResultObject.Id;

            if (contactPatched == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {prayerRequestResultObjectId}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, contactPatched);
        }

        //DELETE FUNCTION
        [FunctionName("DeleteCosmosPrayerRequestsAsyncFunction")]
        public static async Task<HttpResponseMessage> RunDeleteCosmosPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteCosmosPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        {
            //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
            //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
            // 
            //{
            //    "id": 921582759,
            //    "SharedStringId": "921582759",
            //    "CreatedDateTimeString": "May 14 11:10 AM",
            //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
            //    "StringOnlyDateTime": null,
            //    "UpdatedAtString": null,
            //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
            //    "FirstName": "Andrew",
            //    "LastName": "Kim",
            //    "FullName": "Andrew Kim",
            //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
            //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
            //    "PrayerRequestText": "To be delete - From Postman post",
            //    "NumberOfThoughts": 7,
            //    "NumberOfPrayers": 1,
            //    "StringTheNumberOfPrayers": "new and updated commanded",
            //    "IsDeleted": false
            //}

            log.Info("C# HTTP trigger function processed a request.");

            var cosmosDBPrayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var cosmosDBPrayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CosmosDBPrayerRequest>(cosmosDBPrayerRequestResultJson);
            var contactDeleted = await CosmosDBPrayerService.DeleteCosmosPrayerRequestsAsync(cosmosDBPrayerRequestResultObject);
            var contactDeletedId = cosmosDBPrayerRequestResultObject.Id;
            if (contactDeleted == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id:{contactDeletedId}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, contactDeleted);
        }

        //DELETE FUNCTION BY ID
        [FunctionName("DeleteCosmosPrayerRequestsByIdAsyncFunction")]
        public static async Task<HttpResponseMessage> RunDeleteCosmosPrayerRequestsByIdAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteCosmosPrayerRequestsByIdAsyncFunction/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            //NOTE - THIS URL REQUIRES ID AT THE END
            // http://localhost:7071/api/DeleteCosmosPrayerRequestsAsyncFunction/921582759

            log.Info("C# HTTP trigger function processed a request.");

            var contactDeleted = await CosmosDBPrayerService.DeleteCosmosPrayerRequestsByIdAsync(id);

            if (contactDeleted == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id:{id}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, contactDeleted);
        }

    }
}