﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

using Xamarin.Forms;

using Newtonsoft.Json;

namespace ThoughtsAndPrayers.Functions
{
    public class FunctionPrayerService
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
        protected static async Task<List<CosmosDBPrayerRequest>> GetAllPrayerRequests(string apiUrl)
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
                        return default(PrayerRequest);
                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return default(PrayerRequest);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
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
                        return default(PrayerRequest);
                    return await Task.Run(() => Serializer.Deserialize<List<CosmosDBPrayerRequest>>(json)).ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
                return default(CosmosDBPrayerRequest);
            }
            finally
            {
                UpdateActivityIndicatorStatus(false);
            }
        }


        protected static async Task<HttpResponseMessage> PostCosmosPrayerRequestFunctionAsync<CosmosDBPrayerRequest>(string apiUrl, CosmosDBPrayerRequest data)
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

        protected static async Task<HttpResponseMessage> PatchCosmosPrayerRequestFunction<CosmosDBPrayerRequest>(string apiUrl, CosmosDBPrayerRequest data)
        {
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

        protected static async Task<HttpResponseMessage> DeleteCosmosPrayerRequestFunction(string apiUrl)
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
