////A SIMPLE SAMPLE PAGE USING ANIMATIONS

//using System;
//using Lottie.Forms;
//using Xamarin.Forms;

//namespace ThoughtsAndPrayersThree.Pages
//{
//    public class TestAnimationPage : ContentPage
//    {
//		AnimationView _animation;

//        public TestAnimationPage()
//        {
//			var contentView = new ContentView()
//			{
//				BackgroundColor = Color.FromHex("#7000"),
//				VerticalOptions = LayoutOptions.FillAndExpand,
//				HorizontalOptions = LayoutOptions.FillAndExpand
//			};

//			_animation = new AnimationView
//			{
//				//Animation = "LottieLogo1.json",
//				//Animation = "love_explosion.json",
//				//Animation = "checked_done_.json",
//				Animation = "beating_heart.json",
//				//Animation = "like_button.json",
//				WidthRequest = 290,
//				HeightRequest = 290,
//				Loop = true,
//				AutoPlay = true,
//				//Loop = false,
//				//AutoPlay = false,
//				HorizontalOptions = LayoutOptions.Center,
//				VerticalOptions = LayoutOptions.Center,
//			};
//			contentView.Content = _animation;
//			//this.MyViewModel.IsTheViewVisible = true;
//			//contentView.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsTheViewVisible));
//			AbsoluteLayout.SetLayoutFlags
//			(
//				contentView,
//				//              AbsoluteLayoutFlags.PositionProportional
//				AbsoluteLayoutFlags.All
//			);
//			AbsoluteLayout.SetLayoutBounds
//			(
//				contentView,
//				//new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
//				new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)

//			);

//			AbsoluteLayout simpleLayout = new AbsoluteLayout
//			{
//				//#TODO - BACKGROUND
//				//BackgroundColor = Color.Blue.WithLuminosity(0.9),
//				VerticalOptions = LayoutOptions.FillAndExpand
//			};

//            simpleLayout.Children.Add(_animation);

//            Content = simpleLayout;
//        }
//    }
//}

