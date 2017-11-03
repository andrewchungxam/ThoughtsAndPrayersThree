using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using SQLite;

using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.CosmosDB;

namespace ThoughtsAndPrayersThree.LocalData
{
	public class PrayerRequestDatabase
	{
		public PrayerRequestDatabase()
		{
			IfEmptyCheckCosmosDB();
		}

		private void IfEmptyCheckCosmosDB()
		{
			//var list = new List<PrayerRequest> { };
			//         list = this.GetAllPrayerRequests(); 

			//if (!list.Any())
			//{
			//             //                var myListOfCosmosDogs = Task.Run(async () => await CosmosDB.CosmosDBServicePhoto.GetAllCosmosDogs()).Result;

			//             var myListOfPrayerRequests = Task.Run(async () => await ASampleApp.Prayers.CosmosDB.CosmosDBServicePhoto.GetAllCosmosPrayerRequests()).Result; //CosmosDB.CosmosDBServicePhoto   GetAllPrayerRequests()).Result;
			//  foreach (var item in myListOfPrayerRequests)
			//  {
			//      var tempPrayerRequest = CosmosDB.PrayerConverter.ConvertToDog(item);
			//      //this.AddNewPrayerRequest(int id, string createdDateTimeString, DateTimeOffset createdDateTime, string firstName, string lastName, string FullName, string fbProfileUrl, string prayerRequestText);
			//      //this.AddNewPrayerRequest(int id, string createdDateTimeString, DateTimeOffset createdDateTime, string firstName, string lastName, string FullName, string fbProfileUrl, string prayerRequestText);

			//      //                  DO SOMETHING LIKE THIS ON THE VIEW MODEL
			//      //                  App.MyDogListPhotoPage.MyViewModel._observableCollectionOfDogs.Add(tempDog);
			//  }
			//}
		}

		private SQLiteConnection sqliteConnection;

		public PrayerRequestDatabase(string dbPath)
		{
			sqliteConnection = new SQLiteConnection(dbPath);
			sqliteConnection.CreateTable<PrayerRequest>();
		}

		//public void AddNewDogPhoto(string name, string furColor)
		//{
		//  sqliteConnection.Insert(new Dog
		//  {
		//      Name = name,
		//      FurColor = furColor,
		//      //a default dog image for entries via the text only field
		//      DogPictureSource = "https://s-media-cache-ak0.pinimg.com/736x/4b/c2/ac/4bc2acd1af5130a668a4c391805f3f29--teacup-poodle-puppies-teacup-poodles.jpg"
		//  });
		//}

		public void DeletePrayerRequest(PrayerRequest prayerRequest)
		{
			sqliteConnection.Delete(prayerRequest);
		}

		public void DeleteAllPrayerRequests()
		{
			var query = sqliteConnection.Table<PrayerRequest>();   //   Where(v => v.Id > -1);

			foreach (var individualQuery in query)
			{
				sqliteConnection.Delete(individualQuery);
			}
		}

		public void AddNewPrayerRequest(int id, string createdDateTimeString, DateTimeOffset createdDateTime, string firstName, string lastName, string FullName, string fbProfileUrl, string prayerRequestText)
		{
			sqliteConnection.Insert(new PrayerRequest
			{
				Id = id,
				CreatedDateTimeString = createdDateTimeString,
				CreatedDateTime = createdDateTime,
				FirstName = firstName,
				LastName = lastName,
				FullName = FullName,
				FBProfileUrl = fbProfileUrl,
				PrayerRequestText = prayerRequestText
			});
		}

		//public void AddNewDogPhotoFilePhoto(string name, string furColor, string dogFile)
		//{
		//  sqliteConnection.Insert(new Dog { Name = name, FurColor = furColor, DogPictureFile = dogFile });
		//}

		//public void AddNewDogPhotoSourcePhoto(string name, string furColor, string dogSource)
		//{
		//  sqliteConnection.Insert(new Dog { Name = name, FurColor = furColor, DogPictureSource = dogSource });
		//}


		public List<PrayerRequest> GetAllPrayerRequests()
		{
			return sqliteConnection.Table<PrayerRequest>().ToList();
		}

		public PrayerRequest GetFirstPrayerRequests()
		{
			return sqliteConnection.Table<PrayerRequest>().FirstOrDefault();
		}

		public PrayerRequest GetLastPrayerRequests()
		{
			return sqliteConnection.Table<PrayerRequest>().LastOrDefault();
		}
	}
}
