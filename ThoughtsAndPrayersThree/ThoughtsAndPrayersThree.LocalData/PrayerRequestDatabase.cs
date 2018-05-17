using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.CosmosDB;
using SQLite;
using ThoughtsAndPrayersThree.Services;

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
                List<CosmosDBPrayerRequest> listFromCosmosDB = Task.Run(async () => await FunctionPrayerService.GetAllCosmosPrayerRequestsFunction()).Result;

                int intTest = 5;

                //CHECK TO SEE IF COSMOS HAS SOMETHING IN IT
                if(listFromCosmosDB != null && listFromCosmosDB.Any())
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
        public SQLiteConnection SqliteConnectionProperty { get; set; }
        private string _dbPath;

        public PrayerRequestDatabase(string dbPath)
        {
            sqliteConnection = new SQLiteConnection(dbPath);
            sqliteConnection.CreateTable<PrayerRequest>();

            SqliteConnectionProperty = sqliteConnection;
            _dbPath = dbPath;

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

                UpdatedAtString = prayerRequest.UpdatedAtString,
                UpdatedAt = prayerRequest.UpdatedAt,

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
            //NO LONGER NECESSARY WITH THE UPDATE METHOD
            //var getPrayerRequest = this.GetPrayerRequestByIdAsync(prayerRequest);
            //sqliteConnection.Delete(getPrayerRequest);
            //sqliteConnection.Insert(getPrayerRequest);

            sqliteConnection.Update(prayerRequest);

            return;
        }

        public void DeleteItemAsync(PrayerRequest prayerRequest)
        {
            sqliteConnection.Delete(prayerRequest);
            return;
        }


/////////////////////

        #region Constant Fields
        //static readonly Lazy<SQLiteAsyncConnection> _databaseConnectionHolder = new Lazy<SQLiteAsyncConnection(_dbPath)>;

        static readonly SQLiteAsyncConnection _nonLazy_databaseConnectionHolder = new SQLiteAsyncConnection(ThoughtsAndPrayersThree.App.DBPathString);

        //var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(databasePath, storeDateTimeAsTicks: false)));
        //var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

        public static Func<SQLiteAsyncConnection> connectionFunc2 = new Func<SQLiteAsyncConnection>(()=> new SQLiteAsyncConnection(ThoughtsAndPrayersThree.App.DBPathString));
        static readonly Lazy<SQLiteAsyncConnection> _databaseConnectionHolder = new Lazy<SQLiteAsyncConnection>(connectionFunc2);

        #endregion

        #region Fields
        static bool _isInitialized;
        #endregion

        #region Properties
//        static SQLiteAsyncConnection DatabaseConnection => _databaseConnectionHolder.Value;
        static SQLiteAsyncConnection DatabaseConnection 
        { 
            get
            {
                
                return _databaseConnectionHolder.Value; 
            } 
        } 
  

        #endregion

        #region Methods
        protected static async Task<SQLiteAsyncConnection> GetDatabaseConnectionAsync()
        {
            if (!_isInitialized)
                await Initialize();

            return DatabaseConnection;
        }

        static async Task Initialize()
        {
            await DatabaseConnection.CreateTableAsync<PrayerRequest>();
            _isInitialized = true;
        }
        #endregion

        #region Methods
        public static async Task SavePrayerAsync(PrayerRequest prayerRequest)
        {
            var databaseConnection = await GetDatabaseConnectionAsync();
            await databaseConnection.InsertOrReplaceAsync(prayerRequest);
        }

        public static async Task PatchPrayerModelAsync(PrayerRequest prayerRequest)
        {
            var databaseConnection = await GetDatabaseConnectionAsync();
            await databaseConnection.UpdateAsync(prayerRequest);
        }

        public static async Task<int> GetPrayerCountAsync()
        {
            var databaseConnection = await GetDatabaseConnectionAsync();

            return await databaseConnection.Table<PrayerRequest>().CountAsync();
        }

        public static async Task<List<PrayerRequest>> GetAllPrayersAsync()
        {
            var databaseConnection = await GetDatabaseConnectionAsync();

            return await databaseConnection.Table<PrayerRequest>().ToListAsync();
        }

        public static async Task<PrayerRequest> GetPrayerAsync(string id)
        {
            var databaseConnection = await GetDatabaseConnectionAsync();

            return await databaseConnection.Table<PrayerRequest>().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public static async Task DeletePrayerAsync(PrayerRequest prayerRequest)
        {
            var databaseConnection = await GetDatabaseConnectionAsync();

            prayerRequest.IsDeleted = true;

            await databaseConnection.UpdateAsync(prayerRequest);
        }

        #if DEBUG
            public static async Task RemovePrayerAsync(PrayerRequest prayerRequest)
                {
                    var databaseConnection = await GetDatabaseConnectionAsync();

                await databaseConnection.DeleteAsync(prayerRequest);
                }
        #endif  

        #endregion

    }
}
