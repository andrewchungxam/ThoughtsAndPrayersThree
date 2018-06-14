using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public const string ConsoleApplicationFullAccessConnectionString = "Endpoint=sb://samplenotifhubtwonamespace.servicebus.windows.net/;SharedAccessKeyName=ConsoleApplicationFullAccess;SharedAccessKey=DivOPPwrOoRsXhdDpTGBLE+iW4COfUeX4OY96yRVX5Y=";
        public const string ConsoleApplicationNotificationHubName = "MySampleNotificationHub";
    }

    public class AzureNotificationsViaFunctionsURLS
    {
        public const string NativeApiURL = "https://xamarinformsnotificationfunction.azurewebsites.net/" + "api/NotificationFunctionNative";
        public const string TemplateApiURL = "https://xamarinformsnotificationfunction.azurewebsites.net/" + "api/NotificationFunctionTemplate";
        public const string MultipleTemplateApiURL = "https://xamarinformsnotificationfunction.azurewebsites.net/" + "api/NotificationFunctionTemplateMultiple";
    }
}
