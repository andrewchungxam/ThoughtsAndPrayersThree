using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.LocalData;
using ThoughtsAndPrayersThree.CosmosDB;

namespace ThoughtsAndPrayersThree.Services
{
    public static class DatabaseSyncService
    {
        public static async Task SyncRemoteAndLocalDatabases()
        {
            var (contactListFromLocalDatabase, contactListFromRemoteDatabase) = await GetAllSavedContacts().ConfigureAwait(false);

            //SAVING NEW ITEMS
            var (contactsInLocalDatabaseButNotStoredRemotely, contactsInRemoteDatabaseButNotStoredLocally, contactsInBothDatabases) 
                = GetMatchingModels(contactListFromLocalDatabase, contactListFromRemoteDatabase);

            //PATCHING UPDATED ITEMS
            var (contactsToPatchToLocalDatabase, contactsToPatchToRemoteDatabase) 
                = GetModelsThatNeedUpdating(contactListFromLocalDatabase, contactListFromRemoteDatabase, contactsInBothDatabases);

            await SaveContacts( contactsToPatchToRemoteDatabase,
                                contactsToPatchToLocalDatabase,
                                contactsInRemoteDatabaseButNotStoredLocally,   // contactsInRemoteDatabaseButNotStoredLocally.Concat(contactsToPatchToLocalDatabase).ToList(),
                                contactsInLocalDatabaseButNotStoredRemotely).ConfigureAwait(false);
        }

        static async Task<(List<PrayerRequest> contactListFromLocalDatabase, List<PrayerRequest> contactListFromRemoteDatabase)> 
        GetAllSavedContacts()
        {
            var contactListFromLocalDatabaseTask = PrayerRequestDatabase.GetAllPrayersAsync();                                          //var contactListFromLocalDatabaseTask = ThoughtsAndPrayersThree.App.PrayerSQLDatabase.GetAllPrayerRequests();
            var contactListFromRemoteDatabaseTask = FunctionPrayerService.GetAllCosmosPrayerRequestsConvertedToPrayerRequestsFunction();        //APIService.GetAllContactModels();

            await Task.WhenAll(contactListFromLocalDatabaseTask, contactListFromRemoteDatabaseTask).ConfigureAwait(false);

            return (await contactListFromLocalDatabaseTask.ConfigureAwait(false) ?? new List<PrayerRequest>(),
                    await contactListFromRemoteDatabaseTask.ConfigureAwait(false) ?? new List<PrayerRequest>());
                    //await contactListFromRemoteDatabaseTask.ConfigureAwait(false) ?? new List<CosmosDBPrayerRequest>());
        }

        //LET'S T == PRAYER REQUEST
        static (List<T> contactsInLocalDatabaseButNotStoredRemotely, 
                List<T> contactsInRemoteDatabaseButNotStoredLocally, 
                List<T> contactsInBothDatabases) 

        GetMatchingModels<T>(List<T> modelListFromLocalDatabase, List<T> modelListFromRemoteDatabase) 

            where T : IBaseModel
        {
            var modelIdFromLocalDatabaseList = modelListFromLocalDatabase?.Select(x => x.SharedStringId).ToList() ?? new List<string>();
            var modelIdFromRemoteDatabaseList = modelListFromRemoteDatabase?.Select(x => x.SharedStringId).ToList() ?? new List<string>();

            var modelIdsInLocalDatabaseButNotStoredRemotely = modelIdFromLocalDatabaseList?.Except(modelIdFromRemoteDatabaseList)?.ToList() ?? new List<string>();
            var modelIdsInRemoteDatabaseButNotStoredLocally = modelIdFromRemoteDatabaseList?.Except(modelIdFromLocalDatabaseList)?.ToList() ?? new List<string>();
            var modelIdsInBothDatabases = modelIdFromRemoteDatabaseList?.Where(x => modelIdFromLocalDatabaseList?.Contains(x) ?? false).ToList() ?? new List<string>();

            var modelsInLocalDatabaseButNotStoredRemotely = modelListFromLocalDatabase?.Where(x => modelIdsInLocalDatabaseButNotStoredRemotely?.Contains(x?.SharedStringId) ?? false).ToList() ?? new List<T>();
            var modelsInRemoteDatabaseButNotStoredLocally = modelListFromRemoteDatabase?.Where(x => modelIdsInRemoteDatabaseButNotStoredLocally?.Contains(x?.SharedStringId) ?? false).ToList() ?? new List<T>();

            var modelsInBothDatabases = modelListFromLocalDatabase?.Where(x => modelIdsInBothDatabases?.Contains(x?.SharedStringId) ?? false)
                                            .ToList() ?? new List<T>();

            return (modelsInLocalDatabaseButNotStoredRemotely ?? new List<T>(),
                    modelsInRemoteDatabaseButNotStoredLocally ?? new List<T>(),
                    modelsInBothDatabases ?? new List<T>());
        }


        //LET'S T == PRAYERREQUEST
        static (List<T> contactsToPatchToLocalDatabase,List<T> contactsToPatchToRemoteDatabase) 

        GetModelsThatNeedUpdating<T>(
            List<T> modelListFromLocalDatabase,
            List<T> modelListFromRemoteDatabase,
            List<T> modelsFoundInBothDatabases) where T : IBaseModel
        {
            var modelsToPatchToRemoteDatabase = new List<T>();
            var modelsToPatchToLocalDatabase = new List<T>();
            foreach (var contact in modelsFoundInBothDatabases)
            {
                var modelFromLocalDatabase = modelListFromLocalDatabase.Where(x => x.SharedStringId.Equals(contact.SharedStringId)).FirstOrDefault();
                var modelFromRemoteDatabase = modelListFromRemoteDatabase.Where(x => x.SharedStringId.Equals(contact.SharedStringId)).FirstOrDefault();

                //IF LOCAL ITEM IS LATER THEN PATCH REMOTE TIME
                // https://msdn.microsoft.com/en-us/library/system.datetimeoffset.compareto(v=vs.110).aspx
                // Greater than zero - The current DateTimeOffset object is later than other.
                if (modelFromLocalDatabase?.UpdatedAt.CompareTo(modelFromRemoteDatabase?.UpdatedAt ?? default(DateTimeOffset)) > 0)
                    modelsToPatchToRemoteDatabase.Add(contact);

                //IF LOCAL TIME IS EARLIER THEN PATCH LOCAL ITEM
                // < for earlier
                else if (modelFromLocalDatabase?.UpdatedAt.CompareTo(modelFromRemoteDatabase?.UpdatedAt ?? default(DateTimeOffset)) < 0)
                    modelsToPatchToLocalDatabase.Add(contact);
            }

            return (modelsToPatchToLocalDatabase ?? new List<T>(),
                    modelsToPatchToRemoteDatabase ?? new List<T>());
        }

        static Task SaveContacts(List<PrayerRequest> contactsToPatchToRemoteDatabase,
                                 List<PrayerRequest> contactsToPatchToLocalDatabase,
                                 List<PrayerRequest> contactsToAddToLocalDatabase,
                                 List<PrayerRequest> contactsToPostToRemoteDatabase)
        {
            var saveContactTaskList = new List<Task>();

            foreach (var contact in contactsToPatchToRemoteDatabase)
            {
                //COSMOS
                //saveContactTaskList.Add(APIService.PatchContactModel(contact));saveContactTaskList.Add(CosmosDBPrayerService.PatchAndConvertPrayerRequestsAsync(contact));              
                //saveContactTaskList.Add(FunctionPrayerService.PostAndConvertPrayerRequestsAsyncFunction(contact));         
                saveContactTaskList.Add(FunctionPrayerService.PatchAndConvertPrayerRequestAsyncFunction(contact));                  
            }

            foreach (var contact in contactsToPatchToLocalDatabase)
            {
                //SQLITE
                //saveContactTaskList.Add(ContactDatabase.PatchContactModel(contact));
                saveContactTaskList.Add(PrayerRequestDatabase.PatchPrayerModelAsync(contact));
            }

            foreach (var contact in contactsToAddToLocalDatabase)
            {
                //SQLITE
                //saveContactTaskList.Add(ContactDatabase.SaveContact(contact));
                saveContactTaskList.Add(PrayerRequestDatabase.SavePrayerAsync(contact));
            }

            foreach (var contact in contactsToPostToRemoteDatabase)
            {
                //COSMOS
                //saveContactTaskList.Add(APIService.PostContactModel(contact));
                saveContactTaskList.Add(FunctionPrayerService.PostAndConvertPrayerRequestsAsyncFunction(contact));              //PostContactModel(contact));
            }

            return Task.WhenAll(saveContactTaskList);
        }
    }
}
