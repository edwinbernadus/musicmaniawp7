using System.Windows;
using System.Windows.Controls;

namespace MusicLogicWp7.Helper
{
    public class ProgressBarHelper
    {
        public static void TurnOff(ProgressBar MainProgressBar)
        {

            MainProgressBar.IsEnabled = false;
            MainProgressBar.Visibility = Visibility.Collapsed;
            MainProgressBar.IsIndeterminate = false;

        }

        public static void TurnOn(ProgressBar MainProgressBar)
        {


            MainProgressBar.IsEnabled = true;
            MainProgressBar.Visibility = Visibility.Visible;
            MainProgressBar.IsIndeterminate = true;

        }
    }
}