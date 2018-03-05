using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using ThoughtsAndPrayersThree.Helpers;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.ViewModels;
using Xamarin.Forms;

namespace ThoughtsAndPrayersThree.Pages.ViewCells
{
    public class PrayerViewCell : ViewCell
    {
    	MenuItem _deleteAction;

        public PrayerViewCell()
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
                //    Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
                Transformations = new System.Collections.Generic.List<ITransformation>()
                {
                    //new GrayscaleTransformation(),
                    new CircleTransformation(),
                }
            };

            var myNameProperty = new Label() { };
            var myFullNameProperty = new Label() { };
            var myPrayerRequestProperty = new Label() { };
            var thoughtButton = new Button()
            {
                Text = "Thought"
            };
            var prayerButton = new Button()
            {
                Text = "Prayer"
            };

            var dateString = new Label()
            {
            };

            var theNumberOfThoughtLabel = new Label() { };
            var theNumberOfPrayersLabel = new Label() { };

            #region BINDINGS
            cachedImage.SetBinding(CachedImage.SourceProperty, nameof(model.FBProfileUrl));
            dateString.SetBinding(Label.TextProperty, nameof(model.CreatedDateTime), BindingMode.OneWay, new DateTimeToStringConverter());

            theNumberOfThoughtLabel.SetBinding(Label.TextProperty, nameof(model.NumberOfThoughts), BindingMode.OneWay, new NumberOfThoughtsIntToStringConverter());
            theNumberOfPrayersLabel.SetBinding(Label.TextProperty, nameof(model.NumberOfPrayers), BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

            //myNameProperty.SetBinding(Label.TextProperty, nameof(model.FullName));

            myFullNameProperty.SetBinding(Label.TextProperty, nameof(model.FullNameAndDate));



            myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(model.PrayerRequestText));

            var navigationPage = Application.Current.MainPage as NavigationPage;
            var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
            var prayerListViewModel = prayerListPage.BindingContext as PrayerListViewModel;

            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.ThoughtClickCommand", source: prayerListPage));

            prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.PrayerClickCommand", source: prayerListPage));
            #endregion

            #region GRID DEFINITION
            var grid = new Grid() {
                Padding = new Thickness(10, 10, 10, 10)
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
 //           grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { 
                Height = new GridLength(1, GridUnitType.Star)
            });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var Picture = new Label { Text = "Picture Picture Picture Picture Picture Picture Picture Picture Picture" };
            var Name = new Label { Text = "First and Last Name" };
            var Time = new Label { Text = "Time" };
            var PrayerRequest = new Label
            {
                Text = "Text of prayer request."
            };
            var ThoughtsNumber = new Label { Text = "30 Thoughts" };
            var PrayerNumber = new Label { Text = "3 Prayer" };
            var ThoughtsButton = new Label { Text = "Thoughts Button" };
            var PrayerButton = new Label { Text = "Prayer Button" };

            grid.Children.Add(cachedImage, 0, 0);
            Grid.SetRowSpan(cachedImage, 2);
//            Grid.SetColumnSpan(cachedImage, 2);


            //REPLACE WITH NAME AND DATA
            //grid.Children.Add(myNameProperty, 1, 0);
            //Grid.SetColumnSpan(myNameProperty, 3);

            grid.Children.Add(myFullNameProperty, 1, 0);
            Grid.SetColumnSpan(myFullNameProperty, 3);

            //grid.Children.Add(dateString, 1, 1);
            //Grid.SetColumnSpan(dateString, 2);

            grid.Children.Add(theNumberOfThoughtLabel, 1, 1);
            Grid.SetColumnSpan(theNumberOfThoughtLabel, 3);

//            grid.Children.Add(theNumberOfThoughtLabel, 1, 2);
            //Grid.SetColumnSpan(theNumberOfThoughtLabel, 2);
 //           grid.Children.Add(theNumberOfPrayersLabel, 2, 2);
            //Grid.SetColumnSpan(theNumberOfPrayersLabel, 2);

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

