
using System;
using System.Linq;
using Xamarin.Forms;
using System.Globalization;
using ThoughtsAndPrayersThree.Constants;
using ThoughtsAndPrayersThree.Pages.ViewHelpers;
using ThoughtsAndPrayersThree.Models;
using ThoughtsAndPrayersThree.ViewModels;
using ThoughtsAndPrayersThree.CosmosDB;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ThoughtsAndPrayersThree.Pages
{
    public class AddTapPage : ContentPage
    {

        public static PrayerListViewModel ParentViewModelofAddTapPage;

        Label prompt = new Label() { 
#if __ANDROID__
            FontFamily = "Droid Sans Mono",
#endif

#if __IOS__
            FontFamily = "AppleSDGothicNeo-Light",
#endif

#if __Windows__
            FontFamily = "Times New Roman",
#endif

            Text = "Update: What your question?                " };
        

        Label mST = new Label() { Text = "What's on your mind?                " };

        public Editor mySharedText = new Editor
        {
            BackgroundColor = MyColors.Clouds
        };

        public AddTapPage()
        {
            this.Title = "Add a Prayer Request";

            mySharedText.Text = "Hey guys, this is what's going on...";
            mySharedText.TextColor = Color.Gray;

            mySharedText.Focused += myQuestion_Focused;
            mySharedText.Unfocused += myQuestion_Unfocused;

            //BUTTON
            Button submitButton = new StyledButton(StyledButton.Borders.Thin, 1);
            submitButton.Text = "Submit";
            submitButton.HorizontalOptions = LayoutOptions.FillAndExpand;

            submitButton.Clicked += (sender, e) => {
                Device.BeginInvokeOnMainThread(() => {


                    //#TODO - CREATE - int only UUID 
                    //#TOD0 - this projects SQLite enforces PrimaryKey but not unique in this instance.  
                    // Investiage potential issues here with Id collision  (Ie. wrong prayer request gets deleted or shared)
                    // If you create Unique - will need to do some type of Uniqueness of Id checking (potentially on backend server and graceful error checking)

                    System.Random random = new Random();
                    int randomId = random.Next(1, 1000000000);
                    string randomNumber = string.Join(string.Empty, Enumerable.Range(0, 10).Select(number => random.Next(0, 5).ToString()));

                    PrayerRequest newPrayerRequest = new PrayerRequest()
                    {
                        Id = randomId,
                        CreatedDateTimeString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
                        CreatedDateTime = DateTimeOffset.UtcNow,

                        UpdatedAtString = DateTime.Now.ToString("MMM d h:mm tt", new CultureInfo("en-US")),
                        UpdatedAt = DateTimeOffset.UtcNow,

                        StringOnlyDateTime = "March 1, 2018",
                        FirstName = "Andrew",
                        LastName = "Kim",
                        FullName = "Andrew Kim",
                        FullNameAndDate = "Andrew Kim\r\nMarch 1, 2018",
                        //FBProfileUrl = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
                        FBProfileUrl = "http://graph.facebook.com/450/picture?type=normal",
                        PrayerRequestText = mySharedText.Text,
                        NumberOfThoughts = 0,
                        NumberOfPrayers = 0,
                        StringTheNumberOfPrayers = "first test string"
                    };

                    try 
                    {
                        var newCosmosPrayerRequest = PrayerRequestConverter.ConvertToCosmosPrayerRequest(newPrayerRequest);
                        Task.Run(async ()=> await CosmosDBPrayerService.PostCosmosPrayerRequestsAsync(newCosmosPrayerRequest));
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("DocumentClient Error: ", ex.Message);
                    }

                    App.PrayerSQLDatabase.AddNewPrayerRequest(newPrayerRequest);

                    ParentViewModelofAddTapPage.MyObservableCollectionOfUnderlyingData.Add(newPrayerRequest);

                    ParentViewModelofAddTapPage.ResetDataSource();

                    //#TODO
                    //try//catch//async

                    Navigation.PopModalAsync();
                });
            };

            Button cancelButton = new StyledButton(StyledButton.Borders.Thin, 1);
            cancelButton.Text = "Cancel";
            cancelButton.HorizontalOptions = LayoutOptions.FillAndExpand;

            cancelButton.Clicked += (sender, e) => {
                Device.BeginInvokeOnMainThread(() => {

                    Navigation.PopModalAsync();
                });
            };

            StackLayout myEnterTextStacklayout = new StackLayout
            {   BackgroundColor = Color.Transparent,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                    mST, mySharedText
                }
            };


            StackLayout myButtonStacklayout = new StackLayout
            {   
                Padding = new Thickness(10, 10, 10, 10),
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                    cancelButton, submitButton
                }
            };

            StackLayout combinedLayout = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = new Thickness(10, 50, 10, 10),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    myEnterTextStacklayout, myButtonStacklayout

                }
            };


            #region GRID DEFINITION
            var grid = new Grid()
            {
                Padding = new Thickness(10, 10, 10, 10),
                BackgroundColor = MyColors.LighterGray

            };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(.5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            grid.Children.Add(combinedLayout, 0, 1);

            Content = grid;
            #endregion

        }

        private void myQuestion_Focused(object sender, FocusEventArgs e) //triggered when the user taps on the Editor to interact with it
        {
            if (mySharedText.Text.Equals("Hey guys, this is what's going on...")) //if you have the placeholder showing, erase it and set the text color to black
            {
                mySharedText.TextColor = Color.Black;
                mySharedText.Text = "";
            }
        }

        private void myQuestion_Unfocused(object sender, FocusEventArgs e) //triggered when the user taps "Done" or outside of the Editor to finish the editing
        {
            if (mySharedText.Text.Equals("")) //if there is text there, leave it, if the user erased everything, put the placeholder Text back and set the TextColor to gray
            {
                mySharedText.TextColor = Color.Gray;
                mySharedText.Text = "Hey guys, this is what's going on...";
            }
        }
    }
}