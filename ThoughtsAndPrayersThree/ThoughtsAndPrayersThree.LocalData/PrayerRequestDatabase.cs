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
        //THIS WILL NEVER BE HIT
        //public PrayerRequestDatabase()
        //{
        //    LoadSampleData();   //IfEmptyCheckCosmosDB();
        //}

        public void LoadSampleData()  //private void IfEmptyCheckCosmosDB()
        {
            var listFromSQLiteDB = new List<PrayerRequest> { };
            listFromSQLiteDB = this.GetAllPrayerRequests();

            if (!listFromSQLiteDB.Any()) //if LIST == EMPTY
            {
                List<PrayerRequest> sampleListOfFixedPrayerRequests = FixedPrayerRequests.ListOfPrayerRequests;

                foreach (var item in sampleListOfFixedPrayerRequests)
                {
                    this.AddNewPrayerRequest(item);
                }
            }
        }

        public void LoadSampleDataAndCheckForCosmosDB()  
        {
            var listFromSQLiteDB = new List<PrayerRequest> { };
            listFromSQLiteDB = this.GetAllPrayerRequests();

            //IF LOCAL SQL LIST == EMPTY
            if (!listFromSQLiteDB.Any()) 
            {

                //GET WHATEVER YOU HAVE FROM COSMOS INTO A COSMOS DB LIST
                List<CosmosDBPrayerRequest> listFromCosmosDB = Task.Run(async () => await CosmosDBPrayerService.GetAllCosmosPrayerRequests()).Result;

                //CHECK TO SEE IF COSMOS HAS SOMETHING IN IT
                if(listFromCosmosDB.Any())
                {   //IF IT HAS SOMETHING, THEN CONVERT FROM COSMOS TO SQLITE DATA FORMAT AND THEN ADD LOCALLY TO SQLITE     
                    foreach (var cosmosItem in listFromCosmosDB)
                    {
                        var tempPrayer = PrayerRequestConverter.ConvertToPrayerRequest(cosmosItem);
                        this.AddNewPrayerRequest(tempPrayer);
                    }
                } 
                else
                {
                    //IF EMPTY USE THE FIXED SAMPLE PRAYER REQUESTS
                    //USE THE LOCAL SAMPLE DATA --> SAVE IT TO SQLITE
                    this.LoadSampleData();
                }
            }
            return;
        }

        private SQLiteConnection sqliteConnection;

        public PrayerRequestDatabase(string dbPath)
        {
            sqliteConnection = new SQLiteConnection(dbPath);
            sqliteConnection.CreateTable<PrayerRequest>();
        }

        public void DeletePrayerRequest(PrayerRequest prayerRequest)
        {
            sqliteConnection.Delete(prayerRequest);
        }

        public void DeleteAllPrayerRequests()
        {
            var query = sqliteConnection.Table<PrayerRequest>();

            foreach (var individualQuery in query)
            {
                sqliteConnection.Delete(individualQuery);
            }
        }

        public void AddNewPrayerRequest(PrayerRequest prayerRequest)
        {
            sqliteConnection.Insert(new PrayerRequest
            {
                Id = prayerRequest.Id,
                CreatedDateTimeString = prayerRequest.CreatedDateTimeString,
                CreatedDateTime = prayerRequest.CreatedDateTime,
                StringOnlyDateTime = prayerRequest.StringOnlyDateTime,
                FirstName = prayerRequest.FirstName,
                LastName = prayerRequest.LastName,
                FullName = prayerRequest.FullName,
                FullNameAndDate = prayerRequest.FullNameAndDate,
                //GET-ONLY//NewCombinedNameAndDate = newCombinedNameAndDate,
                FBProfileUrl = prayerRequest.FBProfileUrl,
                PrayerRequestText = prayerRequest.PrayerRequestText,
                NumberOfThoughts = prayerRequest.NumberOfThoughts,
                NumberOfPrayers = prayerRequest.NumberOfPrayers,
                //GET-ONLY//CombinedNumberOfThoughtsAndPrayers = combinedNumberOfThoughtsAndPrayers,
                StringTheNumberOfPrayers = prayerRequest.StringTheNumberOfPrayers
            });
        }

        //public void AddNewPrayerRequest
        //(
        //    int id, 
        //    string createdDateTimeString, 
        //    DateTimeOffset createdDateTime, 
        //    string stringOnlyDateTime,
        //    string firstName, 
        //    string lastName, 
        //    string fullName,
        //    string fullNameAndDate,
        //    string newCombinedNameAndDate,
        //    string fbProfileUrl, 
        //    string prayerRequestText,
        //    int numberOfThoughts,
        //    int numberOfPrayers,
        //    string combinedNumberOfThoughtsAndPrayers,
        //    string stringTheNumberOfPrayers 
        //)
        //{
        //    sqliteConnection.Insert(new PrayerRequest
        //    {
        //        Id = id,
        //        CreatedDateTimeString = createdDateTimeString,
        //        CreatedDateTime = createdDateTime,
        //        StringOnlyDateTime = stringOnlyDateTime,
        //        FirstName = firstName,
        //        LastName = lastName,
        //        FullName = fullName,
        //        FullNameAndDate = fullNameAndDate,
        //        //GET-ONLY//NewCombinedNameAndDate = newCombinedNameAndDate,
        //        FBProfileUrl = fbProfileUrl,
        //        PrayerRequestText = prayerRequestText,
        //        NumberOfThoughts = numberOfThoughts,
        //        NumberOfPrayers = numberOfPrayers,
        //        //GET-ONLY//CombinedNumberOfThoughtsAndPrayers = combinedNumberOfThoughtsAndPrayers,
        //        StringTheNumberOfPrayers = stringTheNumberOfPrayers
        //    });
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

        public void UpdateNumberOfPrayers(PrayerRequest cellPrayerRequest)
        {
            this.UpdatePrayerRequest(cellPrayerRequest);
            return;
        }

        public void UpdateNumberOfThoughts(PrayerRequest cellPrayerRequest)
        {
            this.UpdatePrayerRequest(cellPrayerRequest);
            return;
        }

        public PrayerRequest GetPrayerRequestByIdAsync(PrayerRequest prayerRequest)
        {
            return sqliteConnection.Table<PrayerRequest>().Where(x => x.Id.Equals(prayerRequest.Id)).FirstOrDefault();
        }

        public void UpdatePrayerRequest(PrayerRequest prayerRequest)
        {
            //var getPrayerRequest = this.GetPrayerRequestByIdAsync(prayerRequest);
            //sqliteConnection.Insert(getPrayerRequest);
            //sqliteConnection.Delete(getPrayerRequest);

            sqliteConnection.Update(prayerRequest);

            return;
        }

        public void DeleteItemAsync(PrayerRequest prayerRequest)
        {
            sqliteConnection.Delete(prayerRequest);
            return;
        }

    }
}
