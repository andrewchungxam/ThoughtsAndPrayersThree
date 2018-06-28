using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using ThoughtsAndPrayersThree.Helpers;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.Pages.ViewHelpers;
using ThoughtsAndPrayersThree.ViewModels;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Pages.ViewCells
{
    public class PrayerViewCell : ViewCell
    {
        MenuItem _deleteAction;
        public static PrayerListViewModel ParentViewModel;

        public PrayerViewCell()//(PrayerListPage inputPrayerListPage)
        {
           var model = BindingContext as PrayerRequest;

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
            cachedImage.SetBinding(CachedImage.SourceProperty, nameof(model.FBProfileUrl));
            dateString.SetBinding(Label.TextProperty, nameof(model.CreatedDateTime), BindingMode.OneWay, new DateTimeToStringConverter());

            theNumberOfThoughtLabel.SetBinding(Label.TextProperty, nameof(model.NumberOfThoughts), BindingMode.OneWay, new NumberOfThoughtsIntToStringConverter());
            theNumberOfPrayersLabel.SetBinding(Label.TextProperty, nameof(model.NumberOfPrayers), BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

            theCombinedNumberOfThoughtsAndPrayersLabel.SetBinding(Label.TextProperty, nameof(model.CombinedNumberOfThoughtsAndPrayers), BindingMode.Default); //, new CombinedNumberOfThoughtsAndPrayersStringConverter());

            //NEW
            myFullNameProperty.SetBinding(Label.TextProperty, nameof(model.NewCombinedNameAndDate), BindingMode.Default);

            myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(model.PrayerRequestText));

            var navigationPage = Application.Current.MainPage as NavigationPage;
            var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
            //FOR NOW 
            var prayerListViewModel = prayerListPage?.BindingContext as PrayerListViewModel;

            //COMBINED-COMMANDING-NEW
            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddThoughtClickCommand", source: prayerListPage));

            prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddPrayerClickCommand", source: prayerListPage));

            #endregion

            #region GRID DEFINITION
            var grid = new Grid() {
                Padding = new Thickness(10, 10, 10, 10)
            };


#if __ANDROID__
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
#endif

#if __IOS__
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

#endif

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            grid.Children.Add(cachedImage, 0, 0);
            Grid.SetRowSpan(cachedImage, 2);
            //Grid.SetColumnSpan(cachedImage, 2);

            grid.Children.Add(myFullNameProperty, 1, 0);
            Grid.SetColumnSpan(myFullNameProperty, 3);


            grid.Children.Add(theCombinedNumberOfThoughtsAndPrayersLabel, 1, 1);
            Grid.SetColumnSpan(theCombinedNumberOfThoughtsAndPrayersLabel, 3);

            grid.Children.Add(myPrayerRequestProperty, 0, 2);
            Grid.SetColumnSpan(myPrayerRequestProperty, 4);

            grid.Children.Add(thoughtButton, 0, 3);
            Grid.SetColumnSpan(thoughtButton, 2);
            grid.Children.Add(prayerButton, 2, 3);
            Grid.SetColumnSpan(prayerButton, 2);

            View = grid;
            #endregion
        }
    }
}