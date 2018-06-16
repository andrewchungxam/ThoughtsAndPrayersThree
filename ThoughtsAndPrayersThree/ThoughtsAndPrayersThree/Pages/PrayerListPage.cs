using System;

using ThoughtsAndPrayersThree;
using ThoughtsAndPrayersThree.ViewModels;
using ThoughtsAndPrayersThree.Pages.ViewCells;

using Lottie.Forms;
using Xamarin.Forms;
using ThoughtsAndPrayersThree.Models;

namespace ThoughtsAndPrayersThree.Pages
{
    public class PrayerListPage : BaseContentPage<PrayerListViewModel>
    {
        ListView _prayerListView;
        AnimationView _animation;
        AnimationView _animation1;
        Button _button;

        public int test = 0;


        public PrayerListPage()
        {
            this.ToolbarItems.Add(new ToolbarItem("+", null, //"filter.png", 
            async () => 
            { 
                var addTapPage = new AddTapPage(); 
                Navigation.PushModalAsync(addTapPage);
            }));

            var myEmoji = "\U0001F600";

            var thinkingFace = "\U0001F914";
            var prayerHands = "\U0001F64F";

            this.Title = thinkingFace + " + " + prayerHands;  //this.Title = thinkingFace +"s" + " + " + prayerHands + "s";

    

            _prayerListView = new ListView();

            _prayerListView.IsPullToRefreshEnabled = true;
            _prayerListView.SetBinding(ListView.RefreshCommandProperty, nameof(MyViewModel.RefreshCommand));

            _prayerListView.SetBinding(ListView.IsRefreshingProperty, nameof(MyViewModel.IsRefreshing));

            _prayerListView.ItemTemplate = new DataTemplate(() => {
                return new PrayerViewCell(); //(this)
            });

            _prayerListView.SetBinding(ListView.ItemsSourceProperty, nameof(MyViewModel.MyObservableCollectionOfUnderlyingData));
            _prayerListView.HasUnevenRows = true;
            _prayerListView.SetBinding(ListView.HeightRequestProperty, nameof(MyViewModel.HeightRequestDoubleValue));

            var contentView = new ContentView()
            {
                BackgroundColor = Color.FromHex("#7000"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _animation = new AnimationView
            {
                Animation = "beating_heart.json",
                WidthRequest = 290,
                HeightRequest = 290,
                Loop = false,
                AutoPlay = false,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            contentView.Content = _animation;
            contentView.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsTheThoughtAnimationVisible));

            AbsoluteLayout.SetLayoutFlags
            (
                contentView,
                AbsoluteLayoutFlags.All
            );
            AbsoluteLayout.SetLayoutBounds
            (
                contentView,
                new Rectangle(.5, .5, 1, 1)

            );

            var contentView1 = new ContentView()
            {
                BackgroundColor = Color.FromHex("#7000"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            _animation1 = new AnimationView
            {
                Animation = "like_button.json",
                WidthRequest = 290,
                HeightRequest = 290,
                Loop = true,
                AutoPlay = false,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            contentView1.Content = _animation1;
            contentView1.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsThePrayerAnimationVisible));

            AbsoluteLayout.SetLayoutFlags
            (
                contentView1,
                AbsoluteLayoutFlags.All
            );
            
            AbsoluteLayout.SetLayoutBounds
            (
                contentView1,
                new Rectangle(.5, .5, 1, 1)
            );

            AbsoluteLayout simpleLayout = new AbsoluteLayout
            {
                //#TODO - BACKGROUND
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            AbsoluteLayout.SetLayoutFlags
            (
                 _prayerListView,
                 AbsoluteLayoutFlags.PositionProportional
            );

            AbsoluteLayout.SetLayoutBounds
            (
                _prayerListView,
                new Rectangle(0, 0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize) 
            );
            
            //BOTTOM TO TOP -->       
            simpleLayout.Children.Add(_prayerListView);
            simpleLayout.Children.Add(contentView1);
            simpleLayout.Children.Add(contentView);
            Content = simpleLayout;

        }

        private void MyViewModel_PrayerButtonPressed(object sender, PrayerListViewModel.PrayerButtonPressedEventArgs e)
        {
            //THOUGHT BUTTON
            _animation1.Play();
        }

        private void MyViewModel_ThoughtButtonPressed(object sender, PrayerListViewModel.ThoughtButtonPressedEventArgs e)
        {
            //PRAYER BUTTON
            _animation.Play();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            this.MyViewModel.HeightRequestDoubleValue = height;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MyViewModel.ThoughtButtonPressed += MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed += MyViewModel_PrayerButtonPressed;

            _prayerListView.ItemSelected += OnListViewItemSelected;

            MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
        }

        private void OnMessageReceived(object arg1, string arg2)
        {
            //Device.BeginInvokeOnMainThread(() =>
            //{
            //    //lblMsg.Text = msg;
            //});
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    MessagingCenter.Subscribe<object, string>(this, App.NotificationReceivedKey, OnMessageReceived);
        //    //btnSend_Native.Clicked += OnBtnSendClicked_Native;
        //    //btnSend_Template.Clicked += OnBtnSendClicked_Template;
        //    //btnSend_MultipleTemplate.Clicked += OnBtnSendClicked_MultipleTemplate;
        //}

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();

        //    MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        //}

        //void OnMessageReceived(object sender, string msg)
        //{
        //    Device.BeginInvokeOnMainThread(() =>
        //    {
        //        //lblMsg.Text = msg;
        //    });
        //}

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MyViewModel.ThoughtButtonPressed -= MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed -= MyViewModel_PrayerButtonPressed;

            _prayerListView.ItemSelected -= OnListViewItemSelected;

            MessagingCenter.Unsubscribe<object>(this, App.NotificationReceivedKey);
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var itemSelected = e?.SelectedItem as PrayerRequest;

                PrayerRequest selectedPrayerRequest = itemSelected;
                var pdPage = new PrayerDetailPage() { };

                pdPage.MyViewModel.SelectedPrayerRequest = itemSelected;

                pdPage.MyViewModel.TheNumberOfPrayers = selectedPrayerRequest.NumberOfPrayers;
                pdPage.MyViewModel.TheFBProfileUrl = selectedPrayerRequest.FBProfileUrl;
                pdPage.MyViewModel.TheCreatedDateTime = selectedPrayerRequest.CreatedDateTime;

                pdPage.MyViewModel.TheNumberOfThoughts = selectedPrayerRequest.NumberOfThoughts;
                pdPage.MyViewModel.TheNumberOfPrayers = selectedPrayerRequest.NumberOfPrayers;

                pdPage.MyViewModel.TheCombinedNumberOfThoughtsAndPrayers = selectedPrayerRequest.CombinedNumberOfThoughtsAndPrayers;
                pdPage.MyViewModel.TheNewCombinedNameAndDate = selectedPrayerRequest.FullNameAndDate;

                pdPage.MyViewModel.ThePrayerRequestText = selectedPrayerRequest.PrayerRequestText;

                pdPage.MyViewModel.SentimentScore = selectedPrayerRequest.SentimentScore.ToString();

                pdPage.MyViewModel.SentimentCategory = selectedPrayerRequest.SentimentScore;

                await Navigation?.PushAsync(pdPage);

                _prayerListView.SelectedItem = null;
            });
        }





    }
}

