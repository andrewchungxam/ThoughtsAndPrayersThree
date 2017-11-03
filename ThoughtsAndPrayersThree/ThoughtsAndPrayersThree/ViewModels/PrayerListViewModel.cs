using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.CosmosDB;

using ThoughtsAndPrayersThree.ViewModels.Base;

namespace ThoughtsAndPrayersThree.ViewModels
{
	public class PrayerListViewModel : BaseViewModel
	{

		bool _isTheViewVisible;

		public bool IsTheViewVisible
		{
			get { return _isTheViewVisible; }
			set { SetProperty(ref _isTheViewVisible, value); }
		}

		bool _isTheView1Visible;

		public bool IsTheView1Visible
		{
			get { return _isTheView1Visible; }
			set { SetProperty(ref _isTheView1Visible, value); }
		}


		string buttonText = "Winner Winner";

		public string ButtonText
		{
			get { return buttonText; }
			set { SetProperty(ref buttonText, value); }
		}

		public ICommand DeletePrayerFromListCommand { get; set; }
		public ICommand ThoughtClickCommand { get; set; }
        public ICommand PrayerClickCommand { get; set; }

		public ObservableCollection<PrayerRequest> _observableCollectionOfPrayers;
		public ObservableCollection<PrayerRequest> ObservableCollectionOfPrayers
		{
			get { return _observableCollectionOfPrayers; }
			set { SetProperty(ref _observableCollectionOfPrayers, value); }
		}

		public PrayerListViewModel()
		{
			var list = new List<PrayerRequest> { };
            //list = App.PrayerSQLDatabase.GetAllDogs();
            list = App.ListOfPrayers;

			_observableCollectionOfPrayers = new ObservableCollection<PrayerRequest>();
			foreach (var prayer in list)
				_observableCollectionOfPrayers.Add(prayer);

			DeletePrayerFromListCommand = new Command(DeletePrayerFromListAction);
            ThoughtClickCommand = new Command(OnThoughtClickActionAsync);
            PrayerClickCommand = new Command(OnPrayerClickActionAsync);

		}


        /* Unmerged change from project 'ASampleApp.iOS'
        Before:
                void OnThoughtClickAction()
                {
                    if (this.IsTheViewVisible == false)
                    {

                        this.IsTheViewVisible = true;
                        //await Task.Delay(200); //Animation = "checked_done_.json",
                        //this.MyViewModel.PlayTheLottie = true;
                        this._animation.Play();

                        await Task.Delay(2100); //    Animation = "beating_heart.json",
                                                //await Task.Delay(1400); //    Animation = "like_button.json",
                        this.IsTheViewVisible = false;

                    }
                    else
                    {
                        this.IsTheViewVisible = false;
                    }

                    //#TODO - must register the click somewhere
                    ////point 1
                    ////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
                    ////point 2
                    //App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
                    //string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
                    //string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
                    //this.FirstLabel = _lastNameStringAdd;
                    ////ADD THE LAST DOG TO THE ViewModel
                    //var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
                    //App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
                    return;
                }


                private void DeleteAllDogsFromList()
        After:
                async Task OnThoughtClickActionAsync()
                {
                    if (this.IsTheViewVisible == false)
                    {

                        this.IsTheViewVisible = true;
                        //await Task.Delay(200); //Animation = "checked_done_.json",
                        //this.MyViewModel.PlayTheLottie = true;
                        this._animation.Play();

                        await Task.Delay(2100); //    Animation = "beating_heart.json",
                                                //await Task.Delay(1400); //    Animation = "like_button.json",
                        this.IsTheViewVisible = false;

                    }
                    else
                    {
                        this.IsTheViewVisible = false;
                    }

                    //#TODO - must register the click somewhere
                    ////point 1
                    ////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
                    ////point 2
                    //App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
                    //string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
                    //string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
                    //this.FirstLabel = _lastNameStringAdd;
                    ////ADD THE LAST DOG TO THE ViewModel
                    //var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
                    //App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
                    return;
                }


                private void DeleteAllDogsFromList()
        */
        void OnThoughtClickActionAsync()
		{
			if (this.IsTheViewVisible == false)
			{

				this.IsTheViewVisible = true;
				//await Task.Delay(200); //Animation = "checked_done_.json",
				//this.MyViewModel.PlayTheLottie = true;
				//this._animation.Play();

				Task.Delay(2100); //    Animation = "beating_heart.json",
										//await Task.Delay(1400); //    Animation = "like_button.json",
				this.IsTheViewVisible = false;

			}
			else
			{
				this.IsTheViewVisible = false;
			}

            int five = 5;

            //#TODO - must register the click somewhere
			////point 1
			////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
			////point 2
			//App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
			//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
			//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
			//this.FirstLabel = _lastNameStringAdd;
			////ADD THE LAST DOG TO THE ViewModel
			//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
			//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
			return;
		}



		void OnPrayerClickActionAsync()
		{
			if (this.IsTheViewVisible == false)
			{

				this.IsTheViewVisible = true;
				//await Task.Delay(200); //Animation = "checked_done_.json",
				//this.MyViewModel.PlayTheLottie = true;
				//this._animation.Play();

				Task.Delay(2100); //    Animation = "beating_heart.json",
								  //await Task.Delay(1400); //    Animation = "like_button.json",
				this.IsTheViewVisible = false;

			}
			else
			{
				this.IsTheViewVisible = false;
			}

			int five = 5;

			//#TODO - must register the click somewhere
			////point 1
			////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
			////point 2
			//App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
			//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
			//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
			//this.FirstLabel = _lastNameStringAdd;
			////ADD THE LAST DOG TO THE ViewModel
			//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
			//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
			return;
		}


		private void DeleteAllDogsFromList()
		{
			Debug.WriteLine("DELETE ALL DOGS FROM LIST ACTION");
			var allPrayers = _observableCollectionOfPrayers;

			foreach (var prayer in allPrayers)
			{
				DeletePrayerFromListAction(prayer);
			};

			//var myItem = obj as Dog;
			//Debug.WriteLine($"Removing dog {myItem}");
			//if (_observableCollectionOfDogs.Remove(myItem))
			//{
			//  var myCosmosDog = DogConverter.ConvertToCosmosDog(myItem);
			//  await CosmosDBService.DeleteCosmosDogAsync(myCosmosDog);
			//}
			//else
			//{
			//  Debug.WriteLine($"Dog not reomved from observable collection {myItem}");
			//}
		}

		private async void DeletePrayerFromListAction(object obj)
		{
			Debug.WriteLine("DELETE PRAYER FROM LIST ACTION");
			var myPrayer = obj as PrayerRequest;
			Debug.WriteLine($"Removing Prayer {myPrayer}");

			if (_observableCollectionOfPrayers.Remove(myPrayer))
			{
                var myCosmosPrayer = CosmosDB.PrayerRequestConverter.ConvertToCosmosPrayerRequest(myPrayer);
                await CosmosDBPrayerService.DeleteCosmosPrayerRequestsAsync(myCosmosPrayer);
			}
			else
			{
				Debug.WriteLine($"Prayer not removed from observable collection {myPrayer}");
			}
		}
	}
}


//using System;
//using System.Diagnostics;
//using System.Windows.Input;
//using System.Threading.Tasks;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;

//using Xamarin.Forms;

//using ASampleApp.Models;
//using ASampleApp.CosmosDB;

//namespace ThoughtsAndPrayersThree.ViewModels
//{
//	public class PrayerListViewModel : BaseViewModel
//	{
//		public ObservableCollection<Dog> _observableCollectionOfDogs;

//		public ICommand DeleteDogFromListCommand { get; set; }

//		public ObservableCollection<Dog> ObservableCollectionOfDogs
//		{
//			get { return _observableCollectionOfDogs; }
//			set { SetProperty(ref _observableCollectionOfDogs, value); }
//		}

//		public DogListMVVMViewModel()
//		{

//			//https://stackoverflow.com/questions/5561156/convert-listt-to-observablecollectiont-in-wp7
//			var list = new List<Dog> { };
//			list = App.DogRep.GetAllDogs();

//			_observableCollectionOfDogs = new ObservableCollection<Dog>();
//			foreach (var item in list)
//				_observableCollectionOfDogs.Add(item);

//			DeleteDogFromListCommand = new Command(DeleteDogFromListAction);
//		}

//		private void DeleteAllDogsFromList()
//		{
//			//var myItem = obj as Dog;
//			//Debug.WriteLine($"Removing dog {myItem}");
//			//if (_observableCollectionOfDogs.Remove(myItem))
//			//{
//			//  var myCosmosDog = DogConverter.ConvertToCosmosDog(myItem);
//			//  await CosmosDBService.DeleteCosmosDogAsync(myCosmosDog);
//			//}
//			//else
//			//{
//			//  Debug.WriteLine($"Dog not reomved from observable collection {myItem}");
//			//}

//			Debug.WriteLine("DELETE ALL DOGS FROM LIST ACTION");
//			var allDogs = _observableCollectionOfDogs;

//			foreach (var dog in allDogs)
//			{
//				DeleteDogFromListAction(dog);
//			};
//		}

//		private async void DeleteDogFromListAction(object obj)
//		{
//			Debug.WriteLine("DELETE DOG FROM LIST ACTION");
//			var myItem = obj as Dog;
//			Debug.WriteLine($"Removing dog {myItem}");

//			if (_observableCollectionOfDogs.Remove(myItem))
//			{
//				var myCosmosDog = DogConverter.ConvertToCosmosDog(myItem);
//				await CosmosDBService.DeleteCosmosDogAsync(myCosmosDog);
//			}
//			else
//			{
//				Debug.WriteLine($"Dog not reomved from observable collection {myItem}");
//			}
//		}


//	}
//}

