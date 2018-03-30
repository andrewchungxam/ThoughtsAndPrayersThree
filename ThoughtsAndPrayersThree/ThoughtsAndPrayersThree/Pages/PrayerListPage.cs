using System;

using ThoughtsAndPrayersThree;
using ThoughtsAndPrayersThree.ViewModels;
using ThoughtsAndPrayersThree.Pages.ViewCells;

using Lottie.Forms;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Pages
{
    public class PrayerListPage : BaseContentPage<PrayerListViewModel>
    {
        ListView _prayerListPage;
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
                //var result = await page.DisplayAlert("Title", "Message", "Accept", "Cancel");
            }));

            _prayerListPage = new ListView();

            _prayerListPage.ItemTemplate = new DataTemplate(() => {
                return new PrayerViewCell(); //(this)
            });

           //NEW METHOD
            _prayerListPage.SetBinding(ListView.ItemsSourceProperty, nameof(MyViewModel.MyObservableCollectionOfUnderlyingData));


            _prayerListPage.HasUnevenRows = true;

            //OPTION 2 WITH ANIMATIONS
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
                 _prayerListPage,
                AbsoluteLayoutFlags.PositionProportional
            );

            AbsoluteLayout.SetLayoutBounds
            (
                _prayerListPage,
                new Rectangle(0, 0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );

            _button = new Button()
            {
                Text = "Press me!"

            };

            AbsoluteLayout.SetLayoutFlags
            (
                _button,
                AbsoluteLayoutFlags.PositionProportional
            );

            AbsoluteLayout.SetLayoutBounds
            (
                _button,
                new Rectangle(.5, .1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );
            
            //BOTTOM TO TOP -->       
            simpleLayout.Children.Add(_prayerListPage);
            simpleLayout.Children.Add(contentView1);
            simpleLayout.Children.Add(contentView);
            Content = simpleLayout;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MyViewModel.ThoughtButtonPressed += MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed += MyViewModel_PrayerButtonPressed;

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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MyViewModel.ThoughtButtonPressed -= MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed -= MyViewModel_PrayerButtonPressed;
        }
    }
}

