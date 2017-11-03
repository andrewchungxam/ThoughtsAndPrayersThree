using System;

using Xamarin.Forms;
using XFGloss;

namespace ThoughtsAndPrayersThree
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {

            var bkgrndGradient = new Gradient()
			{
				Rotation = 150,
				Steps = new GradientStepCollection()
				{
					new GradientStep(Color.Red, 0),
					new GradientStep(Color.White, .25),
					new GradientStep(Color.Blue, .5),
					new GradientStep(Color.Green, .75),
					new GradientStep(Color.BurlyWood, 1),

//                  new GradientStep(Color.FromHex("#ccd9ff"), 1)
//                  new GradientStep(Color.Black, 1)
                }
			};

			ContentPageGloss.SetBackgroundGradient(this, bkgrndGradient);

			Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}

