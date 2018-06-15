using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

using Microsoft.Rest;

using Newtonsoft.Json;
//using ThoughtsAndPrayersThree.CosmosDB;
using ThoughtsAndPrayersThree.Functions.CosmosDB;
using ThoughtsAndPrayersThree.Models;

using ThoughtsAndPrayersThree.Functions.Constants;


namespace ThoughtsAndPrayersThree.Functions
{
    class ApiServiceClientCredentials : ServiceClientCredentials
    {
        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            request.Headers.Add("Ocp-Apim-Subscription-Key", FunctionConstants.TextAnalyticsKey);
            return base.ProcessHttpRequestAsync(request, cancellationToken);
        }
    }

    //SAMPLE INCLUDING LANGUAGE DETECTION
    public class FunctionsToTextSentiment
    {
        [FunctionName("GetPrayerRequestSentimentById")]
        public static async Task<HttpResponseMessage> RunGetPrayerRequestSentimentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPrayerRequestSentimentById/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var listCosmosPrayerFromGet = await CosmosDBPrayerService.GetCosmosPrayerRequestsByIdAsync(id);
            var cosmosPrayerFromGet = listCosmosPrayerFromGet.FirstOrDefault();

            var textToAnalyze = cosmosPrayerFromGet.PrayerRequestText;
            var sentimentDouble = await GetSentiment(textToAnalyze);

            //create cosmos db prayer request and add sentiment
            if (sentimentDouble != null) { 
                double strictDouble = sentimentDouble.Value;
                cosmosPrayerFromGet.SentimentScore = (float)strictDouble;
            }

            CosmosDBPrayerService.PutCosmosPrayerRequestsAsync(cosmosPrayerFromGet);

            if (listCosmosPrayerFromGet == null | sentimentDouble == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Cosmos Prayer not found with the following Id: {cosmosPrayerFromGet.Id}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, sentimentDouble);
        }

        [FunctionName("PutCosmosDBSentimentById")]
        public static async Task<HttpResponseMessage> RunPutCosmosDBSentimentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "PutCosmosDBSentimentById/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var listCosmosPrayerFromGet = await CosmosDBPrayerService.GetCosmosPrayerRequestsByIdAsync(id);
            var cosmosPrayerFromGet = listCosmosPrayerFromGet.FirstOrDefault();

            var textToAnalyze = cosmosPrayerFromGet.PrayerRequestText;
            var sentimentDouble = await GetSentiment(textToAnalyze);

            if (listCosmosPrayerFromGet == null | sentimentDouble == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Cosmos Prayer not found with the following Id: {cosmosPrayerFromGet.Id}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, sentimentDouble);
        }


        public static async Task<double?> GetSentiment(string text)
        {
            var sentimentDocument = new MultiLanguageBatchInput(new List<MultiLanguageInput> { { new MultiLanguageInput(language: "en", id: "1", text: text) } });

            ITextAnalyticsAPI _client = new TextAnalyticsAPI(new ApiServiceClientCredentials());
            _client.AzureRegion = AzureRegions.Southcentralus;

            var sentimentResults = await _client.SentimentAsync(sentimentDocument).ConfigureAwait(false);

            if (sentimentResults?.Errors?.Any() ?? false)
            {
                var exceptionList = sentimentResults.Errors.Select(x => new Exception($"Id: {x.Id}, Message: {x.Message}"));
                throw new AggregateException(exceptionList);
            }

            var documentResult = sentimentResults?.Documents?.FirstOrDefault();

            return documentResult?.Score;
        }

        [FunctionName("GetPrayerRequestSentiment")]
        public static async Task<HttpResponseMessage> RunGetPrayerRequestSentiment([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetPrayerRequestSentiment")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var getCosmosPrayerRequestResultJson = await req.Content.ReadAsStringAsync();
            var getCosmosPrayerRequestResultObject = Newtonsoft.Json.JsonConvert.DeserializeObject<CosmosDBPrayerRequest>(getCosmosPrayerRequestResultJson);

            var listCosmosPrayerFromGet = await CosmosDBPrayerService.GetSpecificCosmosPrayerRequestsAsync(getCosmosPrayerRequestResultObject);
            var cosmosPrayerFromGet = listCosmosPrayerFromGet.FirstOrDefault();

            var textToAnalyze = cosmosPrayerFromGet.PrayerRequestText;

            var sentimentDouble = await GetSentiment(textToAnalyze);

            if (listCosmosPrayerFromGet == null | sentimentDouble == null)
                return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Cosmos Prayer not found with the following Id: {getCosmosPrayerRequestResultObject.Id}");

            return req.CreateResponse(System.Net.HttpStatusCode.OK, sentimentDouble);
        }

        public void ListOfTextAnalysisMethods()
        {
            // Create a client.
            ITextAnalyticsAPI _client = new TextAnalyticsAPI(new ApiServiceClientCredentials());
            _client.AzureRegion = AzureRegions.Southcentralus;

            var result = _client.DetectLanguageAsync
                                    (new BatchInput
                                        (new List<Input>()
                                            {
                                                new Input("1", "This is a document written in English."),
                                                new Input("2", "Este es un document escrito en Español."),
                                                new Input("3", "这是一个用中文写的文件")
                                            }
                                        )
                                    ).Result;

            foreach (var document in result.Documents)
            {
                Console.WriteLine("Document ID: {0}, Language: {1}", document.Id, document.DetectedLanguages[0].Name);
            }

            KeyPhraseBatchResult result2 = _client.KeyPhrasesAsync
                                            (new MultiLanguageBatchInput
                                                (new List<MultiLanguageInput>()
                                                    {
                                                        new MultiLanguageInput("ja", "1", "猫は幸せ"),
                                                        new MultiLanguageInput("de", "2", "Fahrt nach Stuttgart und dann zum Hotel zu Fu."),
                                                        new MultiLanguageInput("en", "3", "My cat is stiff as a rock."),
                                                        new MultiLanguageInput("es", "4", "A mi me encanta el fútbol!")
                                                    }
                                                )
                                            ).Result;

            foreach (var document in result2.Documents)
            {
                Console.WriteLine("Document ID: {0}", document.Id);
                Console.WriteLine("\t Key phrases:");

                foreach (string keyphrase in document.KeyPhrases)
                {
                    Console.WriteLine("\t\t" + keyphrase);
                }
            }

            Console.WriteLine("\n\n ==== SENTIMENT ANALYSIS ====");

            SentimentBatchResult result3 = _client.SentimentAsync
                                            (new MultiLanguageBatchInput
                                                (new List<MultiLanguageInput>()
                                                    {
                                                        new MultiLanguageInput("en", "0", "I had the best day of my life"),
                                                        new MultiLanguageInput("en", "1", "This was a nahhhhh for me. The speaker put me to sleep."),
                                                        new MultiLanguageInput("es", "2", "No tengo dinero ni nada que dar..."),
                                                        new MultiLanguageInput("it", "3", "L'hotel veneziano era meraviglioso. È un bellissimo pezzo di architettura.")
                                                    }
                                                )
                                            ).Result;

            foreach (var document in result3.Documents)
            {
                Console.WriteLine("Document ID: {0}, Sentiment score: {1:00}", document.Id, document.Score);
            }
        }
    }
}

        //var cosmosPrayerPut = await CosmosDBPrayerService.PostCosmosPrayerRequestsAsync(cosmosPrayerRequestResultObject);
        //var cosmosPrayerRequestResultObjectId = cosmosPrayerRequestResultObject.Id;


        //public static async Task<HttpResponseMessage> RunGetCosmosPrayerRequestsByIdAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetCosmosPrayerRequestsByIdAsyncFunction/{id}")]HttpRequestMessage req, string id, TraceWriter log)
        //{
        //NOTE - THIS URL REQUIRES ID AT THE END
        // http://localhost:7071/api/GetCosmosPrayerRequestsByIdAsyncFunction/92158276

        //log.Info("C# HTTP trigger function processed a request.");

        //var listCosmosPrayer = await CosmosDBPrayerService.GetCosmosPrayerRequestsByIdAsync(id);

        //if (listCosmosPrayer == null)
        //    return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {id}");

        //return req.CreateResponse(System.Net.HttpStatusCode.OK, listCosmosPrayer);
        // }

        //public static async Task<HttpResponseMessage> RunPostCosmosPrayerRequestsAsyncFunction([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "PostCosmosPrayerRequestsAsyncFunction")]HttpRequestMessage req, TraceWriter log)
        //{
        //    //NOTE - WHEN TESTING VIA POSTMAN, IN THE BODY - YOU WANT TO SET THE CONTENT TO RAW - THEN SET TO JSON 
        //    //THEN MAKE SURE YOU SEND JUST ONE OBJECT LIKE THE CLIP BELOW - NOT AN ARRAY WITH ONE OBJECT (IE. NO SQUARE BRACKETS)
        //    //  {
        //    //    "id": "921582760",
        //    //    "SharedStringId": "921582760",
        //    //    "CreatedDateTimeString": "May 14 11:10 AM",
        //    //    "CreatedDateTime": "2018-05-14T11:10:34.931926-07:00",
        //    //    "StringOnlyDateTime": null,
        //    //    "UpdatedAtString": null,
        //    //    "UpdatedAt": "0001-01-01T00:00:00-08:00",
        //    //    "FirstName": "Andrew",
        //    //    "LastName": "Kim",
        //    //    "FullName": "Andrew Kim",
        //    //    "FullNameAndDate": "Andrew Kim\r\nMarch 1, 2018",
        //    //    "FBProfileUrl": "http://graph.facebook.com/450/picture?type=normal",
        //    //    "PrayerRequestText": "From Postman",
        //    //    "NumberOfThoughts": 7,
        //    //    "NumberOfPrayers": 1,
        //    //    "StringTheNumberOfPrayers": "new and updated commanded",
        //    //    "IsDeleted": false
        //    //  }

        //    log.Info("C# HTTP trigger function processed a request.");


        //    if (cosmosPrayerPut == null)
        //        return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Contact Id not found: Id: {cosmosPrayerRequestResultObjectId}");

        //    return req.CreateResponse(System.Net.HttpStatusCode.OK, cosmosPrayerPut);
        //}



        //    var listCosmosPrayer = await CosmosDBPrayerService.GetAllCosmosPrayerRequests();

        //    if (listCosmosPrayer == null)
        //        return req.CreateResponse(System.Net.HttpStatusCode.BadRequest, $"Did not return Get All Cosmos Prayers Request");

        //    //OPTION 1
        //    return req.CreateResponse(System.Net.HttpStatusCode.OK, listCosmosPrayer);

        //OPTION 2
        //var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
        //responseMessage.Content = new StringContent(returnedSerializedListCosmosPrayer, Encoding.UTF8, "application/json");
        //return responseMessage;
    //}


        //public static async Task<double?> GetSentiment(string text)
        //{
        //    var sentimentDocument = new MultiLanguageBatchInput(new List<MultiLanguageInput> { { new MultiLanguageInput(id: "1", text: text) } });

        //    var sentimentResults = await TextAnalyticsApiClient.SentimentAsync(sentimentDocument).ConfigureAwait(false);

        //    if (sentimentResults?.Errors?.Any() ?? false)
        //    {
        //        var exceptionList = sentimentResults.Errors.Select(x => new Exception($"Id: {x.Id}, Message: {x.Message}"));
        //        throw new AggregateException(exceptionList);
        //    }

        //    var documentResult = sentimentResults?.Documents?.FirstOrDefault();

        //    return documentResult?.Score;
        //}
    //}

    //}      
    //        //AzureRegion = AzureRegions.Westus;

    //        Console.OutputEncoding = System.Text.Encoding.UTF8;

    //        // Extracting language
    //        Console.WriteLine("===== LANGUAGE EXTRACTION ======");

    //        var result = client.DetectLanguageAsync(new BatchInput(
    //                new List<Input>()
    //                    {
    //                      new Input("1", "This is a document written in English."),
    //                      new Input("2", "Este es un document escrito en Español."),
    //                      new Input("3", "这是一个用中文写的文件")
    //                })).Result;

    //        // Printing language results.
    //        foreach (var document in result.Documents)
    //        {
    //            Console.WriteLine("Document ID: {0} , Language: {1}", document.Id, document.DetectedLanguages[0].Name);
    //        }

    //// Getting key-phrases
    //Console.WriteLine("\n\n===== KEY-PHRASE EXTRACTION ======");

    //        KeyPhraseBatchResult result2 = client.KeyPhrasesAsync(new MultiLanguageBatchInput(
    //                    new List<MultiLanguageInput>()
    //                    {
    //                      new MultiLanguageInput("ja", "1", "猫は幸せ"),
    //                      new MultiLanguageInput("de", "2", "Fahrt nach Stuttgart und dann zum Hotel zu Fu."),
    //                      new MultiLanguageInput("en", "3", "My cat is stiff as a rock."),
    //                      new MultiLanguageInput("es", "4", "A mi me encanta el fútbol!")
    //                    })).Result;

    //        // Printing keyphrases
    //        foreach (var document in result2.Documents)
    //        {
    //            Console.WriteLine("Document ID: {0} ", document.Id);

    //            Console.WriteLine("\t Key phrases:");

    //            foreach (string keyphrase in document.KeyPhrases)
    //            {
    //                Console.WriteLine("\t\t" + keyphrase);
    //            }
    //        }

    //        // Extracting sentiment
    //        Console.WriteLine("\n\n===== SENTIMENT ANALYSIS ======");

    //        SentimentBatchResult result3 = client.SentimentAsync(
    //                new MultiLanguageBatchInput(
    //                    new List<MultiLanguageInput>()
    //                    {
    //                      new MultiLanguageInput("en", "0", "I had the best day of my life."),
    //                      new MultiLanguageInput("en", "1", "This was a waste of my time. The speaker put me to sleep."),
    //                      new MultiLanguageInput("es", "2", "No tengo dinero ni nada que dar..."),
    //                      new MultiLanguageInput("it", "3", "L'hotel veneziano era meraviglioso. È un bellissimo pezzo di architettura."),
    //                    })).Result;


    //        // Printing sentiment results
    //        foreach (var document in result3.Documents)
    //        {
    //            Console.WriteLine("Document ID: {0} , Sentiment Score: {1:0.00}", document.Id, document.Score);
    //        }
    //    }
        




        //public static Func<TextAnalyticsAPI> newTextAnalyticsAPI = new Func<TextAnalyticsAPI>
        //(() => new TextAnalyticsAPI
        //    {
        //    AzureRegion = AzureRegions.Southcentralus,
        //    Credentials = "2343242342432423"
        //    }
        //);

        //public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    request.Headers.Add("Ocp-Apim-Subscription-Key", "ENTER KEY HERE");
        //    request base.

        //}




        //ITextAnalyticsAPI client = new TextAnalyticsAPI(new ApiKeyServiceClientCredentials());
        //client.AzureRegion = AzureRegions.Southcentralus;





       // static readonly Lazy<TextAnalyticsAPI> _textAnalyticsApiClientHolder = new Lazy<TextAnalyticsAPI>(newTextAnalyticsAPI);

       // static TextAnalyticsAPI TextAnalyticsApiClient => _textAnalyticsApiClientHolder.Value;



//    }
//}