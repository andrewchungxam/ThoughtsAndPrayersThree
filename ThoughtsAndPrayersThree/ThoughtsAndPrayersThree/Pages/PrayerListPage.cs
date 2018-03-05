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
        Button _myButton;
        Button _button;

        public PrayerListPage()
        {
            _prayerListPage = new ListView();

            var _prayerCellTemplate = new DataTemplate(typeof(PrayerViewCell));
            _prayerListPage.ItemTemplate = _prayerCellTemplate;
            _prayerListPage.SetBinding(ListView.ItemsSourceProperty,nameof(MyViewModel.ObservableCollectionOfPrayers ));

            _prayerListPage.HasUnevenRows = true;

            //OPTION 1 WITHOUT ANIMATIONS
            //CONFIRMED WORKING
            //Content = _prayerListPage;

            //OPTION 2 WITH ANIMATIONS
            var contentView = new ContentView()
            {
                BackgroundColor = Color.FromHex("#7000"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _animation = new AnimationView
            {
                //Animation = "LottieLogo1.json",
                //Animation = "love_explosion.json",
                //Animation = "checked_done_.json",
                Animation = "beating_heart.json",
                //Animation = "like_button.json",
                WidthRequest = 290,
                HeightRequest = 290,
                Loop = false,
                AutoPlay = false,
                //Loop = false,
                //AutoPlay = false,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            contentView.Content = _animation;
            contentView.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsTheThoughtAnimationVisible));

            AbsoluteLayout.SetLayoutFlags
            (
                contentView,
//              AbsoluteLayoutFlags.PositionProportional
                AbsoluteLayoutFlags.All
            );
            AbsoluteLayout.SetLayoutBounds
            (
                contentView,
                //new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
                new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)

            );

            var contentView1 = new ContentView()
            {
                BackgroundColor = Color.FromHex("#7000"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            _animation1 = new AnimationView
            {
                //Animation = "beating_heart.json",
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
            //                AbsoluteLayoutFlags.PositionProportional
            AbsoluteLayoutFlags.All
            );
            AbsoluteLayout.SetLayoutBounds
            (
                contentView1,
                new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
            );

            AbsoluteLayout simpleLayout = new AbsoluteLayout
            {
                //#TODO - BACKGROUND
                //BackgroundColor = Color.Blue.WithLuminosity(0.9),
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

            ///////////////////
            ///   
            /// 

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


//
//      BOTTOM TO TOP -->       
//

            simpleLayout.Children.Add(_prayerListPage);
            //            simpleLayout.Children.Add(_button);
            simpleLayout.Children.Add(contentView1);

            simpleLayout.Children.Add(contentView);



            //simpleLayout.Children.Add(contentView1);

            Content = simpleLayout;

            //Content = new StackLayout
            //{
            //    Children = {
            //        new Label { Text = "Hello ContentPage" }
            //    }
            //};
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

            //CAN YOU INCREMENT THE COUNT 1?
            //
            //https://forums.xamarin.com/discussion/61317/calling-command-from-viewmodel-using-button-within-listview

            //placeList.ItemTapped += async (sender, e) => {
            //    var item = (Place)e.Item;
            //    _vm.ViewCommand.Execute(item);
            //    placeList.SelectedItem = null;
            //};


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

