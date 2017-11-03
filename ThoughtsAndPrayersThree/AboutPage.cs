using System;

using XFGloss;

using Xamarin.Forms;

namespace ThoughtsAndPrayersThree
{
    public class AboutPage : ContentPage
    {
        public AboutPage()
        {
            //BackgroundColor = Color.Red;

			var bkgrndGradient = new Gradient()
			{
				Rotation = 150,
				Steps = new GradientStepCollection()
				{
					//new GradientStep(Color.Red, 0),
					new GradientStep(Color.White, .25),
                    new GradientStep(Color.FromHex("#ccd9ff"), 1)

                    //new GradientStep(Color.Blue, .5),
                    //new GradientStep(Color.Green, .75),
                    //new GradientStep(Color.BurlyWood, 1),

//                  new GradientStep(Color.Black, 1)
                }
			};

			ContentPageGloss.SetBackgroundGradient(this, bkgrndGradient);

            this.Title = "Hello in the actual About Page";

            Label theLabel = new Label { Text = "About Page : Hello Content Page" };


            //         var theStack = new StackLayout()
            //         {
            //             //BackgroundColor = Color.Blue,   
            //             Children =
            //             {
            //                 new Label { Text = "About Page : Hello Content Page"}
            //             }
            //         };

            //Content = theStack;


            Content = theLabel;


			//Content = new StackLayout
            //{
            //    //BackgroundColor = Color.FromHex("555555"),
            //    Children = {
            //        new Label { Text = "Hello ContentPage" }
            //    }
            //};
        }
    }
}

