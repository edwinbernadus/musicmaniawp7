using System.Windows;
using System.Windows.Controls;

namespace Music
{
    public class DisplayHelper
    {
        public static void SetProgressBar(ProgressBar progressBar,bool input=false)
        {
            if (input)
            {
                progressBar.Visibility = Visibility.Visible;
                progressBar.IsIndeterminate = true;
            }
            else
            {
                progressBar.Visibility = Visibility.Collapsed;
                progressBar.IsIndeterminate = false;
            }
        }
    }
}