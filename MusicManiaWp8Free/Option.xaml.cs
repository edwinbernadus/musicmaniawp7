using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MusicLogicExtendedWp7.Repository;

namespace MusicManiaWp7
{
    public partial class Option : PhoneApplicationPage
    {
        public Option()
        {
            InitializeComponent();
        }

        private async Task Init()
        {
            if (ParameterRepository.IsThisWp8Version)
            {
                if (await GlobalManager.IsEnableCopyToMusicHub())
                {
                    bool data = await OneValueRepository.Get(OneValueRepository.AutomaticlyCopyMusicHub.Item1,
                                                             OneValueRepository.AutomaticlyCopyMusicHub.Item2);
                    ToggleSwitch.IsChecked = data;
                    ToggleSwitch.IsEnabled = true;
                }
                else
                {
                    ToggleSwitch.IsEnabled = false;
                }
            }
            else
            {
                ToggleSwitch.IsEnabled = false;
                WarningCopyWp7.Visibility = Visibility.Visible;
            }
            ToggleSwitch.Visibility = Visibility.Visible;


            bool data2 = await OneValueRepository.Get(OneValueRepository.LowBandwithMode.Item1,
                                                             OneValueRepository.LowBandwithMode.Item2);
            BandwidthModeSwitch.IsChecked = data2;
            BandwidthModeSwitch.Visibility = Visibility.Visible;

            FlurryHelper.LogPage();
        }

        private async void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            await OneValueRepository.Set(OneValueRepository.AutomaticlyCopyMusicHub.Item1, true);
        }

        private async void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            await OneValueRepository.Set(OneValueRepository.AutomaticlyCopyMusicHub.Item1, false);
        }


        private async void BandwithToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            await OneValueRepository.Set(OneValueRepository.LowBandwithMode.Item1, true);
        }

        private async void BandwithToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            await OneValueRepository.Set(OneValueRepository.LowBandwithMode.Item1, false);
        }

        private void PhoneApplicationPage_Loaded_1(object sender, RoutedEventArgs e)
        {
            Init();

        }
    }
}