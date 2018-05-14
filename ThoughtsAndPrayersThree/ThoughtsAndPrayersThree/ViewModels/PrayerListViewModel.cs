using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;

using ThoughtsAndPrayersThree.Models;

using ThoughtsAndPrayersThree.ViewModels.Base;
using ThoughtsAndPrayersThree.Pages.ViewCells;

using ThoughtsAndPrayersThree.Pages;
using ThoughtsAndPrayersThree.CosmosDB;
using ThoughtsAndPrayersThree.Services;
using ThoughtsAndPrayersThree.LocalData;

namespace ThoughtsAndPrayersThree.ViewModels
{
    public class ResetableObservableCollection<T> : ObservableCollection<T>
    {
        public void Reset() => this.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
    }

    public class PrayerListViewModel : BaseViewModel
    {

        double _heightRequestDoubleValue;
        public double HeightRequestDoubleValue
        {
            get { return _heightRequestDoubleValue; }
            set { SetProperty(ref _heightRequestDoubleValue, value); }
        }
        
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
            get { return _theNumberOfThoughts; }
            set { 
                SetProperty(ref _theNumberOfThoughts, value); 
                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
            }
        }

        int _theNumberOfPrayers;
        public int TheNumberOfPrayers
        {
            get { return _theNumberOfPrayers; }
            set { 
                SetProperty(ref _theNumberOfPrayers, value);
                OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
            }
        }

        //STRING_TEST
        string _stringTheNumberOfPrayers;
        public string StringTheNumberOfPrayers
        {
            get { return _stringTheNumberOfPrayers; }
            set { SetProperty(ref _stringTheNumberOfPrayers, value); }
        }

        string _combinedNumberOfThoughtsAndPrayers;
        public string CombinedNumberOfThoughtsAndPrayers
        {
            get { return _combinedNumberOfThoughtsAndPrayers; }
            set { SetProperty(ref _combinedNumberOfThoughtsAndPrayers, value); }
        }

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
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

        public ICommand RefreshCommand { get; set; }

        public PrayerListViewModel()
        {

            PrayerViewCell.ParentViewModel = this;
            AddTapPage.ParentViewModelofAddTapPage = this;
            PrayerDetailPageViewModel.ParentViewModelOfDetailPage = this;

            var list = new List<PrayerRequest> { };
            //list = App.PrayerSQLDatabase.GetAllDogs();
            list = App.ListOfPrayers;

            foreach (var prayerRequest in list)
                MyObservableCollectionOfUnderlyingData.Add(prayerRequest);

            //           DeletePrayerFromListCommand = new Command(DeletePrayerFromListAction);

            RefreshCommand = new Command(
                execute: async () => { await ExecuteRefreshCommand(); });

            ThoughtClickCommand = new Command(
                execute: async () => { await OnThoughtClickActionAsync(); });
            //  canExecute: () => !IsBusy);

            //ThoughtClickCommand = new Command <PrayerRequest>(OnThoughtClickActionAsync2);

            PrayerClickCommand = new Command(
                execute: async() => { await OnPrayerClickActionAsync(); });

            AddThoughtClickCommand = new Command <PrayerRequest> (OnAddThoughtClickActionAsync);

            AddPrayerClickCommand = new Command<PrayerRequest>(OnAddPrayerClickActionAsync);
        }

        async Task ExecuteRefreshCommand()
        {
            IsRefreshing = true;

            try
            {
                var minimumSpinnerTime = Task.Delay(1000);

                await DatabaseSyncService.SyncRemoteAndLocalDatabases().ConfigureAwait(false);

                var prayerRequestList = await PrayerRequestDatabase.GetAllPrayersAsync().ConfigureAwait(false);
                //AllContactsList = contactList.Where(x => !x.IsDeleted).OrderBy(x => x.FullName).ToList();
                //AllContactsList = prayerRequestList.OrderBy(x => x.CreatedDateTime).ToList();

                MyObservableCollectionOfUnderlyingData.Clear();
                foreach (var prayerRequest in prayerRequestList)
                    MyObservableCollectionOfUnderlyingData.Add(prayerRequest);

//                  MAY NEED THIS
//                  this.ResetDataSource();

                await minimumSpinnerTime;
            }
            catch (Exception e)
            {
                //AppCenterHelpers.LogException(e);
            }
            finally
            {
                IsRefreshing = false;
            }
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
                cellPrayerRequest.UpdatedAtString = DateTime.Now.ToString("MMM d h:mm tt", new System.Globalization.CultureInfo("en-US"));
                cellPrayerRequest.UpdatedAt = DateTimeOffset.UtcNow;

                try
                {
                    var updatedCosmosPrayerRequest = PrayerRequestConverter.ConvertToCosmosPrayerRequest(cellPrayerRequest);
                    Task.Run(async () => await CosmosDBPrayerService.PutCosmosPrayerRequestsAsync(updatedCosmosPrayerRequest));
                }
                        catch (Exception ex)
                {
                    Debug.WriteLine("DocumentClient Error: ", ex.Message);
                }

                App.PrayerSQLDatabase.UpdateNumberOfThoughts(cellPrayerRequest);
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
                cellPrayerRequest.UpdatedAtString = DateTime.Now.ToString("MMM d h:mm tt", new System.Globalization.CultureInfo("en-US"));
                cellPrayerRequest.UpdatedAt = DateTimeOffset.UtcNow;

                try
                {
                    var updatedCosmosPrayerRequest = PrayerRequestConverter.ConvertToCosmosPrayerRequest(cellPrayerRequest);
                    Task.Run(async () => await CosmosDBPrayerService.PutCosmosPrayerRequestsAsync(updatedCosmosPrayerRequest));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("DocumentClient Error: ", ex.Message);
                }

                App.PrayerSQLDatabase.UpdateNumberOfPrayers(cellPrayerRequest);
                this.ResetDataSource();
                this.OnPrayerClickActionAsync();

            }
            return;
        }
    }
}