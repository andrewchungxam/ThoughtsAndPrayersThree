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
    public abstract class BaseFunctionPrayerService  //FunctionPrayerService
    {
        #region Constant Fields
        static readonly Lazy<JsonSerializer> _serializerHolder = new Lazy<JsonSerializer>();
        static readonly Lazy<HttpClient> _clientHolder = new Lazy<HttpClient>(() => CreateHttpClient(TimeSpan.FromSeconds(60)));
        #endregion

        #region Fields
        static int _networkIndicatorCount = 0;
        #endregion

        #region Properties
        static HttpClient Client => _clientHolder.Value;
        static JsonSerializer Serializer => _serializerHolder.Value;
        #endregion

        #region Methods
        protected static async Task<List<CosmosDBPrayerRequest>> GetAllCosmosPrayerRequests(string apiUrl)
        {
            var stringPayload = string.Empty;

            try
            {
                UpdateActivityIndicatorStatus(true);

                using (var stream = await Client.GetStreamAsync(apiUrl).ConfigureAwait(false))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    if (json == null)
                        return default(List<CosmosDBPrayerRequest>);
                    //Task.Run(async () => await Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json))); // .ConfigureAwait(false);
                    //var listCosmosDB =  Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json))                    //return await Task.Run(async () => await Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json))); // .ConfigureAwait(false);
                    //return listCosmosDB;                                                                            //  await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                    //await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return default(List<CosmosDBPrayerRequest>);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<List<PrayerRequest>> GetAllCosmosPrayerRequestsConvertedToPrayerRequests(string apiUrl)
        {
            var stringPayload = string.Empty;

            try
            {
                UpdateActivityIndicatorStatus(true);

                using (var stream = await Client.GetStreamAsync(apiUrl).ConfigureAwait(false))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    if (json == null)
                        return default(List<PrayerRequest>);
                    var listCosmosDBPrayerRequest = await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                    var listOfConvertedPrayerRequests = new List<PrayerRequest>();
                    foreach (var cosmosDBItem in listCosmosDBPrayerRequest)
                    {
                        var tempConvertedItem = PrayerRequestConverter.ConvertToPrayerRequest(cosmosDBItem);
                        listOfConvertedPrayerRequests.Add(tempConvertedItem);
                    }
                    return listOfConvertedPrayerRequests;
                }
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return default(List<PrayerRequest>);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }

            //var ListOfConvertedPrayerRequests = new List<PrayerRequest>();

            //foreach (var cosmosDBItem in MyListOfPrayerRequests)
            //{
            //    var tempConvertedItem = PrayerRequestConverter.ConvertToPrayerRequest(cosmosDBItem);
            //    ListOfConvertedPrayerRequests.Add(tempConvertedItem);

            //}

        }


        protected static async Task<List<CosmosDBPrayerRequest>> GetCosmosPrayerRequestsByIdAsync(string apiUrl, PrayerRequest data)
        {
            var stringPayload = string.Empty;

            if (data != null)
                stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);
            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            try
            {
                UpdateActivityIndicatorStatus(true);

                using (var stream = await Client.GetStreamAsync(apiUrl).ConfigureAwait(false))
                using (var reader = new StreamReader(stream))
                using (var json = new JsonTextReader(reader))
                {
                    if (json == null)
                        return default(List<CosmosDBPrayerRequest>);
                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return default(List<CosmosDBPrayerRequest>);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }


        protected static async Task<HttpResponseMessage> PostCosmosPrayerRequestsAsync(string apiUrl, CosmosDBPrayerRequest data)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.PostAsync(apiUrl, httpContent).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<HttpResponseMessage> PostAndConvertPrayerRequestsAsync(string apiUrl, PrayerRequest data)
        {
            var cosmosDBPrayerRequest = PrayerRequestConverter.ConvertToCosmosPrayerRequest(data);
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(cosmosDBPrayerRequest)).ConfigureAwait(false);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.PostAsync(apiUrl, httpContent).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<HttpResponseMessage> PutCosmosPrayerRequestByAsync(string apiUrl, CosmosDBPrayerRequest data)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("PUT"),
                RequestUri = new Uri(apiUrl),
                Content = httpContent
            };

            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<HttpResponseMessage> PatchAndConvertPrayerRequestAsync(string apiUrl, PrayerRequest data)
        {
            var cosmosDBPrayerRequest = PrayerRequestConverter.ConvertToCosmosPrayerRequest(data);

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri(apiUrl),
                Content = httpContent
            };

            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<HttpResponseMessage> DeleteCosmosPrayerRequestsAsync(string apiUrl, CosmosDBPrayerRequest data)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            var httpRequest = new HttpRequestMessage
            {
                Method = new HttpMethod("DELETE"),
                RequestUri = new Uri(apiUrl),
                Content = httpContent
            };

            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.SendAsync(httpRequest).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        protected static async Task<HttpResponseMessage> DeleteCosmosPrayerRequestByIdAsync(string apiUrl)
        {

            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, new Uri(apiUrl));

            try
            {
                UpdateActivityIndicatorStatus(true);

                return await Client.SendAsync(httpRequest);
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return null;
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }

        static void UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
        {
            if (isActivityIndicatorDisplayed)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = true);
                _networkIndicatorCount++;
            }
            else if (--_networkIndicatorCount <= 0)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = false);
                _networkIndicatorCount = 0;
            }
        }

        static HttpClient CreateHttpClient(TimeSpan timeout)
        {
            HttpClient client;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    client = new HttpClient();
                    break;
                default:
                    client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip });
                    break;

            }
            client.Timeout = timeout;
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            return client;
        }
        #endregion
    }
}



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

