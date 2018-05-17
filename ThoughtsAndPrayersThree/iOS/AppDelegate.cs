using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Transformations;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;

namespace ThoughtsAndPrayersThree.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //var ignore = new CircleTransformation();

            global::Xamarin.Forms.Forms.Init();

			AnimationViewRenderer.Init();
            CachedImageRenderer.Init();

			LoadApplication(new App());





            return base.FinishedLaunching(app, options);
        }
    }
}
