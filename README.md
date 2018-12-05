# ThoughtsAndPrayersThree
Thoughts and Prayers mobile application done with Xamarin.Forms, MVVM and CosmosDB

This Xamarin Forms project will be split into distict parts that will build on top of each other.

[Part 1: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-01-ListAndAddButton)
1) a List View 
2) Thought and Prayer Buttons on each cell 
3) Lottie animations
4) Local and global styles
5) Add Request button which adds to the List View

[Part 2: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-02-Clickable-ViewCells) <br />
Part 1 plus
1) a Detail page view with Lottie animation within a Grid
2) Links to the Observable Collection in ListView 

[Part 3: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-03-SQLite) <br /> 
Part 2 plus
1) Saving data via SQLite

[Part 4: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-04-CosmosDB) <br />
Part 3 plus
1) Saving and updating data to Cosmos DB (For clarity/simplicity - only the functionality of Adding and Updating is reflected here - ie. only Post and Put Http verbs are coded in this branch)
2) As you're doing this - please monitor what is happening in your CosmosDB.  Go to your Azure portal > click into the CosmosDB > Click Data Explorer > Click into the Database/Collection which in this case will be Xamarin > PrayerRequests > Click Documents to see the individual entries and notice they are being updated as you click the Thought or Prayer buttons

[Part 5: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-05-CosmosDB-GettingRemoteData) <br />
Part 4 plus
1) Simple check of CosmosDB and downloading data (includes Get/Post/Put HTTP verbs are coded here)

[Part 6: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/blob/Branch-06-SynchronizeLocalAndRemote/README.md)  <br />
Part 5 plus

1) Synchronization at startup between local and remote storage

For clarity and simplicity, all the code prior to Part 5 has been left in the most convenient and obvious places.  However with the introduction of both a web service and a local store - there needs to be some synchronization/coordination.  Code will need to become slightly more modular -- the entry point where you will begin to see this is in the OnAppearing of the ViewModel of the List Page.

[Part 7: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/blob/Branch-07-Functions/README.md) <br />
Part 6 plus

1) Using Functions as an intermediary between Client and CosmosDB

[Part 8: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/blob/Branch-08-AppCenter/README.md) <br />
Part 7 plus

1) Added App Center functionality

[Part 9: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/blob/Branch-09-Text-Sentiment/README.md) <br />
Part 8 plus

1) Incorporated Cognitive Services - results shown in the Detail Page and of course CosmosDB

[Part 10: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-10-NotificationHub) <br />
Part 9 plus

1) Notification Service

[Part 11: Thoughts + Prayers App with:]
<br />
Part 10 plus
1) Getting ready for initial app store submission
