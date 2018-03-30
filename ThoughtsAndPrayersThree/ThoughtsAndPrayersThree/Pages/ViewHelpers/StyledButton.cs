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
            BackgroundColor = MyColors.Clouds;
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
            BackgroundColor = MyColors.MyBlue1;
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
                    BorderColor = MyColors.Clouds;
                    BorderWidth = 1;
                    break;
            }
        } 

    } // END STYLED BUTTON
}