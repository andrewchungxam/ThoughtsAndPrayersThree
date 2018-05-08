# ThoughtsAndPrayersThree
Thoughts and Prayers done with MVVM and CosmosDB

This Xamarin Forms project will be split into distict parts that will build on top of each other.

[Part 1: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-01-ListAndAddButton)
1) a List View 
2) Thought and Prayer Buttons on each cell 
3) Lottie animations
4) Local and global styles
5) Add Request button which adds to the List View

[Part 2: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-02-Clickable-ViewCells)
Part 1 plus
1) a Detail page view with Lottie animation within a Grid
2) Links to the Observable Collection in ListView 

[Part 3: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-03-SQLite)
Part 2 plus
1) Saving data via SQLite

[Part 4: Thoughts + Prayers App with:](https://github.com/andrewchungxam/ThoughtsAndPrayersThree/tree/Branch-03-SQLite)
Part 3 plus
1) Saving data to Cosmos DB
2) Synchronization at startup between local and remote storage

For clarity and simplicity, all the code has been put into the most convenient and obvious places.  However with the introduction of both a web service and a local store - there needs to be some synchronization/coordination.  Code will need to become slightly more modular -- the entry point where you will begin to see this is in the OnAppearing of View Model of the List Page.
