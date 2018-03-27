using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ThoughtsAndPrayersThree;
using Xamarin.Forms;
using System.Threading.Tasks;
using ThoughtsAndPrayersThree.Constants;

namespace ThoughtsAndPrayersThree.Pages.ViewHelpers
{
    public class ReverseStyledButton : Button
    {

        public enum Borders
        {
            None,
            Thin
        }

        public ReverseStyledButton(Borders border, double opacity = 0)
        {
            //BackgroundColor = Color.Transparent;
            //BackgroundColor = Color.FromHex ("#2980b9");
            BackgroundColor = MyColors.Clouds;
            //BackgroundColor = MyColors.WetAsphalt;
            //          TextColor = Color.White;
            TextColor = MyColors.MyBlue1;
            FontSize = 18;
            Opacity = opacity;
            WidthRequest = 100;
            FontFamily = Device.OnPlatform(
                iOS: "AppleSDGothicNeo-Light",
                Android: "Droid Sans Mono",
                WinPhone: "Times New Roman" //WinPhone: "Comic Sans MS"
            );

            switch (border)
            {
                case Borders.None:
                    break;
                case Borders.Thin:
                    BorderRadius = 3;
                    //              BorderColor = Color.White;
                    BorderColor = MyColors.Clouds;
                    BorderWidth = 1;
                    break;
            }
        }
    } // END REVERSE STYLED BUTTON




    public class StyledButton : Button
    {

        public enum Borders
        {
            None,
            Thin
        }

        public StyledButton(Borders border, double opacity = 0)
        {
            //BackgroundColor = Color.Transparent;
            //BackgroundColor = Color.FromHex ("#2980b9");
            BackgroundColor = MyColors.MyBlue1;
            //BackgroundColor = MyColors.WetAsphalt;
            //          TextColor = Color.White;
            TextColor = MyColors.Clouds;
            FontSize = 18;
            Opacity = opacity;
            WidthRequest = 100;
            FontFamily = Device.OnPlatform(
                iOS: "AppleSDGothicNeo-Light",
                Android: "Droid Sans Mono",
                WinPhone: "Times New Roman" //WinPhone: "Comic Sans MS"
            );

            switch (border)
            {
                case Borders.None:
                    break;
                case Borders.Thin:
                    BorderRadius = 3;
                    //              BorderColor = Color.White;
                    BorderColor = MyColors.Clouds;
                    BorderWidth = 1;
                    break;
            }
        } 

    } // END STYLED BUTTON
}


    //public class LoginPage : ContentPage
    //{

    //    protected override async void OnAppearing()
    //    {
    //        base.OnAppearing();
    //        //          await RefreshData ();

    //        //this doesn't get called when the webview comes back
    //        if (AppConstants.Authenticated == true)
    //        {
    //            //do something amazing!!!!
    //            Debug.WriteLine("He is logged in");
    //            Navigation.PushModalAsync(new StartingMasterDetailPage());

    //        }
    //    }

    //    public LoginPage()
    //    {
    //        //BACKGROUND COLOR
    //        //BackgroundColor = Color.FromHex ("#298555");
    //        BackgroundColor = MyColors.MyLightPurple;
    //        NavigationPage.SetHasNavigationBar(this, false);

    //        //LABEL
    //        Label welcomeLabel = new Label
    //        {
    //            HorizontalTextAlignment = TextAlignment.Center,
    //            VerticalTextAlignment = TextAlignment.Start,
    //            Text = "Thoughts and Prayers",
    //            //TextColor = Color.White
    //            TextColor = MyColors.MidnightBlue
    //        };

    //        Label deleteSpacingLabel = new Label
    //        {
    //            HorizontalTextAlignment = TextAlignment.Center,
    //            VerticalTextAlignment = TextAlignment.Start,
    //            Text = "      ",
    //            TextColor = Color.White
    //        };


    //        welcomeLabel.FontFamily = Device.OnPlatform(
    //            iOS: "AppleSDGothicNeo-Light",
    //            Android: "Droid Sans Mono",
    //            WinPhone: "Comic Sans MS"
    //        );

    //        //BUTTON
    //        Button loginButton = new StyledButton(StyledButton.Borders.Thin, 1);
    //        loginButton.Text = "Facebook Login";
    //        //loginButton.WidthRequest = 50;

    //        //loginButton.Clicked += (sender, e) => 

    //        //{
    //        //  if (AppConstants.Authenticated == false) 
    //        //  {
    //        //      Device.BeginInvokeOnMainThread (NextMethod ());
    //        //  } 
    //        //  else                
    //        //  { 
    //        //      Navigation.PushModalAsync (new StartingMasterDetailPage ());
    //        //  }

    //        loginButton.Clicked += async (sender, e) => {
    //            if (AppConstants.Authenticated == false)
    //            {
    //                if (App.Authenticator != null)
    //                {
    //                    AppConstants.Authenticated = await App.Authenticator.Authenticate();
    //                    if (AppConstants.Authenticated)
    //                    {

    //                        //great but not too many Dependency service
    //                        //var  userinfo = await DependencyService.Get<IFacebookID> ().GetFacebookIdAsync ();

    //                        //AppConstants.FBIdentityID = userinfo;



    //                        //int j =10;
    //                        //new
    //                        Navigation.PushModalAsync(new StartingMasterDetailPage());


    //                    }
    //                }
    //            }
    //            else
    //            {
    //                Navigation.PushModalAsync(new StartingMasterDetailPage());
    //            }



    //        };

    //        //              {
    //        //November 2016
    //        //Navigation.PushAsync (new NavigationPage (new SamplePage ()) { //BarBackgroundColor = Color.Black  }   );  //FromHex ("#298555")

    //        //December 01.2016 - FREEZE
    //        //Navigation.PushAsync (new SamplePage ());

    //        //DECEMBER 05.2016
    //        //Navigation.PushAsync (new TheTabbedPage ());
    //        //Navigation.RemovePage (this);


    //        //try
    //        //          {
    //        //                  var cloudService = ServiceLocator.Instance.Resolve<ICloudService> ();
    //        //                  cloudService.LoginAsync ();
    //        //Application.Current.MainPage = new NavigationPage (new StartingMasterDetailPage ()); //Pages.TaskList ());

    //        //} catch (Exception ex) {  

    //        //ADDING FACEBOOK LOGIN
    //        //                  //DECEMBER 06.2016
    //        //                  Navigation.PushModalAsync (new StartingMasterDetailPage ());

    //        //              });





    //        //          };

    //        //https://forums.xamarin.com/discussion/18670/define-layout-sizes-in-inches-with-idisplay-extension-code-only

    //        //SET THE CONTENT ON THE PAGE
    //        Content = new StackLayout
    //        {
    //            VerticalOptions = LayoutOptions.Center,
    //            HorizontalOptions = LayoutOptions.CenterAndExpand,
    //            Children =
    //            {
    //                welcomeLabel, deleteSpacingLabel, loginButton
    //            }
    //        };




    //    }

    //    //static Action NewMethod ()
    //    //{
    //    //  return async () => {
    //    //                                                       //}
    //    //  };
    //    //}

    //    static Action NextMethod()
    //    {
    //        return async () => {
    //            if (App.Authenticator != null)
    //            {
    //                AppConstants.Authenticated = await App.Authenticator.Authenticate();
    //            }
    //        };
    //    }




    //}


//}


