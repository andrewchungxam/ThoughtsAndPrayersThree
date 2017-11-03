using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Crashes;
using Microsoft.Azure.Mobile.Analytics;

using Microsoft.WindowsAzure.Storage.Blob;

using ThoughtsAndPrayersThree.LocalData;

using ThoughtsAndPrayersThree.Pages;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.Constants;
using ThoughtsAndPrayersThree.BlobStorage;

using ThoughtsAndPrayersThree.LocalData;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree
{
	public class App : Application
	{
        //public static DogRepository DogRep { get; private set; }
//#TODO
        //		public static DogPhotoRepository DogPhotoRep { get; set; }
        //public static DogRepositoryBaseSixtyFour DogRepBaseSixtyFour { get; private set; }
        //public static DogRepositoryBlob DogRepBlob { get; private set; }

        //public static DogListMVVMPage MyDogListMVVMPage { get; set; }
        //public static DogListPhotoPage MyDogListPhotoPage { get; set; }
        //public static DogListPhotoBase64Page MyDogListPhotoBase64Page { get; set; }
        //public static DogListPhotoBlobPage MyDogListPhotoBlobPage { get; set; }

        //public static CloudBlobClient MyCloudBlobClient { get; set; }
        //public static CloudBlobContainer MyCloudBlobContainer { get; set; }
        //public static AzureBlobStorage MyAzureBlobStorage { get; set; }
        //public static String ContainerName = "my10container";

        public static List<PrayerRequest> ListOfPrayers { get; set; } = FixedPrayerRequests.ListOfPrayerRequests;

        public static LocalData.PrayerRequestDatabase PrayerSQLDatabase { get; set; } 

		public static HttpClient myHttpClient;

		public App()
		{

			//HTTPClient
			myHttpClient = new HttpClient();

			////MAKE THIS ASYNC AND PULL THIS OUT OF THE CONSTRUCTOR
			//MyAzureBlobStorage = new AzureBlobStorage();
			//MyCloudBlobClient = MyAzureBlobStorage.CreateCloudBlobClient();
			//MyCloudBlobContainer = MyAzureBlobStorage.CreateCloudBlobClientAndContainer(ContainerName);

			string dbPath = LocalData.FileAccessHelper.GetLocalFilePath("ThoughtsAndPrayer1.db3");

			////USE THIS FOR LIST PHOTO PAGE
			//#TODO 
//			DogPhotoRep = new DogPhotoRepository(dbPath);

            //NAVIGATE TO YOUR FIRST PAGE
            //         var applicationStartPage = new MyPage();
            //var myNavigationPage = new NavigationPage(applicationStartPage);
            //MainPage = myNavigationPage;

#if __ANDROID__
            MainPage = new NavigationPage(new MainPage());
#endif

#if __IOS__

            //MainPage = new ThoughtsAndPrayersThree.MainPage();
            //MainPage = new ThoughtsAndPrayersThree.MainPage();
            MainPage = new NavigationPage(new PrayerListPage());
#endif

			//ADDING STYLES TO RESOURCE DICTIONARIES
//			Resources = new ResourceDictionary();
			//IMPLICIT 
//			Resources.Add(StyleDictionary.navigationPageStyle);

			//ANDROID

			//DogRepBlob = new DogRepositoryBlob(dbPath);


			////Initialize Dog Photo View Page
			//MyDogListMVVMPage = new DogListMVVMPage();
			//MyDogListPhotoPage = new DogListPhotoPage();
			//MyDogListPhotoBase64Page = new DogListPhotoBase64Page();
			//MyDogListPhotoBlobPage = new DogListPhotoBlobPage();

		}


		protected override void OnStart()
		{
			MobileCenter.Start(ASampleAppConstants.MobileCenterIOS +
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





//using System;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Collections.Generic;

//using Xamarin.Forms;

//using Microsoft.Azure.Mobile;
//using Microsoft.Azure.Mobile.Crashes;
//using Microsoft.Azure.Mobile.Analytics;
//using Microsoft.WindowsAzure.Storage.Blob;

//using ASampleApp.Data;
//using ASampleApp.Pages;
//using ASampleApp.Models;
//using ASampleApp.Constants;
//using ASampleApp.BlobStorage;

//namespace ASampleApp
//{
//    public class App : Application
//    {

////        public static DogRepository DogRep { get; private set; }
//        public static DogPhotoRepository DogPhotoRep { get; set; }
//        //public static DogRepositoryBaseSixtyFour DogRepBaseSixtyFour { get; private set; }
//        public static DogRepositoryBlob DogRepBlob { get; private set; }

//        public static DogListMVVMPage MyDogListMVVMPage { get; set; }
//        public static DogListPhotoPage MyDogListPhotoPage { get; set; }
//        public static DogListPhotoBase64Page MyDogListPhotoBase64Page  { get;set;}
//        public static DogListPhotoBlobPage MyDogListPhotoBlobPage { get; set; }



//        public static CloudBlobClient MyCloudBlobClient { get; set; }

//        public static CloudBlobContainer MyCloudBlobContainer { get; set; }

//        public static AzureBlobStorage MyAzureBlobStorage { get; set; }

//        public static String ContainerName = "my10container";

//        public static HttpClient myHttpClient;

//        public App ()
//        {
//            //HTTPClient
//            myHttpClient = new HttpClient();

//            //MAKE THIS ASYNC AND PULL THIS OUT OF THE CONSTRUCTOR
//            MyAzureBlobStorage = new AzureBlobStorage();
//            MyCloudBlobClient = MyAzureBlobStorage.CreateCloudBlobClient();
//            MyCloudBlobContainer = MyAzureBlobStorage.CreateCloudBlobClientAndContainer(ContainerName);

//            string dbPath = FileAccessHelper.GetLocalFilePath("asa17dog12.db3");
//            //USE THIS FOR LIST PAGE
//            //DogRep = new DogRepository(dbPath);
//            ////USE THIS FOR LIST PHOTO PAGE
//            //DogPhotoRep = new DogPhotoRepository(dbPath);
//            ////USE THIS FOR LIST PHOTO BASE SIXTY FOUR PAGE
//            //DogRepBaseSixtyFour = new DogRepositoryBaseSixtyFour(dbPath);

//            DogRepBlob = new DogRepositoryBlob(dbPath);

//            var applicationStartPage = new FirstPage ();

//            var myNavigationPage = new NavigationPage(applicationStartPage);

//            MainPage = myNavigationPage;

//            //Initialize Dog Photo View Page
//            MyDogListMVVMPage = new DogListMVVMPage();
//            MyDogListPhotoPage = new DogListPhotoPage();
//            MyDogListPhotoBase64Page = new DogListPhotoBase64Page();
//            MyDogListPhotoBlobPage = new DogListPhotoBlobPage();

//        }


//        protected override void OnStart ()
//        {
//            // Handle when your app starts
//            //MobileCenter.Start("ios=294968e2-d712-43fc-a8bd-1492ed9ea29c;" +
//            //"uwp={Your UWP App secret here};" +
//            //"android={Your Android App secret here}",
//            //typeof(Analytics), typeof(Crashes));

//            MobileCenter.Start(ASampleAppConstants.MobileCenterIOS +
//                   "uwp={Your UWP App secret here};" +
//                   "android={Your Android App secret here}",
//                   typeof(Analytics), typeof(Crashes));
//        }

//        protected override void OnSleep ()
//        {
//            // Handle when your app sleeps
//        }

//        protected override void OnResume ()
//        {
//            // Handle when your app resumes
//        }
//    }
//}
