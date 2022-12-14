using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BugSense;
using MSPToolkit.Utilities;
using Microsoft.Phone.BackgroundAudio;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Model;
using MusicLogicExtendedWp7;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.Repository;
using MusicLogicExtendedWp7.ViewModels;


namespace MusicManiaWp7
{
    public partial class App : Application
    {
        
        Microsoft.Xna.Framework.Media.MediaLibrary lib = null;
        Microsoft.Devices.MediaHistory history = null;

        


        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The MainViewModel object.</returns>
        //public static MainViewModel ViewModel
        //{
        //    get
        //    {
        //        // Delay creation of the view model until necessary
        //        if (viewModel == null)
        //            viewModel = new MainViewModel();

        //        return viewModel;
        //    }
        //}

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        
        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            BugSenseHandler.Instance.Init(this, ParameterRepository.bugsenseId);
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;
#if DEBUG
            InAppPurchaseManager.SetupMockIAP();
#endif
            
            ServiceLocatorHelper.Init();

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                //Application.Current.Host.Settings.EnableFrameRateCounter = true;

                //MetroGridHelper.IsVisible = true;
                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                //PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }


            
        }

        
        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            try
            {
                FlurryWP8SDK.Api.StartSession(ParameterRepository.flurryId);
            }
            catch (Exception exception)
            {
                
            }
            
            
            //IsolatedStorageExplorer.Explorer.Start("localhost");
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched

        

        private async  void Application_Activated(object sender, ActivatedEventArgs e)
        {

            try
            {
                FlurryWP8SDK.Api.StartSession(ParameterRepository.flurryId);
            }
            catch (Exception exception)
            {
                
            }
            
            
            if (e.IsApplicationInstancePreserved == false)
            {
                

                await ResumeLogic();
            }


        }

        public static async Task ResumeLogic()
        {
            //MainRepository.CopyMusicHubAction = MusicHubHelper.ExportToHub;

            MainRepository.IsContinueTombstone = true;
            //MainRepository.IsThisWp8Version = ParameterRepository.IsThisWp8Version;
            
            await MainRepository.PersistantSongRepository.Init();
            await MainRepository.StreamingInfoViewModelRepository.Init();
            await MainRepository.TombstoneRepository.LoadTombstone();
            await MainRepository.DownloadProcessViewModelRepository.Init();
        }

        public static async Task FirstLoadLogic()
        {
            //MainRepository.CopyMusicHubAction = MusicHubHelper.ExportToHub;
            //MainRepository.IsThisWp8Version = ParameterRepository.IsThisWp8Version;
            
            
            var isEnableToCopy = await GlobalManager.IsEnableCopyToMusicHub();
            MainRepository.InitializeSetEnableToCopyMode(isEnableToCopy);
            await MainRepository.PopulateData();
            
        }

       

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            //Fascade.BusinessLogicFascade.CleanUpMemory();
            
            //IsolatedStorageHelper.SaveSerializableObject<InRamStorageRepository>(MainRepository.MemoryStorageRepository, ParameterRepository.fileNameMemoryStorage);
            try
            {
                MainRepository.TombstoneRepository.SaveTombstone();
            }
            catch (Exception exception)
            {
                
            }
            
            // Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            MainRepository.DownloadProcessViewModelRepository.CleanUpMemory();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }





        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;

            ThemeManager.ToDarkTheme();
            //ThemeManager.SetAccentColor(AccentColor.Gray);

            var newColor = ColorRepositiory.GetOrange();
            ThemeManager.SetAccentColor(newColor);
            
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

      
    }
}