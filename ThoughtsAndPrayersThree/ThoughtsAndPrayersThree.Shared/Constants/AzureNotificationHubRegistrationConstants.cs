using System;

namespace ThoughtsAndPrayersThree.Shared.Constants
{
    public class AzureNotificationHubRegistrationConstants
    {
        public const string AzureNotificationFunctionBaseUrl = "https://thoughtsandprayersthreefunction.azurewebsites.net";
    }
}

namespace ThoughtsAndPrayersThree.Functions.Constants
{
    public class FunctionConstants
    {
        public static string TextAnalyticsKey = "885c6b53172b442c8877699a539617cf";
    }

    public class AzureNotificationHubConstants
    {
        ////STRING PLACEHOLDERS
        //public const string ConsoleApplicationFullAccessConnectionString = ;
        //public const string ConsoleApplicationNotificationHubName = ;

        //STRINGS
        //public const string ConsoleApplicationFullAccessConnectionString = "Endpoint=sb://samplenotifhubtwonamespace.servicebus.windows.net/;SharedAccessKeyName=ConsoleApplicationFullAccess;SharedAccessKey=DivOPPwrOoRsXhdDpTGBLE+iW4COfUeX4OY96yRVX5Y=";
        //public const string ConsoleApplicationNotificationHubName = "MySampleNotificationHub";

        public const string NotificationHubFullAccessConnectionString = "Endpoint=sb://thandprayerthreenotihubnamespace.servicebus.windows.net/;SharedAccessKeyName=NotificationHubFullAccessConnectionString;SharedAccessKey=iU/Miur4KTrML7l+XUVU/WXO1awdVGj8wsnNTaxgpYE=";
        public const string NotificationHubName = "ThAndPrayerThreeNotiHub";
    }

    public class AzureNotificationsViaFunctionsURLS
    {
        public const string NativeApiURL = "https://thoughtsandprayersthreefunction.azurewebsites.net" + "/api/NotificationFunctionNative";
        public const string TemplateApiURL = "https://thoughtsandprayersthreefunction.azurewebsites.net" + "/api/NotificationFunctionTemplate";
        public const string MultipleTemplateApiURL = "https://thoughtsandprayersthreefunction.azurewebsites.net" + "/api/NotificationFunctionTemplateMultiple";
    }
}

namespace ThoughtsAndPrayersThree
{
    public class AzureFunctionConstants
    {
        public const string AzureFunctionBaseUrl = "https://thoughtsandprayersthreefunction.azurewebsites.net";
    }
}

namespace ThoughtsAndPrayersThree.CosmosDB
{
    public static class CosmosDBConstants
    {

        //#error SIGN UP FOR AZURE - THEN SET THE BELOW KEYS AND THEN COMMENT OUT THIS LINE
        //public static readonly String myEndPoint = "https://YOUR_AZURE_COSMOSDB_NAME.documents.azure.com:443/";
        //public static readonly String myKey = "YOUR_SECRET_KEY_23j9fj932jrh23hr93r29jrj3r2j3rjjdf==";

        public static readonly String myEndPoint = "https://thoughtandprayers.documents.azure.com:443/";
        public static readonly String myKey = "0skBTr3vdf62xx7iWaKEJEGQbDHTouaoIYhj6ZGX51ZT3tfITlbDRUUVCRLsmUy81IgoBHvq2lyc69G3Fbt2XA==";

        //#error Add the following Nuget package to all your platform projects :: Microsoft.Azure.DocumentDb.Core

    }
}

