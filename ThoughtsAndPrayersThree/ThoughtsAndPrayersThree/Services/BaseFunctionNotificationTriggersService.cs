
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xamarin.Forms;

using Newtonsoft.Json;
using System.Collections.Generic;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.CosmosDB;

namespace ThoughtsAndPrayers.Services
{
    public abstract class BaseFunctionNotificationTriggersService  //FunctionPrayerService
    {
        #region Constant Fields
        static readonly Lazy<JsonSerializer> _serializerHolder = new Lazy<JsonSerializer>();
        //static readonly Lazy<HttpClient> _clientHolder = new Lazy<HttpClient>(() =>  CreateHttpClient(TimeSpan.FromSeconds(60)));

        public static Func<HttpClient> clientFunc = new Func<HttpClient>(() => new HttpClient());
        static readonly Lazy<HttpClient> _clientHolder = new Lazy<HttpClient>(clientFunc);

        #endregion

        #region Fields
        static int _networkIndicatorCount = 0;
        #endregion

        #region Properties
        static HttpClient Client => _clientHolder.Value;
        static JsonSerializer Serializer => _serializerHolder.Value;
        #endregion

        #region Methods

        protected static async Task<HttpResponseMessage> TriggerHappyNotification(string apiUrl)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(apiUrl),
            };

            try
            {

                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
        }


        protected static async Task<HttpResponseMessage> TriggerSadAssurnanceNotification(string apiUrl)
        {
            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("POST"),
                RequestUri = new Uri(apiUrl),
            };

            try
            {

                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
        }
        #endregion

    }
}
