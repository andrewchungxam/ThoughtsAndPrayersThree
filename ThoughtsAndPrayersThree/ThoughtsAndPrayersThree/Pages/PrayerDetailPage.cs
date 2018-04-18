using System;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using ThoughtsAndPrayersThree.Constants;
using ThoughtsAndPrayersThree.Helpers;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.Pages.ViewHelpers;
using ThoughtsAndPrayersThree.ViewModels;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Pages
{
    public class PrayerDetailPage :  BaseContentPage<PrayerDetailPageViewModel>
    {
        public Lottie.Forms.AnimationView sampleAnimationView;
        public Lottie.Forms.AnimationView sampleAnimationView2;
        public Lottie.Forms.AnimationView sampleAnimationViewPlaceholder;

        public PrayerDetailPage()
        {
            var cachedImage = new CachedImage()
            {
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
                WidthRequest = 50,
                HeightRequest = 50,
                CacheDuration = TimeSpan.FromDays(30),
                DownsampleToViewSize = true,
                RetryCount = 0,
                RetryDelay = 250,
                BitmapOptimizations = false,
                LoadingPlaceholder = "tab_feed.png",
                ErrorPlaceholder = "xamarin_logo.png",
                //Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
                Transformations = new System.Collections.Generic.List<ITransformation>()
                    {
                        //new GrayscaleTransformation(),
                        new CircleTransformation(),
                    }
            };

            var myNameProperty = new Label() { };
            var myFullNameProperty = new Label() { };
            var myPrayerRequestProperty = new Label() { };

            var thoughtButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Thought"
            };
            var prayerButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Prayer"
            };

            var dateString = new Label()
            {
            };

            var theNumberOfThoughtLabel = new Label() { };
            var theNumberOfPrayersLabel = new Label() { };

            var theCombinedNumberOfThoughtsAndPrayersLabel = new Label();

            #region BINDINGS
            cachedImage.SetBinding(CachedImage.SourceProperty, nameof(this.MyViewModel.TheFBProfileUrl));

            dateString.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheCreatedDateTime ), BindingMode.OneWay, new DateTimeToStringConverter());

            theNumberOfThoughtLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNumberOfThoughts), BindingMode.OneWay, new NumberOfThoughtsIntToStringConverter());
            theNumberOfPrayersLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNumberOfPrayers), BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

            theCombinedNumberOfThoughtsAndPrayersLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheCombinedNumberOfThoughtsAndPrayers), BindingMode.Default); //, new CombinedNumberOfThoughtsAndPrayersStringConverter());
            //theCombinedNumberOfThoughtsAndPrayersLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.SelectedPrayerRequest.CombinedNumberOfThoughtsAndPrayers), BindingMode.Default); //, new CombinedNumberOfThoughtsAndPrayersStringConverter());

            myFullNameProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNewCombinedNameAndDate), BindingMode.Default);
            myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.ThePrayerRequestText));

            //////COMBINED-COMMANDING-NEW
            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("SelectedPrayerRequest"));  //("BindingContext.SelectedPrayerRequest"));  //new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddThoughtClickCommand", source: this));

            prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("SelectedPrayerRequest"));  //("BindingContext.SelectedPrayerRequest"));  //new Binding("."));
            prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddPrayerClickCommand", source: this));

            sampleAnimationView = new Lottie.Forms.AnimationView() { 
                Animation = "beating_heart.json",
                Loop = true,
                AutoPlay = false,
                BackgroundColor = MyColors.MyBlue1
            };

            sampleAnimationViewPlaceholder = new Lottie.Forms.AnimationView()
            {
                Animation = "beating_heart.json",
                Loop = true,
                AutoPlay = false
            };

            sampleAnimationView2 = new Lottie.Forms.AnimationView()
            {
                Animation = "like_button.json",
                Loop = true,
                AutoPlay = false,
                BackgroundColor = MyColors.MyBlue1
            };

            #endregion

            #region GRID DEFINITION
            var grid = new Grid()
            {
                Padding = new Thickness(10, 10, 10, 10)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(cachedImage, 0, 0);
            Grid.SetRowSpan(cachedImage, 2);
            Grid.SetColumnSpan(cachedImage, 4);

            grid.Children.Add(myFullNameProperty, 4, 0);
            Grid.SetColumnSpan(myFullNameProperty, 12);

            grid.Children.Add(theCombinedNumberOfThoughtsAndPrayersLabel, 4, 1);
            Grid.SetColumnSpan(theCombinedNumberOfThoughtsAndPrayersLabel, 12);

            grid.Children.Add(myPrayerRequestProperty, 0, 2);
            Grid.SetColumnSpan(myPrayerRequestProperty, 16);

            grid.Children.Add(sampleAnimationView, 0, 3);
            Grid.SetColumnSpan(sampleAnimationView, 3);
            grid.Children.Add(thoughtButton, 3, 3);
            Grid.SetColumnSpan(thoughtButton, 5);

            grid.Children.Add(sampleAnimationView2, 8, 3);
            Grid.SetColumnSpan(sampleAnimationView2, 3);

            grid.Children.Add(sampleAnimationViewPlaceholder, 8, 3);
            Grid.SetColumnSpan(sampleAnimationViewPlaceholder, 3);

            grid.Children.Add(prayerButton, 11, 3);
            Grid.SetColumnSpan(prayerButton, 5);
    
            Content = grid;
            #endregion

        }

        private void OnBackButtonClickedRight(object sender, EventArgs e)
        {
            sampleAnimationViewPlaceholder.IsVisible = !sampleAnimationViewPlaceholder.IsVisible;
            sampleAnimationView2.Play();
        }

        private void OnBackButtonClickedLeft(object sender, EventArgs e)
        {
            sampleAnimationView.Play();
        }

		protected override void OnAppearing()
		{
            base.OnAppearing();
            MyViewModel.ThoughtButtonPressed += MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed += MyViewModel_PrayerButtonPressed;

		}

        private void MyViewModel_PrayerButtonPressed(object sender, PrayerDetailPageViewModel.PrayerButtonPressedEventArgs e)
        {
            sampleAnimationViewPlaceholder.IsVisible = !sampleAnimationViewPlaceholder.IsVisible;
            sampleAnimationView2.Play();

        }

        private void MyViewModel_ThoughtButtonPressed(object sender, PrayerDetailPageViewModel.ThoughtButtonPressedEventArgs e)
        {
            sampleAnimationView.Play();
        }

		protected override void OnDisappearing()
		{
            base.OnDisappearing();
            MyViewModel.ThoughtButtonPressed -= MyViewModel_ThoughtButtonPressed;
            MyViewModel.PrayerButtonPressed -= MyViewModel_PrayerButtonPressed;

		}


	}






}

