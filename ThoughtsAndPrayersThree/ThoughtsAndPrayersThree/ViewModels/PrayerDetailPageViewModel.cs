using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.ViewModels.Base;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.ViewModels
{
    public class PrayerDetailPageViewModel : BaseViewModel
    {
        public static PrayerListViewModel ParentViewModelOfDetailPage;

        private PrayerRequest _prayerRequest;
        public PrayerRequest SelectedPrayerRequest
        {
            get { return _prayerRequest; }
            set 
            { 
                SetProperty(ref _prayerRequest, value);
            }
        }

        public string SelectedPrayerName
        {
            get { return _prayerRequest?.FullName; }
            set {
                _prayerRequest.FullName = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _theCreatedDateTime;
        public DateTimeOffset TheCreatedDateTime
        {
            get { return _theCreatedDateTime; }
            set { SetProperty(ref _theCreatedDateTime, value); }
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
            set
            {
                SetProperty(ref _theNumberOfThoughts, value);
                ////OnPropertyChanged(nameof(this.TheNumberOfThoughts));
                //OnPropertyChanged(nameof(this.TheCombinedNumberOfThoughtsAndPrayers));
            }
        }

        int _theNumberOfPrayers;
        public int TheNumberOfPrayers
        {
            get { return _theNumberOfPrayers; }
            set
            {
                SetProperty(ref _theNumberOfPrayers, value);
                //OnPropertyChanged(nameof(this.CombinedNumberOfThoughtsAndPrayers));
            }
        }

        string _theFBProfileUrl;
        public string TheFBProfileUrl
        {
            get { return _theFBProfileUrl; }
            set { SetProperty(ref _theFBProfileUrl, value); }
        }

        string _theCombinedNumberOfThoughtsAndPrayers;
        public string TheCombinedNumberOfThoughtsAndPrayers
        {
            get { return _theCombinedNumberOfThoughtsAndPrayers; }
            set { SetProperty(ref _theCombinedNumberOfThoughtsAndPrayers, value); }
        }

        string _theNewCombinedNameAndDate;
        public string TheNewCombinedNameAndDate
        {
            get { return _theNewCombinedNameAndDate; }
            set { SetProperty(ref _theNewCombinedNameAndDate, value); }
        }

        string _thePrayerRequestText;
        public string ThePrayerRequestText
        {
            get { return _thePrayerRequestText; }
            set { SetProperty(ref _thePrayerRequestText, value); }
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

        public EventHandler<LeftAnimationEventArgs> TakePhotoSucceeded;
        public class LeftAnimationEventArgs : EventArgs
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

        public PrayerDetailPageViewModel()
        {

            ThoughtClickCommand = new Command(
                execute: async () => { await OnThoughtClickActionAsync(); });
            //  canExecute: () => !IsBusy);

            
            PrayerClickCommand = new Command(
                execute: async () => { await OnPrayerClickActionAsync(); });

            AddThoughtClickCommand = new Command<PrayerRequest>(OnAddThoughtClickActionAsync);

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

        void OnAddThoughtClickActionAsync(PrayerRequest specificCellPrayerRequest)
        {
            if (specificCellPrayerRequest != null)
            {
                specificCellPrayerRequest.NumberOfThoughts = specificCellPrayerRequest.NumberOfThoughts + 1;
                this.TheCombinedNumberOfThoughtsAndPrayers = this.SelectedPrayerRequest.CombinedNumberOfThoughtsAndPrayers;

                ThoughtButtonPressed?.Invoke(this, new ThoughtButtonPressedEventArgs { EventArg1 = "Thought Event 1", EventArg2 = "Thought Event 2" });

                var originalItem = ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData.FirstOrDefault(i => i.Id == specificCellPrayerRequest.Id);
                if (originalItem == null) 
                {
                    return;
                }
                var index = ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData.IndexOf(originalItem);
                ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData[index] = specificCellPrayerRequest;

                this.OnThoughtClickActionAsync();
            }
            return;
        }


        void OnAddPrayerClickActionAsync(PrayerRequest specificCellPrayerRequest)
        {
            if (specificCellPrayerRequest != null)
            {
                specificCellPrayerRequest.NumberOfPrayers = specificCellPrayerRequest.NumberOfPrayers + 1;
                this.TheCombinedNumberOfThoughtsAndPrayers = this.SelectedPrayerRequest.CombinedNumberOfThoughtsAndPrayers;

                PrayerButtonPressed?.Invoke(this, new PrayerButtonPressedEventArgs { EventArg1 = "Thought Event 1", EventArg2 = "Thought Event 2" });

                var originalItem = ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData.FirstOrDefault(i => i.Id == specificCellPrayerRequest.Id);
                if (originalItem == null)
                {
                    return;
                }
                var index = ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData.IndexOf(originalItem);
                ParentViewModelOfDetailPage.MyObservableCollectionOfUnderlyingData[index] = specificCellPrayerRequest;

                this.OnPrayerClickActionAsync();
            }
            return;
        }
    }
}
