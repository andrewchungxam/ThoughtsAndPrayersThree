using System;
using ThoughtsAndPrayersThree.CustomRenderers;
using Xamarin.Forms;

using ThoughtsAndPrayersThree.Pages;

using XFGloss;


namespace ThoughtsAndPrayersThree
{
	public class MainPage : TabbedPage
	{
		public MainPage()
		{
            this.BarBackgroundColor = Color.Blue;
            this.BarTextColor = Color.Red;

            //BackgroundColor = Color.Red;

			Page itemsPage, aboutPage = null;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:

                    itemsPage = new NavigationPage(new PrayerListPage()
                        {
                            Title = "Hello in Main Page",
						//						    BackgroundColor = Color.Blue



			})
					{
						Title = "Tab Browse",
//       //                 BarBackgroundColor = Color.Black,
//		//				BarTextColor = Color.White
						//						BarBackgroundColor = Color.FromHex("#01FFFFFF"),
						//BackgroundColor = Color.Blue
					};

                    aboutPage = new NavigationPage(new AboutPage()
                        {
                            Title = "Hello in About Page",
                       // BackgroundColor = Color.Blue
                        })
					{
						Title = "Tab About",
//						BarBackgroundColor = Color.FromHex("#01FFFFFF"),
						//BackgroundColor = Color.Blue

					};

					itemsPage.Icon = "tab_feed.png";
					aboutPage.Icon = "tab_about.png";
					
                    break;
				default:
					itemsPage = new PrayerListPage()
					{
						Title = "Tab Browse"
					};

					aboutPage = new AboutPage()
					{
						Title = "Tab About"
					};
					break;
			}

			Children.Add(itemsPage);
			Children.Add(aboutPage);

			Title = Children[0].Title;
		}

		protected override void OnCurrentPageChanged()
		{
			base.OnCurrentPageChanged();
			Title = CurrentPage?.Title ?? string.Empty;
		}
	}
}

