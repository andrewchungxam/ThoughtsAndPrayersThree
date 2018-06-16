using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using ThoughtsAndPrayersThree;
using ThoughtsAndPrayers.Services;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.Services
{
    public class NotificationTriggerService : BaseFunctionNotificationTriggersService
    {
        //AZURE-BASE
        public const string NotificationAzureFunctionStringBase = "https://thoughtsandprayersthreefunction.azurewebsites.net";

        //GetAllCosmosPrayerRequestsFunction: http://localhost:7071/api/GetAllCosmosPrayerRequestsFunction
        //GetAllPrayerRequestsFunction: http://localhost:7071/api/GetAllPrayerRequestsFunction

        //DeleteCosmosPrayerRequestsByIdAsyncFunction
        public const string RouteTriggerHappyNotification = "/api/NotificationFunctionHappyAgreement";
        public const string RouteTriggerSadAssuranceNotification = "/api/NotificationFunctionSadReassurance";

        public static Task<HttpResponseMessage> TriggerHappyNotificationFunction()
        => TriggerHappyNotification($"{NotificationAzureFunctionStringBase}{RouteTriggerHappyNotification}");

        public static Task<HttpResponseMessage> TriggerSadAssuranceNotificationFunction()
        => TriggerSadAssurnanceNotification($"{NotificationAzureFunctionStringBase}{RouteTriggerSadAssuranceNotification}");
    }
}



//public const string RouteGetCosmosPrayerRequestsByIdAsyncFunction = "/api/GetCosmosPrayerRequestsByIdAsyncFunction/";   //--  {id}";
//public static Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsyncFunction(string apiUrl, PrayerRequest data)
//=> GetCosmosPrayerRequestsByIdAsync($"{AzureFunctionStringBase}{RouteGetCosmosPrayerRequestsByIdAsyncFunction}apiUrl",  data);

//GetCosmosPrayerRequestsByIdAsyncFunction: http://localhost:7071/api/GetCosmosPrayerRequestsByIdAsyncFunction/{id}
//PostCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/PostCosmosPrayerRequestsAsyncFunction
//PostAndConvertPrayerRequestsAsyncFunction: http://localhost:7071/api/PostAndConvertPrayerRequestsAsyncFunction
//PutCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/PutCosmosPrayerRequestsAsyncFunction
//PatchAndConvertPrayerRequestsAsyncFunction: http://localhost:7071/api/PatchAndConvertPrayerRequestsAsyncFunction
//DeleteCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/DeleteCosmosPrayerRequestsAsyncFunction
//DeleteCosmosPrayerRequestsByIdAsyncFunction: http://localhost:7071/api/DeleteCosmosPrayerRequestsByIdAsyncFunction/{id}