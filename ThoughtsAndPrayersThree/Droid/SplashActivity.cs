
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Lottie.Forms.Droid;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Transformations;
using Android.Support.V7.App;

namespace ThoughtsAndPrayersThree.Droid
{
    //[Activity(Label = "SplashActivity")]
    //[Activity(Label = "ThoughtsAndPrayersThree.Droid", Icon = "@drawable/Android_ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "ThoughtsAndPrayersThree.Droid", Icon = "@drawable/Android_ic_launcher", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
            //Dispose(Resource.Drawable.DrawableLogoSample);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            this.Dispose();

            //CAREFUL WITH MEMORY USAGE OF BITMAP -- COULD ALSO USE LOWER FOOTPRINT BACKGROUND COLOR AND LOGO WITH TRANSPARENT BACKGROUND
            //NOTE JON PRYORS NOTE HERE:    
            //https://bugzilla.xamarin.com/show_bug.cgi?id=3024

        }
    }
}
