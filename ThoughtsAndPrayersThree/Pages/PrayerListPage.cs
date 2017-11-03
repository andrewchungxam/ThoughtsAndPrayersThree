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

        public PrayerListPage()
        {
            _prayerListPage = new ListView();

            var _prayerCellTemplate = new DataTemplate(typeof(PrayerViewCell));
            _prayerListPage.ItemTemplate = _prayerCellTemplate;
            _prayerListPage.SetBinding(ListView.ItemsSourceProperty,nameof(MyViewModel.ObservableCollectionOfPrayers ));

			_prayerListPage.HasUnevenRows = true;

            Content = _prayerListPage;

//			var contentView = new ContentView()
//			{
//				//BackgroundColor = Color.FromHex("#7000"),
//				//VerticalOptions = LayoutOptions.FillAndExpand,
//				//HorizontalOptions = LayoutOptions.FillAndExpand,
//				//Content = new Label()
//				//{
//				//   Text = "hello there"
//				//}

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
//				Loop = false,
//				AutoPlay = false,
//				HorizontalOptions = LayoutOptions.Center,
//				VerticalOptions = LayoutOptions.Center,
//			};
//			contentView.Content = _animation;
//			contentView.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsTheViewVisible));
//            AbsoluteLayout.SetLayoutFlags
//            (
//                contentView,
////              AbsoluteLayoutFlags.PositionProportional
   //             AbsoluteLayoutFlags.All
   //         );
   //         AbsoluteLayout.SetLayoutBounds
   //         (
   //             contentView,
   //             new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
   //         );

			//var contentView1 = new ContentView()
			//{
			//	BackgroundColor = Color.FromHex("#7000"),
			//	VerticalOptions = LayoutOptions.FillAndExpand,
			//	HorizontalOptions = LayoutOptions.FillAndExpand
			//};
			//_animation1 = new AnimationView
			//{
			//	//Animation = "beating_heart.json",
			//	Animation = "like_button.json",
			//	WidthRequest = 290,
			//	HeightRequest = 290,
			//	Loop = false,
			//	AutoPlay = false,
			//	HorizontalOptions = LayoutOptions.Center,
			//	VerticalOptions = LayoutOptions.Center,
			//};
			//contentView1.Content = _animation1;
			//contentView1.SetBinding(IsVisibleProperty, nameof(MyViewModel.IsTheView1Visible));
			//AbsoluteLayout.SetLayoutFlags
			//(
			//	contentView1,
			////                AbsoluteLayoutFlags.PositionProportional
			//AbsoluteLayoutFlags.All
			//);
			//AbsoluteLayout.SetLayoutBounds
			//(
			//	contentView1,
			//	new Rectangle(.5, .5, 1, 1)//AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
			//);

   //         AbsoluteLayout simpleLayout = new AbsoluteLayout
			//{
			//	//#TODO - BACKGROUND
			//	//BackgroundColor = Color.Blue.WithLuminosity(0.9),
			//	VerticalOptions = LayoutOptions.FillAndExpand
			//};

			//AbsoluteLayout.SetLayoutFlags
   //         (
			//	 _prayerListPage,
	  //          AbsoluteLayoutFlags.PositionProportional
   //         );

			//AbsoluteLayout.SetLayoutBounds
			//(
			//	_prayerListPage,
			//	new Rectangle(0, 0, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
			//);

			//simpleLayout.Children.Add(_prayerListPage);
			//simpleLayout.Children.Add(contentView);
			//simpleLayout.Children.Add(contentView1);

			////Content = simpleLayout;

			//Content = new StackLayout
			//{
			//    Children = {
			//        new Label { Text = "Hello ContentPage" }
			//    }
			//};
		}
    }
}

