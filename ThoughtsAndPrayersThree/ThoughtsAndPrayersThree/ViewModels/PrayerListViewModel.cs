using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.CosmosDB;

using ThoughtsAndPrayersThree.ViewModels.Base;
using ThoughtsAndPrayersThree.Pages.ViewCells;

using ThoughtsAndPrayersThree.Pages;

namespace ThoughtsAndPrayersThree.ViewModels
{
    public class ResetableObservableCollection<T> : ObservableCollection<T>
    {
        public void Reset() => this.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    }

    public class PrayerListViewModel : BaseViewModel
    {
        
        bool _isTheThoughtAnimationVisible;
        public bool IsTheThoughtAnimationVisible 
        {
            get { return _isTheThoughtAnimationVisible; } 
            set { SetProperty(ref _isTheThoughtAnimationVisible, value); }
        }

        bool _isThePrayerAnimationVisible;
        public bool IsThePrayerAnimationVisible
        {
            get { return _isThePrayerAnimationVisible; }
            set { SetProperty(ref _isThePrayerAnimationVisible, value); }
        }

        public ObservableCollection<PrayerRequest> _observableCollectionOfPrayers;
        public ObservableCollection<PrayerRequest> ObservableCollectionOfPrayers
        {
            get { return _observableCollectionOfPrayers; }
            set { SetProperty(ref _observableCollectionOfPrayers, value); }
        }

        int _theNumberOfThoughts;
        public int TheNumberOfThoughts
        {
            get { return TheNumberOfThoughts; }
            set { SetProperty(ref _theNumberOfThoughts, value); 
                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
            }
        }

        int _theNumberOfPrayers;
        public int TheNumberOfPrayers
        {
            get { return TheNumberOfPrayers; }
            set { SetProperty(ref _theNumberOfPrayers, value); 
                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
            }
        }

        //STRING_TEST
        string _stringTheNumberOfPrayers;
        public string StringTheNumberOfPrayers
        {
            get { return StringTheNumberOfPrayers; }
            set { SetProperty(ref _stringTheNumberOfPrayers, value); }
        }

        string _combinedNumberOfThoughtsAndPrayers;
        public string CombinedNumberOfThoughtsAndPrayers
        {
            get { return CombinedNumberOfThoughtsAndPrayers; }
            set { SetProperty(ref _combinedNumberOfThoughtsAndPrayers, value); }
        }

        public ResetableObservableCollection<PrayerRequest> MyObservableCollectionOfUnderlyingData
        {
            get;
            set;
        } = new ResetableObservableCollection<PrayerRequest>();

        public void ResetDataSource()
        {
            MyObservableCollectionOfUnderlyingData.Reset();
        }


        public EventHandler<PhotoSavedSuccessAlertEventArgs> TakePhotoSucceeded;
        public class PhotoSavedSuccessAlertEventArgs : EventArgs
        {
            public string Title { get; set; }
            public string Message { get; set; }
        }

        public EventHandler<ThoughtButtonPressedEventArgs> ThoughtButtonPressed;
        public class ThoughtButtonPressedEventArgs : EventArgs
        {
            public string EventArg1 { get; set; }
            public string EventArg2 { get; set; }
        }

        public EventHandler<PrayerButtonPressedEventArgs> PrayerButtonPressed;
        public class PrayerButtonPressedEventArgs : EventArgs
        {
            public string EventArg1 { get; set; }
            public string EventArg2 { get; set; }
        }

        public ICommand DeletePrayerFromListCommand { get; set; }
        public ICommand ThoughtClickCommand { get; set; }
        public ICommand PrayerClickCommand { get; set; }
        public ICommand AddThoughtClickCommand { get; set; }
        public ICommand AddPrayerClickCommand { get; set; }


        public PrayerListViewModel()
        {

            PrayerViewCell.ParentViewModel = this;
            AddTapPage.ParentViewModel = this;

            var list = new List<PrayerRequest> { };
            //list = App.PrayerSQLDatabase.GetAllDogs();
            list = App.ListOfPrayers;

            foreach (var prayerRequest in list)
                MyObservableCollectionOfUnderlyingData.Add(prayerRequest);

            DeletePrayerFromListCommand = new Command(DeletePrayerFromListAction);

            ThoughtClickCommand = new Command(
                execute: async () => { await OnThoughtClickActionAsync(); });
            //  canExecute: () => !IsBusy);


            //ThoughtClickCommand = new Command <PrayerRequest>(OnThoughtClickActionAsync2);


            PrayerClickCommand = new Command(
                execute: async() => { await OnPrayerClickActionAsync(); });

            AddThoughtClickCommand = new Command <PrayerRequest> (OnAddThoughtClickActionAsync);


            AddPrayerClickCommand = new Command<PrayerRequest>(OnAddPrayerClickActionAsync);

        }


        async Task OnThoughtClickActionAsync()
        {
            if (this.IsTheThoughtAnimationVisible == false)
            {
                this.IsTheThoughtAnimationVisible = true;
                ThoughtButtonPressed?.Invoke(this, new ThoughtButtonPressedEventArgs { EventArg1 = "Event arg 1", EventArg2 = "Event arg 2" });
                await Task.Delay(2100);
                this.IsTheThoughtAnimationVisible = false;
            }
            else
            {
                this.IsTheThoughtAnimationVisible = false;
            }
            return;

        }

        async Task OnPrayerClickActionAsync()
        {
            if (this.IsThePrayerAnimationVisible == false)
            {
                this.IsThePrayerAnimationVisible = true;
                PrayerButtonPressed?.Invoke(this, new PrayerButtonPressedEventArgs { EventArg1 = "Event arg 3", EventArg2 = "Event arg 4" });
                await Task.Delay(2100); 
                this.IsThePrayerAnimationVisible = false;
            }
            else
            {
                this.IsThePrayerAnimationVisible = false;
            }
            return;

        }

        void OnAddThoughtClickActionAsync(PrayerRequest cellPrayerRequest)
        {
            if (cellPrayerRequest != null)
            {
                cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
                cellPrayerRequest.NumberOfThoughts = cellPrayerRequest.NumberOfThoughts + 1;
                this.ResetDataSource();

                this.OnThoughtClickActionAsync();
            }

            return;
        }


        void OnAddPrayerClickActionAsync(PrayerRequest cellPrayerRequest)
        {
  

            if (cellPrayerRequest != null)
            {
                cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
                cellPrayerRequest.NumberOfPrayers = cellPrayerRequest.NumberOfPrayers + 1;
                this.ResetDataSource();

                this.OnPrayerClickActionAsync();

            }
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
//using System.Linq;

//using Xamarin.Forms;

//using ThoughtsAndPrayersThree.Models;
//using ThoughtsAndPrayersThree.CosmosDB;

//using ThoughtsAndPrayersThree.ViewModels.Base;
//using ThoughtsAndPrayersThree.Pages.ViewCells;

//namespace ThoughtsAndPrayersThree.ViewModels
//{
//    public class ResetableObservableCollection<T> : ObservableCollection<T>
//    {
//        public void Reset() => this.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
//    }


//	public class PrayerListViewModel : BaseViewModel
//	{
        
//        bool _isTheThoughtAnimationVisible;
//		public bool IsTheThoughtAnimationVisible 
//		{
//            get { return _isTheThoughtAnimationVisible; } 
//			set { SetProperty(ref _isTheThoughtAnimationVisible, value); }
//        }

//        bool _isThePrayerAnimationVisible;
//		public bool IsThePrayerAnimationVisible
//		{
//			get { return _isThePrayerAnimationVisible; }
//			set { SetProperty(ref _isThePrayerAnimationVisible, value); }
//		}

//		public ObservableCollection<PrayerRequest> _observableCollectionOfPrayers;
//		public ObservableCollection<PrayerRequest> ObservableCollectionOfPrayers
//		{
//			get { return _observableCollectionOfPrayers; }
//			set { SetProperty(ref _observableCollectionOfPrayers, value); }
//		}

//		int _theNumberOfThoughts;
//		public int TheNumberOfThoughts
//		{
//			get { return TheNumberOfThoughts; }
//			set { SetProperty(ref _theNumberOfThoughts, value); 
//                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
//            }
//		}

//        int _theNumberOfPrayers;
//        public int TheNumberOfPrayers
//		{
//            get { return TheNumberOfPrayers; }
//			set { SetProperty(ref _theNumberOfPrayers, value); 
//                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
//            }
//		}

//        //STRING_TEST
//        string _stringTheNumberOfPrayers;
//        public string StringTheNumberOfPrayers
//        {
//            get { return StringTheNumberOfPrayers; }
//            set { SetProperty(ref _stringTheNumberOfPrayers, value); }
//        }

//        string _combinedNumberOfThoughtsAndPrayers;
//        public string CombinedNumberOfThoughtsAndPrayers
//        {
//            get { return CombinedNumberOfThoughtsAndPrayers; }
//            set { SetProperty(ref _combinedNumberOfThoughtsAndPrayers, value); }
//        }

//        public ResetableObservableCollection<PrayerRequest> MyObservableCollectionOfUnderlyingData
//        {
//            get;
//            set;
//        } = new ResetableObservableCollection<PrayerRequest>();

//        public void ResetDataSource()
//        {
//            MyObservableCollectionOfUnderlyingData.Reset();
//        }


//		public EventHandler<PhotoSavedSuccessAlertEventArgs> TakePhotoSucceeded;
//		public class PhotoSavedSuccessAlertEventArgs : EventArgs
//		{
//			public string Title { get; set; }
//			public string Message { get; set; }
//		}

//		public EventHandler<ThoughtButtonPressedEventArgs> ThoughtButtonPressed;
//        public class ThoughtButtonPressedEventArgs : EventArgs
//		{
//			public string EventArg1 { get; set; }
//			public string EventArg2 { get; set; }
//		}

//		public EventHandler<PrayerButtonPressedEventArgs> PrayerButtonPressed;
//		public class PrayerButtonPressedEventArgs : EventArgs
//		{
//			public string EventArg1 { get; set; }
//			public string EventArg2 { get; set; }
//		}

//		public ICommand DeletePrayerFromListCommand { get; set; }
//		public ICommand ThoughtClickCommand { get; set; }
//        public ICommand PrayerClickCommand { get; set; }
//        public ICommand AddThoughtClickCommand { get; set; }
//        public ICommand AddPrayerClickCommand { get; set; }


//        public PrayerListViewModel()
//        {

//            PrayerViewCell.ParentViewModel = this;

//            var list = new List<PrayerRequest> { };
//            //list = App.PrayerSQLDatabase.GetAllDogs();
//            list = App.ListOfPrayers;

//            foreach (var prayerRequest in list)
//                MyObservableCollectionOfUnderlyingData.Add(prayerRequest);

//            DeletePrayerFromListCommand = new Command(DeletePrayerFromListAction);

//            ThoughtClickCommand = new Command(
//                execute: async () => { await OnThoughtClickActionAsync(); });
//            //  canExecute: () => !IsBusy);


//            //ThoughtClickCommand = new Command <PrayerRequest>(OnThoughtClickActionAsync2);


//            PrayerClickCommand = new Command(
//                execute: async() => { await OnPrayerClickActionAsync(); });

//            AddThoughtClickCommand = new Command <PrayerRequest> (OnAddThoughtClickActionAsync);


//            AddPrayerClickCommand = new Command<PrayerRequest>(OnAddPrayerClickActionAsync);

//		}


//        /* Unmerged change from project 'ASampleApp.iOS'
//        Before:
//                void OnThoughtClickAction()
//                {
//                    if (this.IsTheViewVisible == false)
//                    {

//                        this.IsTheViewVisible = true;
//                        //await Task.Delay(200); //Animation = "checked_done_.json",
//                        //this.MyViewModel.PlayTheLottie = true;
//                        this._animation.Play();

//                        await Task.Delay(2100); //    Animation = "beating_heart.json",
//                                                //await Task.Delay(1400); //    Animation = "like_button.json",
//                        this.IsTheViewVisible = false;

//                    }
//                    else
//                    {
//                        this.IsTheViewVisible = false;
//                    }

//                    //#TODO - must register the click somewhere
//                    ////point 1
//                    ////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
//                    ////point 2
//                    //App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
//                    //string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
//                    //string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//                    //this.FirstLabel = _lastNameStringAdd;
//                    ////ADD THE LAST DOG TO THE ViewModel
//                    //var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
//                    //App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
//                    return;
//                }


//                private void DeleteAllDogsFromList()
//        After:
//                async Task OnThoughtClickActionAsync()
//                {
//                    if (this.IsTheViewVisible == false)
//                    {

//                        this.IsTheViewVisible = true;
//                        //await Task.Delay(200); //Animation = "checked_done_.json",
//                        //this.MyViewModel.PlayTheLottie = true;
//                        this._animation.Play();

//                        await Task.Delay(2100); //    Animation = "beating_heart.json",
//                                                //await Task.Delay(1400); //    Animation = "like_button.json",
//                        this.IsTheViewVisible = false;

//                    }
//                    else
//                    {
//                        this.IsTheViewVisible = false;
//                    }

//                    //#TODO - must register the click somewhere
//                    ////point 1
//                    ////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
//                    ////point 2
//                    //App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
//                    //string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
//                    //string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//                    //this.FirstLabel = _lastNameStringAdd;
//                    ////ADD THE LAST DOG TO THE ViewModel
//                    //var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
//                    //App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
//                    return;
//                }


//                private void DeleteAllDogsFromList()
//        */

//        //void OnThoughtClickActionAsync2(PrayerRequest cellPrayerRequest)
//        //{
//        //    if (this.IsTheThoughtAnimationVisible == false)
//        //    {
//        //        this.IsTheThoughtAnimationVisible = true;
//        //        ThoughtButtonPressed?.Invoke(this, new ThoughtButtonPressedEventArgs { EventArg1 = "Event arg 1", EventArg2 = "Event arg 2" });
//        //        Task.Delay(2100);
//        //        this.IsTheThoughtAnimationVisible = false;
//        //    }
//        //    else
//        //    {
//        //        this.IsTheThoughtAnimationVisible = false;
//        //    }
//        //    return;
//        //}

//        //void OnAddThoughtClickActionAsync(PrayerRequest cellPrayerRequest)
//        //{
//        //    int hi = 5;
//        //    if (cellPrayerRequest != null)
//        //    {
//        //        cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
//        //        cellPrayerRequest.NumberOfThoughts = cellPrayerRequest.NumberOfThoughts + 1;
//        //        this.ResetDataSource();
//        //    }      
//        //}


//        async Task OnThoughtClickActionAsync()
//		{
//			if (this.IsTheThoughtAnimationVisible == false)
//			{
//				this.IsTheThoughtAnimationVisible = true;
//                ThoughtButtonPressed?.Invoke(this, new ThoughtButtonPressedEventArgs { EventArg1 = "Event arg 1", EventArg2 = "Event arg 2" });
//				await Task.Delay(2100);
//				this.IsTheThoughtAnimationVisible = false;

//                //REFERENCE THE ITEM

//                //new PrayerRequest()
//                //{
//                //    Id = 1,
//                //    CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
//                //    CreatedDateTime = DateTimeOffset.UtcNow,
//                //    FirstName = "Andrew",
//                //    LastName = "Kim",
//                //    FullName = "Andrew Kim",
//                //    FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
//                //    //                FBProfileUrl = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
//                //    FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
//                //    PrayerRequestText = "Guys - we want to make it to the World Series this year.  It's going to be an odd numbered year - so we have a chance.  Please keep us in your thoughts!",
//                //    NumberOfThoughts = 7,
//                //    NumberOfPrayers = 5
//                //},

//                //ASSIGN VARIABLE

//                //ADD 1

//                //RE-ASSIGN COUNT

//			}
//			else
//			{
//				this.IsTheThoughtAnimationVisible = false;
//			}
//			return;

//				//await Task.Delay(200); //Animation = "checked_done_.json",
//				//this.MyViewModel.PlayTheLottie = true;
//				//this._animation.Play();

//				//Task.Delay(20000);
//				//this.IsTheViewVisible = false;

//				//TEMP Task.Delay(2100); //    Animation = "beating_heart.json",
//				//						//await Task.Delay(1400); //    Animation = "like_button.json",
//				//TEMP this.IsTheViewVisible = false;


//			//Task.Delay(2000);
//			//this.IsTheViewVisible = false;

//            //#TODO - must register the click somewhere
//			////point 1
//			////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
//			////point 2
//			//App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
//			//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
//			//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//			//this.FirstLabel = _lastNameStringAdd;
//			////ADD THE LAST DOG TO THE ViewModel
//			//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
//			//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
//		}

//		async Task OnPrayerClickActionAsync()
//		{
//			if (this.IsThePrayerAnimationVisible == false)
//			{
//				this.IsThePrayerAnimationVisible = true;
//				PrayerButtonPressed?.Invoke(this, new PrayerButtonPressedEventArgs { EventArg1 = "Event arg 3", EventArg2 = "Event arg 4" });
//				await Task.Delay(2100); 
//				this.IsThePrayerAnimationVisible = false;
//			}
//			else
//			{
//				this.IsThePrayerAnimationVisible = false;
//			}
//			return;

//			//#TODO - must register the click somewhere
//			////point 1
//			////App.DogRep.AddNewDogPhotoURL(this.FirstEntryText, this.SecondEntryText, this.PhotoURLEntry);
//			////point 2
//			//App.DogRepBaseSixtyFour.AddNewDogPhotoSourceB64(this.FirstEntryText, this.SecondEntryText, this.PhotoSourceBaseSixtyFourEntry); //this.PhotoSourceEntry);
//			//string _lastNameString = App.DogRepBaseSixtyFour.GetLastDogB64().Name;
//			//string _lastNameStringAdd = System.String.Format("{0} added to the list!", _lastNameString);
//			//this.FirstLabel = _lastNameStringAdd;
//			////ADD THE LAST DOG TO THE ViewModel
//			//var tempLastDog = App.DogRepBaseSixtyFour.GetLastDogB64();
//			//App.MyDogListPhotoBase64Page.MyViewModel._observableCollectionOfDogs.Add(tempLastDog);
//		}


//        //async Task OnAddThoughtClickActionAsync(PrayerRequest cellPrayerRequest)
//        //{
//        //    int hi = 5;

//        //    var button = (Button)sender;
//        //    string newString = "2nd string";

//        //    var cellBindingContext = (ThoughtsAndPrayersThree.Models.PrayerRequest)button.BindingContext; //this.BindingContext;
//        //    if (cellBindingContext != null)
//        //    {
//        //        cellBindingContext.StringTheNumberOfPrayers = "new and updated";
//        //        this.ResetDataSource();
//        //    }





//        //    return;
//        //}

//        //async Task OnAsyncAddThoughtClickActionAsync(PrayerRequest cellPrayerRequest)
//        //{

//        //    if (cellPrayerRequest != null)
//        //    {
//        //        cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
//        //        this.ResetDataSource();
//        //    }

//        //    return;
//        //}


//        void OnAddThoughtClickActionAsync(PrayerRequest cellPrayerRequest)
//        {
//            if (cellPrayerRequest != null)
//            {
//                cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
//                cellPrayerRequest.NumberOfThoughts = cellPrayerRequest.NumberOfThoughts + 1;
//                this.ResetDataSource();

//                this.OnThoughtClickActionAsync();

//                //TRIGGER A COMMAND
//            }

//            return;
//        }


//        void OnAddPrayerClickActionAsync(PrayerRequest cellPrayerRequest)
//        {
  

//            if (cellPrayerRequest != null)
//            {
//                cellPrayerRequest.StringTheNumberOfPrayers = "new and updated commanded";
//                cellPrayerRequest.NumberOfPrayers = cellPrayerRequest.NumberOfPrayers + 1;
//                this.ResetDataSource();

//                this.OnPrayerClickActionAsync();

//            }
//            return;
//        }


//		private void DeleteAllDogsFromList()
//		{
//			Debug.WriteLine("DELETE ALL DOGS FROM LIST ACTION");
//			var allPrayers = _observableCollectionOfPrayers;

//			foreach (var prayer in allPrayers)
//			{
//				DeletePrayerFromListAction(prayer);
//			};

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
//		}

//		private async void DeletePrayerFromListAction(object obj)
//		{
//			Debug.WriteLine("DELETE PRAYER FROM LIST ACTION");
//			var myPrayer = obj as PrayerRequest;
//			Debug.WriteLine($"Removing Prayer {myPrayer}");

//			if (_observableCollectionOfPrayers.Remove(myPrayer))
//			{
//                var myCosmosPrayer = CosmosDB.PrayerRequestConverter.ConvertToCosmosPrayerRequest(myPrayer);
//                await CosmosDBPrayerService.DeleteCosmosPrayerRequestsAsync(myCosmosPrayer);
//			}
//			else
//			{
//				Debug.WriteLine($"Prayer not removed from observable collection {myPrayer}");
//			}
//		}
//	}
//}




