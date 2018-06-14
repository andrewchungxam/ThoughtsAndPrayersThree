using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using Microsoft.WindowsAzure.Storage.Blob;

using ThoughtsAndPrayersThree.LocalData;

using ThoughtsAndPrayersThree.Pages;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.Constants;

using ThoughtsAndPrayersThree.LocalData;
using ThoughtsAndPrayersThree.Models;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace ThoughtsAndPrayersThree
{
    public class App : Application
    {
        public const string NotificationReceivedKey = "NotificationReceived";

        public static List<PrayerRequest> ListOfPrayers { get; set; } //= FixedPrayerRequests.ListOfPrayerRequests;
        public static LocalData.PrayerRequestDatabase PrayerSQLDatabase { get; set; }

        public static string DBPathString { get; set; } = "";

        public App()
        {
            string dbPath = LocalData.FileAccessHelper.GetLocalFilePath("ThoughtsAndPrayer40.db3");
            DBPathString = dbPath;

            PrayerSQLDatabase = new PrayerRequestDatabase(dbPath);
            //PrayerSQLDatabase.LoadSampleData();
            PrayerSQLDatabase.LoadSampleDataAndCheckForCosmosDB();
            ListOfPrayers = PrayerSQLDatabase.GetAllPrayerRequests(); 

#region Global styles

            var labelStyle = new Style(typeof(Label))
            {

            #if __ANDROID__
                            Setters = 
                                {
                                    new Setter
                                    { 
                                        Property = Label.FontFamilyProperty,   Value = "Droid Sans Mono"
                                    }
                                }
                            };
            #endif

            #if __IOS__
                            Setters =
                                {
                                    new Setter
                                    {
                                        Property = Label.FontFamilyProperty,   Value = "AppleSDGothicNeo-Light"
                                    }
                                }
                        };
            #endif                        

            #if __Windows__
                        Setters = 
                                {
                                    new Setter
                                    { 
                                        Property = Label.FontFamilyProperty,   Value = "Times New Roman"
                                    }
                                }
                            };
            #endif
                        var editorStyle = new Style(typeof(Editor))
                        {
            #if __ANDROID__
                            Setters = 
                                {
                                    new Setter
                                    { 
                                        Property = Label.FontFamilyProperty,   Value = "Droid Sans Mono"
                                    }
                                }
                            };
            #endif

            #if __IOS__
                            Setters =
                                {
                                new Setter
                                {
                                    Property = Label.FontFamilyProperty,
                                    Value = "AppleSDGothicNeo-Light"
                                }
                                }
                        };
            #endif                        

            #if __Windows__
                        Setters = 
                                {
                                    new Setter
                                    { 
                                        Property = Label.FontFamilyProperty,   Value = "Times New Roman"
                                    }
                                }
                            };
            #endif



            Resources = new ResourceDictionary();
            Resources.Add(labelStyle); //THIS IS IMPLICIT
            Resources.Add("editorStyle", editorStyle); //THIS IS IMPLICIT

#endregion


#if __ANDROID__
            MainPage = new NavigationPage(new MainPage());
#endif

#if __IOS__

            var np = new NavigationPage(new PrayerListPage());
            np.BarBackgroundColor = MyColors.MyBlue1;
            np.BarTextColor = Color.White;

            MainPage = np;
#endif
		}

		protected override void OnStart()
		{

            AppCenter.Start(
                "ios=b96ae0ab-a574-4be3-b325-001800d81cf3;" +
                "uwp={Your UWP App secret here};" +
                "android={Your Android App secret here}",
                typeof(Analytics), typeof(Crashes));
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}