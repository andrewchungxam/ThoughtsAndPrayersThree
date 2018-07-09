using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.NotificationHubs;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.Extensions.Http;

using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Generic;
using ThoughtsAndPrayersThree.Functions.Constants;
using ThoughtsAndPrayersThree.Shared.Models;

namespace ThoughtsAndPrayersThree.Functions.NotificationFunctions
{
    public static class NotificationTriggerForPrayerRequestsViaFunction
    {
        //PLEASE AVOID PORT EXHAUSTION: 
        //https://docs.microsoft.com/en-us/azure/azure-functions/manage-connections





        [FunctionName("NotificationFunctionHappyAgreement")]
        public static async Task<HttpResponseMessage> RunNotificationFunctionHappyAgreement([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            //
            //THIS WILL ONLY WORK IF IN YOUR IOS APPLICATION - YOU USE A TEMPLATE REGISTRATION
            //
            try
            {
                NotificationHubClient hubClient = Microsoft.Azure.NotificationHubs.NotificationHubClient.CreateClientFromConnectionString
                    (
                        AzureNotificationHubConstants.NotificationHubFullAccessConnectionString,
                        AzureNotificationHubConstants.NotificationHubName,
                        true
                    );
                // Create an array of breaking news categories.
                //var categories = new string[] { "World", "Politics", "Business", "Technology", "Science", "Sports" };

                //Array.Resize(ref categories, categories.Length + 1);
                //var templateUsername = "TemplateUser101";
                //categories[categories.Length - 1] = "username:" + templateUsername;

                //Dictionary<string, string> templateParams = new Dictionary<string, string>();

                //foreach (var category in categories)
                //{
                //    templateParams["messageParam"] = "Breaking " + category + " News!";
                //    await hubClient.SendTemplateNotificationAsync(templateParams, categories);
                //}

                ////////////////////////
                var happyAgreement = new string[] { 
                    "You can do this!",
                    "We're all with you",
                    "God is with you!",
                    "Yeah you got this!",
                    "You can do this!",
                    "We're all with you",
                    "God is with you!",
                    "Yeah you got this!",
                    "You can do this!",
                    "We're all with you",
                    "God is with you!",
                    "Yeah you got this!"
                };

                Dictionary<string, string> templateParams = new Dictionary<string, string>();

                var templateUsername = "NewUser101";
                var tagTemplateUsername = "username:" + templateUsername;

                Random rnd = new Random();
                int randomEncouragement = rnd.Next(0, 11);

                templateParams["messageParam"] = happyAgreement[randomEncouragement];

                await hubClient.SendTemplateNotificationAsync(templateParams, tagTemplateUsername);

                return req.CreateResponse(System.Net.HttpStatusCode.OK, "Success - Notification Function triggered - Template Multiple");
            }

            catch (Exception exception)
            {
                Debug.WriteLine("Error: ", exception.Message);
            }

            return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Bad Requestion - Notification Function triggered - Template Multiple");
        }



        [FunctionName("NotificationFunctionSadReassurance")]
        public static async Task<HttpResponseMessage> RunNotificationFunctionSadReassurance([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            //
            //THIS WILL ONLY WORK IF IN YOUR IOS APPLICATION - YOU USE A TEMPLATE REGISTRATION
            //
            try
            {
                NotificationHubClient hubClient = Microsoft.Azure.NotificationHubs.NotificationHubClient.CreateClientFromConnectionString
                    (
                        AzureNotificationHubConstants.NotificationHubFullAccessConnectionString,
                        AzureNotificationHubConstants.NotificationHubName,
                        true
                    );

                ////////////////////////
                var sadReassurance = new string[] {
                    "You're loved more than you know",
                    "God will redeem all things",
                    "He loves you more than you can know!"
                };

                Dictionary<string, string> templateParams = new Dictionary<string, string>();

                var templateUsername = "NewUser101";
                var tagTemplateUsername = "username:" + templateUsername;

                Random rnd = new Random();
                int randomEncouragement = rnd.Next(0, 2);

                templateParams["messageParam"] = sadReassurance[randomEncouragement];

                await hubClient.SendTemplateNotificationAsync(templateParams, tagTemplateUsername);

                return req.CreateResponse(System.Net.HttpStatusCode.OK, "Success - Notification Function triggered - Template Multiple");
            }

            catch (Exception exception)
            {
                Debug.WriteLine("Error: ", exception.Message);
            }

            return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, "Bad Requestion - Notification Function triggered - Template Multiple");
        }




    }
}
