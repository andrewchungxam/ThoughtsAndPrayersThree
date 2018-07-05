using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using ThoughtsAndPrayersThree;
using ThoughtsAndPrayers.Services;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.Services
{
    public class FunctionSentimentService : BaseFunctionSentimentService
    {

        #region Constants
        //AZURE FUNCTION STRING BASE
        public const string AzureFunctionStringBase = AzureFunctionConstants.AzureFunctionBaseUrl;


        //http://localhost:7071/api/GetPrayerRequestSentimentById/921582730

        //RouteGetPrayerRequestSentimentById
        public const string RouteGetPrayerRequestSentimentById = "/api/GetPrayerRequestSentimentById";
        #endregion



        #region Methods
        public static Task<double> FunctionGetPrayerRequestSentimentById(string id)
        => GetPrayerRequestSentimentById($"{AzureFunctionStringBase}{RouteGetPrayerRequestSentimentById}/{id}");
        #endregion 

    }
}
