using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using ThoughtsAndPrayersThree;
using ThoughtsAndPrayers.Services;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.Services
{
    public class FunctionPrayerService : BaseFunctionPrayerService
    {
        //AZURE-BASE
        public const string AzureFunctionStringBase = "https://thoughtsandprayersthreefunction.azurewebsites.net";

        //GetAllCosmosPrayerRequestsFunction: http://localhost:7071/api/GetAllCosmosPrayerRequestsFunction
        //GetAllPrayerRequestsFunction: http://localhost:7071/api/GetAllPrayerRequestsFunction
        //GetCosmosPrayerRequestsByIdAsyncFunction: http://localhost:7071/api/GetCosmosPrayerRequestsByIdAsyncFunction/{id}
        //PostCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/PostCosmosPrayerRequestsAsyncFunction
        //PostAndConvertPrayerRequestsAsyncFunction: http://localhost:7071/api/PostAndConvertPrayerRequestsAsyncFunction
        //PutCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/PutCosmosPrayerRequestsAsyncFunction
        //PatchAndConvertPrayerRequestsAsyncFunction: http://localhost:7071/api/PatchAndConvertPrayerRequestsAsyncFunction
        //DeleteCosmosPrayerRequestsAsyncFunction: http://localhost:7071/api/DeleteCosmosPrayerRequestsAsyncFunction
        //DeleteCosmosPrayerRequestsByIdAsyncFunction: http://localhost:7071/api/DeleteCosmosPrayerRequestsByIdAsyncFunction/{id}

        //GetAllCosmosPrayerRequestsFunction
        public const string RouteGetAllCosmosPrayerRequestsFunction = "/api/GetAllCosmosPrayerRequestsFunction";
        //GetAllPrayerRequestsFunction   
        public const string RouteGetAllPrayerRequestsFunction = "/api/GetAllPrayerRequestsFunction";
        //GetCosmosPrayerRequestsByIdAsyncFunction
        public const string RouteGetCosmosPrayerRequestsByIdAsyncFunction = "/api/GetCosmosPrayerRequestsByIdAsyncFunction/";   //--  {id}";
        //PostCosmosPrayerRequestsAsyncFunction
        public const string RoutePostCosmosPrayerRequestsAsyncFunction = "/api/PostCosmosPrayerRequestsAsyncFunction";
        //PostAndConvertPrayerRequestsAsyncFunction
        public const string RoutePostAndConvertPrayerRequestsAsyncFunction = "/api/PostAndConvertPrayerRequestsAsyncFunction";
        //PutCosmosPrayerRequestsAsyncFunction
        public const string RoutePutCosmosPrayerRequestsAsyncFunction = "/api/PutCosmosPrayerRequestsAsyncFunction";
        //PatchAndConvertPrayerRequestsAsyncFunction
        public const string RoutePatchAndConvertPrayerRequestsAsyncFunction = "/api/PatchAndConvertPrayerRequestsAsyncFunction";
        //DeleteCosmosPrayerRequestsAsyncFunction
        public const string RouteDeleteCosmosPrayerRequestsAsyncFunction = "/api/DeleteCosmosPrayerRequestsAsyncFunction";
        //DeleteCosmosPrayerRequestsByIdAsyncFunction
        public const string RouteDeleteCosmosPrayerRequestsByIdAsyncFunction = "/api/DeleteCosmosPrayerRequestsByIdAsyncFunction/";  //-- {id}";

        public static Task<List<CosmosDBPrayerRequest>> GetAllCosmosPrayerRequestsFunction()
        => GetAllCosmosPrayerRequests($"{AzureFunctionStringBase}{RouteGetAllCosmosPrayerRequestsFunction}");

        public static Task<List<PrayerRequest>> GetAllCosmosPrayerRequestsConvertedToPrayerRequestsFunction()
        => GetAllCosmosPrayerRequestsConvertedToPrayerRequests($"{AzureFunctionStringBase}{RouteGetAllPrayerRequestsFunction}");

        public static Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsyncFunction(string apiUrl, PrayerRequest data)
        => GetCosmosPrayerRequestsByIdAsync($"{AzureFunctionStringBase}{RouteGetCosmosPrayerRequestsByIdAsyncFunction}apiUrl", data);

        public static Task<HttpResponseMessage> PostCosmosPrayerRequestsAsyncFunction(CosmosDBPrayerRequest data)
        => PostCosmosPrayerRequestsAsync($"{AzureFunctionStringBase}{RoutePostCosmosPrayerRequestsAsyncFunction}", data);

        public static Task<HttpResponseMessage> PostAndConvertPrayerRequestsAsyncFunction(PrayerRequest data)
        => PostAndConvertPrayerRequestsAsync($"{AzureFunctionStringBase}{RoutePostAndConvertPrayerRequestsAsyncFunction}", data);

        public static Task<HttpResponseMessage> PutCosmosPrayerRequestByAsyncFunction(CosmosDBPrayerRequest data)
        => PutCosmosPrayerRequestByAsync($"{AzureFunctionStringBase}{RoutePutCosmosPrayerRequestsAsyncFunction}", data);

        public static Task<HttpResponseMessage> PatchAndConvertPrayerRequestAsyncFunction(PrayerRequest data)
        => PatchAndConvertPrayerRequestAsync($"{AzureFunctionStringBase}{RoutePatchAndConvertPrayerRequestsAsyncFunction}", data);

        public static Task<HttpResponseMessage> DeleteCosmosPrayerRequestsAsyncFunction(CosmosDBPrayerRequest data)
        => DeleteCosmosPrayerRequestsAsync($"{AzureFunctionStringBase}{RouteDeleteCosmosPrayerRequestsAsyncFunction}", data);

        public static Task<HttpResponseMessage> DeleteCosmosPrayerRequestByIdAsyncFunction(string apiUrl)
        => DeleteCosmosPrayerRequestByIdAsync($"{AzureFunctionStringBase}{RouteDeleteCosmosPrayerRequestsByIdAsyncFunction}/apiUrl");
    }
}






//public static Task<List<CosmosDBPrayerRequest>> GetAllPrayerRequestsFunction(string apiUrl) 
//=> GetAllPrayerRequests(apiUrl);

//public static Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsyncFunction(string apiUrl, PrayerRequest data) 
//=> GetCosmosPrayerRequestsByIdAsync(apiUrl, data);

//public static Task<HttpResponseMessage> PostCosmosPrayerRequestsAsyncFunction(string apiUrl, CosmosDBPrayerRequest data)
//=> PostCosmosPrayerRequestsAsync(apiUrl, data);

//public static Task<HttpResponseMessage> PatchCosmosPrayerRequestFunction(string apiUrl, CosmosDBPrayerRequest data)
//=> PatchCosmosPrayerRequest(apiUrl, data);

//public static Task<HttpResponseMessage> DeleteCosmosPrayerRequestFunction(string apiUrl)
//=> DeleteCosmosPrayerRequest(apiUrl);


//protected static async Task<HttpResponseMessage> PutCosmosPrayerRequestAsync(string apiUrl, CosmosDBPrayerRequest data)
//{
//    var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

//    var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

//    var httpRequest = new HttpRequestMessage
//    {
//        Method = new HttpMethod("PUT"),
//        RequestUri = new Uri(apiUrl),
//        Content = httpContent
//    };

//    try
//    {
//        UpdateActivityIndicatorStatus(true);

//        return await Client.SendAsync(httpRequest).ConfigureAwait(false);
//    }
//    catch (Exception e)
//    {
//        //AppCenterHelpers.LogException(e);
//        return null;
//    }
//    finally
//    {
//        UpdateActivityIndicatorStatus(false);
//    }
//}


//using System;
//using System.IO;
//using System.Net;
//using System.Text;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Net.Http.Headers;

//using Xamarin.Forms;

//using Newtonsoft.Json;
//using System.Collections.Generic;
//using ThoughtsAndPrayersThree.Models;

//namespace ThoughtsAndPrayers.Services
//{
//    abstract class BaseFunctionPrayerService  //FunctionPrayerService
//    {
//        #region Constant Fields
//        static readonly Lazy<JsonSerializer> _serializerHolder = new Lazy<JsonSerializer>();
//        static readonly Lazy<HttpClient> _clientHolder = new Lazy<HttpClient>(() => CreateHttpClient(TimeSpan.FromSeconds(60)));
//        #endregion

//        #region Fields
//        static int _networkIndicatorCount = 0;
//        #endregion

//        #region Properties
//        static HttpClient Client => _clientHolder.Value;
//        static JsonSerializer Serializer => _serializerHolder.Value;
//        #endregion

//        public const string AzureFunctionStringBase = "https://thoughtsandprayersthreefunction.azurewebsites.net";

//        #region Methods
//        protected static async Task<List<CosmosDBPrayerRequest>> GetAllPrayerRequests(string apiUrl)
//        {
//            var stringPayload = string.Empty;

//            try
//            {
//                UpdateActivityIndicatorStatus(true);

//                using (var stream = await Client.GetStreamAsync(apiUrl).ConfigureAwait(false))
//                using (var reader = new StreamReader(stream))
//                using (var json = new JsonTextReader(reader))
//                {
//                    if (json == null)
//                        return default(List<CosmosDBPrayerRequest>);
//                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
//                }
//            }
//            catch (Exception e)
//            {
//                //AppCenterHelpers.LogException(e);
//                return default(List<CosmosDBPrayerRequest>);
//            }
//            finally
//            {
//                UpdateActivityIndicatorStatus(false);
//            }
//        }

//        protected static async Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsync(string apiUrl, PrayerRequest data)
//        {
//            var stringPayload = string.Empty;

//            if (data != null)
//                stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);
//            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

//            try
//            {
//                UpdateActivityIndicatorStatus(true);

//                using (var stream = await Client.GetStreamAsync(apiUrl).ConfigureAwait(false))
//                using (var reader = new StreamReader(stream))
//                using (var json = new JsonTextReader(reader))
//                {
//                    if (json == null)
//                        return default(List<CosmosDBPrayerRequest>);
//                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
//                }
//            }
//            catch (Exception e)
//            {
//                //AppCenterHelpers.LogException(e);
//                return default(List<CosmosDBPrayerRequest>);
//            }
//            finally
//            {
//                UpdateActivityIndicatorStatus(false);
//            }
//        }


//        protected static async Task<HttpResponseMessage> PostCosmosPrayerRequestsAsync<CosmosDBPrayerRequest>(string apiUrl, CosmosDBPrayerRequest data)
//        {
//            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

//            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
//            try
//            {
//                UpdateActivityIndicatorStatus(true);

//                return await Client.PostAsync(apiUrl, httpContent).ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                //AppCenterHelpers.LogException(e);
//                return null;
//            }
//            finally
//            {
//                UpdateActivityIndicatorStatus(false);
//            }
//        }

//        protected static async Task<HttpResponseMessage> PatchCosmosPrayerRequest<CosmosDBPrayerRequest>(string apiUrl, CosmosDBPrayerRequest data)
//        {
//            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

//            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

//            var httpRequest = new HttpRequestMessage
//            {
//                Method = new HttpMethod("PATCH"),
//                RequestUri = new Uri(apiUrl),
//                Content = httpContent
//            };

//            try
//            {
//                UpdateActivityIndicatorStatus(true);

//                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
//            }
//            catch (Exception e)
//            {
//                //AppCenterHelpers.LogException(e);
//                return null;
//            }
//            finally
//            {
//                UpdateActivityIndicatorStatus(false);
//            }
//        }

//        protected static async Task<HttpResponseMessage> DeleteCosmosPrayerRequest(string id)
//        {

//            //API + id

//            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, new Uri(apiUrl));

//            try
//            {
//                UpdateActivityIndicatorStatus(true);

//                return await Client.SendAsync(httpRequest);
//            }
//            catch (Exception e)
//            {
//                //AppCenterHelpers.LogException(e);
//                return null;
//            }
//            finally
//            {
//                UpdateActivityIndicatorStatus(false);
//            }
//        }

//        static void UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
//        {
//            if (isActivityIndicatorDisplayed)
//            {
//                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = true);
//                _networkIndicatorCount++;
//            }
//            else if (--_networkIndicatorCount <= 0)
//            {
//                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = false);
//                _networkIndicatorCount = 0;
//            }
//        }

//        static HttpClient CreateHttpClient(TimeSpan timeout)
//        {
//            HttpClient client;

//            switch (Device.RuntimePlatform)
//            {
//                case Device.iOS:
//                case Device.Android:
//                    client = new HttpClient();
//                    break;
//                default:
//                    client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip });
//                    break;

//            }
//            client.Timeout = timeout;
//            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

//            return client;
//        }
//        #endregion
//    }
//}




