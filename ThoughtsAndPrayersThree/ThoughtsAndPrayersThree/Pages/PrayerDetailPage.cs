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
            var mySentimentProperty = new Label() { };
            var mySentimentCategoryProperty = new Label() { };



#if __ANDROID__
            var thoughtButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Thought",
        FontSize = 16
            };
            var prayerButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Prayer",
        FontSize = 16
            };

#endif

#if __IOS__

            var thoughtButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Thought"
            };
            var prayerButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
            {
                Text = "Prayer"
            };

#endif

            var dateString = new Label()
            {
            };

            var theNumberOfThoughtLabel = new Label() { };
            var theNumberOfPrayersLabel = new Label() { };

            var sentimentLabel = new Label { 
                Text = "Sentiment Score:"
            };

            var sentimentCategoryLabel = new Label { 
                Text = "Sentiment category"
            };


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

            mySentimentProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.SentimentScore), BindingMode.Default);
            mySentimentCategoryProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.SentimentCategory), BindingMode.OneWay, new SentimentCategoryConverter());

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

#if __ANDROID__

            var grid = new Grid()
            {
                Padding = new Thickness(10, 10, 10, 10)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
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

            grid.Children.Add(sentimentLabel, 0, 3);
            Grid.SetColumnSpan(sentimentLabel, 8);

            grid.Children.Add(mySentimentProperty, 7, 3);
            Grid.SetColumnSpan(mySentimentProperty, 8);

            grid.Children.Add(sentimentCategoryLabel, 0, 4);
            Grid.SetColumnSpan(sentimentCategoryLabel, 8);

            grid.Children.Add(mySentimentCategoryProperty, 7, 4);
            Grid.SetColumnSpan(mySentimentCategoryProperty, 8);

            //THOUGHT HEART ANIMATION
            grid.Children.Add(sampleAnimationView, 0, 5);
            Grid.SetColumnSpan(sampleAnimationView, 3);
            Grid.SetRowSpan(sampleAnimationView, 2);
                
            //THOUGHT BUTTON
            grid.Children.Add(thoughtButton, 3, 5);
            Grid.SetColumnSpan(thoughtButton, 5);
            Grid.SetRowSpan(thoughtButton, 2);

            //PRAYER HEART ANIMATION
            grid.Children.Add(sampleAnimationView2, 8, 5);
            Grid.SetColumnSpan(sampleAnimationView2, 3);
            Grid.SetRowSpan(sampleAnimationView2, 2);

            grid.Children.Add(sampleAnimationViewPlaceholder, 8, 5);
            Grid.SetColumnSpan(sampleAnimationViewPlaceholder, 3);
            Grid.SetRowSpan(sampleAnimationViewPlaceholder, 2);

            //PRAYER BUTTON
            grid.Children.Add(prayerButton, 11, 5);
            Grid.SetColumnSpan(prayerButton, 5);
            Grid.SetRowSpan(prayerButton, 2);


            Content = grid;
#endif

#if __IOS__

            var grid = new Grid()
            {
                Padding = new Thickness(10, 10, 10, 10)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
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

            grid.Children.Add(sentimentLabel, 0, 3);
            Grid.SetColumnSpan(sentimentLabel, 8);

            grid.Children.Add(mySentimentProperty, 7, 3);
            Grid.SetColumnSpan(mySentimentProperty, 8);

            grid.Children.Add(sentimentCategoryLabel, 0, 4);
            Grid.SetColumnSpan(sentimentCategoryLabel, 8);

            grid.Children.Add(mySentimentCategoryProperty, 7, 4);
            Grid.SetColumnSpan(mySentimentCategoryProperty, 8);

            grid.Children.Add(sampleAnimationView, 0, 5);
            Grid.SetColumnSpan(sampleAnimationView, 3);
            grid.Children.Add(thoughtButton, 3, 5);
            Grid.SetColumnSpan(thoughtButton, 5);

            grid.Children.Add(sampleAnimationView2, 8, 5);
            Grid.SetColumnSpan(sampleAnimationView2, 3);

            grid.Children.Add(sampleAnimationViewPlaceholder, 8, 5);
            Grid.SetColumnSpan(sampleAnimationViewPlaceholder, 3);

            grid.Children.Add(prayerButton, 11, 5);
            Grid.SetColumnSpan(prayerButton, 5);
    
            Content = grid;
#endif
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

