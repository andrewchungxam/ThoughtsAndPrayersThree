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

        //List<FFImageLoading.Work.ITransformation> _myListOfTransformations;



		public PrayerViewCell()
    	{
			var model = BindingContext as PrayerRequest;

			//_myListOfTransformations = new List<FFImageLoading.Work.ITransformation> ()
			//{ 
            //    new CircleTransformation() 
            //};


            var cachedImage = new CachedImage()
            {
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
                WidthRequest = 70,
                HeightRequest = 70,
                CacheDuration = TimeSpan.FromDays(0), //30 days
                DownsampleToViewSize = true,
                RetryCount = 0,
                RetryDelay = 250,
                BitmapOptimizations = false,
                LoadingPlaceholder = "tab_feed.png",
                ErrorPlaceholder = "xamarin_logo.png",
                //    Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
//                Transformations = _myListOfTransformations
//                Transformations =
            	Transformations = new System.Collections.Generic.List<ITransformation>() 
                {
            		new GrayscaleTransformation(),
		            new CircleTransformation(),
	            }
            };

            //cachedImage.Transformations = FFImageLoading.Transformations.CircleTransformation;
            //new FFImageLoading.  Transformations.CircleTransformation()

            cachedImage.SetBinding(CachedImage.SourceProperty, nameof(model.FBProfileUrl));


			var userImage = new Image() { };
    		var myNameProperty = new Label() { };
    		var myPrayerRequestProperty = new Label() { };
            var thoughtButton = new Button() { 
                Text = "Thought"
            };
            var prayerButton = new Button() { 
                Text = "Prayer"
            };

            var dateString = new Label() { 
            };

            //CONFIRMED WORKS
   //         DateTime dt = DateTime.Now;
			//CultureInfo ci = new CultureInfo("en-US");
   //         string sampleDateTimeStringMonth = dt.ToString("MMM d", ci);
			//string sampleDateTimeStringTime = dt.ToString("h:mm tt", ci);
   //         string sampleDateTimeStringConnected = ($"{sampleDateTimeStringMonth} at {sampleDateTimeStringTime}");
			//dateString.Text = sampleDateTimeStringConnected;

			//NOW BINDING TO DATETIMEOFFSET WITH TRANSFORMATION
            dateString.SetBinding(Label.TextProperty, nameof(model.CreatedDateTime), BindingMode.OneWay, new DateTimeToStringConverter());



			//dateString.SetBinding(Label.TextProperty, sampleDateTimeString);

			//dateString.SetBinding(Label.TextProperty, nameof(model.CreatedDateTimeString));

			//var oneSurveyQuestion = new Object()
			//{
			//	CreateDateString = sampleDateTimeString
			//};

			//dateString.SetBinding(Label.TextProperty, nameof(model.CreatedDateTime), );

			//DateTime dt = DateTime.Now;
			//CultureInfo ci = new CultureInfo("en-US");
			//string sampleDateTimeString = dt.ToString("MMM d h:mm tt", ci);

			//SurveyQuestion oneSurveyQuestion = new SurveyQuestion()
			//{
			//	CreateDateString = sampleDateTimeString
			//};


			userImage.SetBinding(Image.SourceProperty, nameof(model.FBProfileUrl));
            myNameProperty.SetBinding(Label.TextProperty, nameof(model.FullName));
            myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(model.PrayerRequestText));
			//thoughtButton.SetBinding(Button.);

			//thoughtButton.SetBinding(Button.CommandProperty, new Binding(".ThoughtClickCommand"));

			var navigationPage = Application.Current.MainPage as NavigationPage;
            var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
            var prayerListViewModel = prayerListPage.BindingContext as PrayerListViewModel;

            //METHOD 1 - CONFIRMED
            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.ThoughtClickCommand", source: new PrayerListPage()));

            //METHOD 2 - CONFIRMED!
            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.ThoughtClickCommand", source: prayerListPage));

            prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.PrayerClickCommand", source: prayerListPage));

            //            thoughtButton.SetBinding(Button.BindingContextProperty, new Binding("."));
            //         thoughtButton.SetBinding(Button.BindingContextProperty, new Binding("."));
            ////thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            //thoughtButton.SetBinding(Button.TextProperty, "ButtonText" );
            //thoughtButton.SetBinding(Button.CommandProperty, new Binding(nameof(ViewModels.PrayerListViewModel.ThoughtClickCommand)));


            //https://forums.xamarin.com/discussion/58709/context-actions-command-binding-with-parameter#latest

            //thoughtButton.SetBinding(Button.CommandProperty, nameof(prayerListViewModel.ThoughtClickCommand));
            //thoughtButton.BindingContext = new Binding(".");

            //_firstButton.SetBinding(Button.CommandProperty, nameof(MyViewModel.MyFavoriteCommand));

            var textStack = new StackLayout
            {
                //Padding = 10, //E
                //HorizontalOptions = LayoutOptions.FillAndExpand, //NE
                Margin = new Thickness(0, 3, 0, 0),
                //VerticalOptions = LayoutOptions.FillAndExpand, //NE
                Orientation = StackOrientation.Vertical,
                Children =
                    {
                        myNameProperty,
                        dateString,
                        myPrayerRequestProperty,
                        thoughtButton,
                        prayerButton
                    }
            };

            var cellLayoutStack = new StackLayout
            {
                Margin = new Thickness(0, 0, 0, 0),
                //Spacing = 10, //default is 6
                Orientation = StackOrientation.Horizontal,
                Children =
                    {
                        cachedImage,
    //					userImage,
    					textStack
                    }
            };

            View = cellLayoutStack;

            //MenuItem ITEM AND CONTEXT ACTIONS
            //var moreAction = new MenuItem { Text = "More" };
            //moreAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            //moreAction.Clicked += async (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    Debug.WriteLine("More Context Action clicked: " + mi.CommandParameter);
            //};
            // add to the ViewCell's ContextActions property
            //ContextActions.Add(moreAction);

            _deleteAction = new MenuItem { Text = "Delete", IsDestructive = true }; // red background
            _deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));

            //_deleteAction.Clicked += async (sender, e) =>
            //{
            //    var mi = ((MenuItem)sender);
            //    Debug.WriteLine("Delete Context Action clicked: " + mi.CommandParameter);
            //};

            ContextActions.Add(_deleteAction);
        }

    	protected override void OnAppearing()
    	{
    		base.OnAppearing();
    		_deleteAction.Clicked += HandleDeleteActionClicked;
    	}

    	protected override void OnDisappearing()
    	{
    		base.OnDisappearing();
    		_deleteAction.Clicked -= HandleDeleteActionClicked;
    	}

    	private void HandleDeleteActionClicked(object sender, EventArgs e)
    	{
    		//throw new NotImplementedException();

    		var myMenuItem = (MenuItem)sender;
            var selectedModel = myMenuItem.BindingContext as PrayerRequest;

    		//DELETE FROM DATABASE - AND BEFORE REFRESH THE DATA SOURCE ON THE UI 
    		App.PrayerSQLDatabase.DeletePrayerRequest(selectedModel);
			
			//Wait for the iOS animation to finish
			switch (Device.RuntimePlatform)
    		{
    			case Device.iOS:
    				Task.Delay(250);
    				break;
    		}

    		var navigationPage = Application.Current.MainPage as NavigationPage;
            var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
    		var prayerListViewModel = prayerListPage.BindingContext as PrayerListViewModel;

            var myMenuItemCommandParameter = (PrayerRequest)((MenuItem)sender).CommandParameter;

            prayerListViewModel.DeletePrayerFromListCommand.Execute(myMenuItemCommandParameter);

			//^ MAHDI - just remove the specific item and the observable collection + INotifyPropertyChanged will auto-update the UI as necessary
			//FormList.Remove(item); //MAHDI

			//> BM dogListPhotoViewModel.RefreshAllDataCommand?.Execute(true); //
			//BM - refresh all the data
		}
    }
}

//namespace ThoughtsAndPrayersThree.Page.ViewCells
//{
//	//public class PrayerViewCell : ContentPage
//	//{
//	//    public PrayerViewCell()
//	//    {
//	//        Content = new StackLayout
//	//        {
//	//            Children = {
//	//                new Label { Text = "Hello ContentPage" }
//	//            }
//	//        };
//	//    }
//	//}
//}