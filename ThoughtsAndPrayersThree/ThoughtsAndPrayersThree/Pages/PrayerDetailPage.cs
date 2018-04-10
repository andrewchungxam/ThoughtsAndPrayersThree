﻿using System;
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


    //METHOD 1: USING ITEM SELECTED DIRECTION AS ARGUMENT

    //public class PrayerDetailPage : ContentPage
    //{
    //    public static PrayerListViewModel ParentViewModelOfDetailPage;

    //    public PrayerDetailPage(PrayerRequest itemSelected)
    //    {
    //        BindingContext = itemSelected;

    //        var cachedImage = new CachedImage()
    //        {
    //            //HorizontalOptions = LayoutOptions.Center,
    //            //VerticalOptions = LayoutOptions.Center,
    //            WidthRequest = 50,
    //            HeightRequest = 50,
    //            CacheDuration = TimeSpan.FromDays(30),
    //            DownsampleToViewSize = true,
    //            RetryCount = 0,
    //            RetryDelay = 250,
    //            BitmapOptimizations = false,
    //            LoadingPlaceholder = "tab_feed.png",
    //            ErrorPlaceholder = "xamarin_logo.png",
    //            //Source = "http://loremflickr.com/600/600/nature?filename=simple.jpg",
    //            Transformations = new System.Collections.Generic.List<ITransformation>()
    //                {
    //                    //new GrayscaleTransformation(),
    //                    new CircleTransformation(),
    //                }
    //        };
    //        var myTestProperty = new Label { Text = "hello" };

    //        var myNameProperty = new Label() { };
    //        var myFullNameProperty = new Label() { };
    //        var myPrayerRequestProperty = new Label() { };

    //        var thoughtButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
    //        {
    //            Text = "Thought"
    //        };
    //        var prayerButton = new ReverseStyledButton(ReverseStyledButton.Borders.Thin, 1)
    //        {
    //            Text = "Prayer"
    //        };

    //        var dateString = new Label()
    //        {
    //        };

    //        var theNumberOfThoughtLabel = new Label() { };
    //        var theNumberOfPrayersLabel = new Label() { };

    //        var theCombinedNumberOfThoughtsAndPrayersLabel = new Label();

    //        #region BINDINGS
    //        //cachedImage.SetBinding(CachedImage.SourceProperty, nameof(Models.PrayerRequest.FBProfileUrl));
    //        cachedImage.SetBinding(CachedImage.SourceProperty, nameof(itemSelected.FBProfileUrl));

    //        dateString.SetBinding(Label.TextProperty, nameof(itemSelected.CreatedDateTime), BindingMode.OneWay, new DateTimeToStringConverter());

    //        theNumberOfThoughtLabel.SetBinding(Label.TextProperty, nameof(itemSelected.NumberOfThoughts), BindingMode.OneWay, new NumberOfThoughtsIntToStringConverter());
    //        theNumberOfPrayersLabel.SetBinding(Label.TextProperty, nameof(itemSelected.NumberOfPrayers), BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

    //        theCombinedNumberOfThoughtsAndPrayersLabel.SetBinding(Label.TextProperty, nameof(itemSelected.CombinedNumberOfThoughtsAndPrayers), BindingMode.Default); //, new CombinedNumberOfThoughtsAndPrayersStringConverter());

    //        //NEW
    //        myFullNameProperty.SetBinding(Label.TextProperty, nameof(itemSelected.NewCombinedNameAndDate), BindingMode.Default);
    //        myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(itemSelected.PrayerRequestText));

    //        //var navigationPage = Application.Current.MainPage as NavigationPage;
    //        //var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
    //        //var prayerListViewModel = prayerListPage.BindingContext as PrayerListViewModel;

    //        //NEEDED??????
    //        //var navigationPage = Application.Current.MainPage as NavigationPage;
    //        //var prayerDetailPage = navigationPage.CurrentPage as PrayerDetailPage;
    //        //var prayerDetailPageViewModel = prayerDetailPage.BindingContext as PrayerDetailPageViewModel;


    //        ////COMBINED-COMMANDING-NEW
    //        //thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
    //        //thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddThoughtClickCommand", source: prayerDetailPage));

    //        //prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
    //        //prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddPrayerClickCommand", source: prayerDetailPage));

    //        #endregion

    //        //Content = new StackLayout
    //        //{
    //        //    Children = {
    //        //        cachedImage, dateString, theCombinedNumberOfThoughtsAndPrayersLabel, myFullNameProperty, myPrayerRequestProperty
    //        //    }
    //        //};


    //        #region GRID DEFINITION
    //        var grid = new Grid()
    //        {
    //            Padding = new Thickness(10, 10, 10, 10)
    //        };

    //        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
    //        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
    //        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
    //        grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

    //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
    //        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

    //        grid.Children.Add(cachedImage, 0, 0);
    //        Grid.SetRowSpan(cachedImage, 2);
    //        //Grid.SetColumnSpan(cachedImage, 2);

    //        grid.Children.Add(myFullNameProperty, 1, 0);
    //        Grid.SetColumnSpan(myFullNameProperty, 3);

    //        //TODO
    //        //DELETE

    //        //grid.Children.Add(myTestProperty, 1, 0);
    //        //Grid.SetColumnSpan(myTestProperty, 3);

    //        grid.Children.Add(theCombinedNumberOfThoughtsAndPrayersLabel, 1, 1);
    //        Grid.SetColumnSpan(theCombinedNumberOfThoughtsAndPrayersLabel, 3);

    //        grid.Children.Add(myPrayerRequestProperty, 0, 2);
    //        Grid.SetColumnSpan(myPrayerRequestProperty, 4);

    //        grid.Children.Add(thoughtButton, 0, 3);
    //        Grid.SetColumnSpan(thoughtButton, 2);
    //        grid.Children.Add(prayerButton, 2, 3);
    //        Grid.SetColumnSpan(prayerButton, 2);

    //        Content = grid;
    //        #endregion

    //    }
    //}

    //METHOD 2: USING ITEM SELECTED DIRECTION AS ARGUMENT

    public class PrayerDetailPage :  BaseContentPage<PrayerDetailPageViewModel>
    {
        public static PrayerListViewModel ParentViewModelOfDetailPage;

        public Lottie.Forms.AnimationView sampleAnimationView;
        public Lottie.Forms.AnimationView sampleAnimationView2;
        public Lottie.Forms.AnimationView sampleAnimationViewPlaceholder;

        public StackLayout myStackLayout;

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

            //var myTestLabel = new Label() { Text = "hello" };

            var myTestLabel = new Label(); //{ Text = "hello" };
            myTestLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNumberOfPrayers), BindingMode.Default, new NumberOfPrayersIntToStringConverter());
            //myTestLabel.SetBinding(Label.TextProperty, "TheNumberOfPrayers", BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

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
            //cachedImage.SetBinding(CachedImage.SourceProperty, nameof(Models.PrayerRequest.FBProfileUrl));
            cachedImage.SetBinding(CachedImage.SourceProperty, nameof(this.MyViewModel.TheFBProfileUrl));

            dateString.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheCreatedDateTime ), BindingMode.OneWay, new DateTimeToStringConverter());

            theNumberOfThoughtLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNumberOfThoughts), BindingMode.OneWay, new NumberOfThoughtsIntToStringConverter());
            theNumberOfPrayersLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNumberOfPrayers), BindingMode.OneWay, new NumberOfPrayersIntToStringConverter());

            theCombinedNumberOfThoughtsAndPrayersLabel.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheCombinedNumberOfThoughtsAndPrayers), BindingMode.Default); //, new CombinedNumberOfThoughtsAndPrayersStringConverter());

            //NEW

            myFullNameProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.TheNewCombinedNameAndDate), BindingMode.Default);
            myPrayerRequestProperty.SetBinding(Label.TextProperty, nameof(this.MyViewModel.ThePrayerRequestText));

            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //var prayerListPage = navigationPage.CurrentPage as PrayerListPage;
            //var prayerListViewModel = prayerListPage.BindingContext as PrayerListViewModel;

            //NEEDED??????
            //var navigationPage = Application.Current.MainPage as NavigationPage;
            //var prayerDetailPage = navigationPage.CurrentPage as PrayerDetailPage;
            //var prayerDetailPageViewModel = prayerDetailPage.BindingContext as PrayerDetailPageViewModel;

            //var theBindingContextViewModel = this.BindingContext;

            //////COMBINED-COMMANDING-NEW
            thoughtButton.SetBinding(Button.CommandParameterProperty, new Binding("SelectedPrayerRequest"));  //("BindingContext.SelectedPrayerRequest"));  //new Binding("."));
            thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddThoughtClickCommand", source: this));

            //thoughtButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddThoughtClickCommand", source: theBindingContextViewModel));

            //            prayerButton.SetBinding(Button.CommandParameterProperty, new Binding("."));
            ////            prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddPrayerClickCommand", source: prayerDetailPage));
            //prayerButton.SetBinding(Button.CommandProperty, new Binding("BindingContext.AddPrayerClickCommand", source: theBindingContextViewModel));


            var button3 = new Button { Text = "Tap Me" };
            Label label3 = new Label { Text = "I can be toggled" };

            button3.Clicked += OnButtonActivated3;

            sampleAnimationView = new Lottie.Forms.AnimationView() { 
                Animation = "beating_heart.json",
                Loop = true,
                AutoPlay = false,
                BackgroundColor = MyColors.MyBlue1
                //IsVisible = false

            };

            sampleAnimationViewPlaceholder = new Lottie.Forms.AnimationView()
            {
                Animation = "beating_heart.json",
                Loop = true,
                AutoPlay = false,

                //IsVisible = false
            };


            var animationButton = new Button() { 
                Text = "left"
            };

            animationButton.Clicked += OnBackButtonClickedLeft;


            sampleAnimationView2 = new Lottie.Forms.AnimationView()
            {
                Animation = "like_button.json",
                Loop = true,
                AutoPlay = false,
                BackgroundColor = MyColors.MyBlue1


            };

            var animationButton2 = new Button()
            {
                Text = "right"
            };

            animationButton2.Clicked += OnBackButtonClickedRight;


            myStackLayout = new StackLayout() { 
                Children = {label3, sampleAnimationView}
            };

            var myStackLayoutButton3 = new Button()
            {
                Text = "Stack 3"
            };

            myStackLayoutButton3.Clicked += OnButtonStackLayoutThree;



            void OnButtonActivated3(object sender, EventArgs args)
            {
                label3.IsVisible = !label3.IsVisible;
                sampleAnimationView.IsVisible = !sampleAnimationView.IsVisible;
                sampleAnimationViewPlaceholder.IsVisible = !sampleAnimationViewPlaceholder.IsVisible;
            }

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

            //grid.Children.Add(sampleAnimationView, 0, 5);
            //Grid.SetColumnSpan(sampleAnimationView, 1);
            grid.Children.Add(animationButton, 0,4);
            Grid.SetColumnSpan(animationButton, 3);

            //grid.Children.Add(sampleAnimationView2, 1, 5);
            //Grid.SetColumnSpan(sampleAnimationView2, 1);

            grid.Children.Add(animationButton2, 3, 4);
            Grid.SetColumnSpan(animationButton2, 3);

            //grid.Children.Add(label3, 0, 7);
            //Grid.SetColumnSpan(label3, 1);
            //grid.Children.Add(button3, 1, 7);
            //Grid.SetColumnSpan(button3, 1);

            //grid.Children.Add(myStackLayout, 0, 8);
            //Grid.SetColumnSpan(myStackLayout, 1);
            //grid.Children.Add(myStackLayoutButton3, 1, 8);
            //Grid.SetColumnSpan(myStackLayoutButton3, 1);


            //grid.Children.Add(myTestLabel, 0, 0);
            //Grid.SetColumnSpan(myTestLabel, 2);

            Content = grid;
            #endregion

        }

        private void OnButtonStackLayoutThree(object sender, EventArgs e)
        {
            myStackLayout.IsVisible = !myStackLayout.IsVisible;
        }

        private void OnBackButtonClickedRight(object sender, EventArgs e)
        {
            sampleAnimationViewPlaceholder.IsVisible = !sampleAnimationViewPlaceholder.IsVisible;
            sampleAnimationView2.Play();
        }

        private void OnBackButtonClickedLeft(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            sampleAnimationView.Play();
        }


    }






}

