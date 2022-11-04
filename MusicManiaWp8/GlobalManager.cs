using System;
using System.Windows;
using MusicLogicExtendedWp7.Helper;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicWp7;


namespace MusicManiaWp7
{
    public static class GlobalManager
    {
        public static bool IsDirectCommitDownload
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return !isTrial;
            }
        }

        public static bool IsShowRingToneLink
        {
            get
            {
                 var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }
        }

        public static bool ShowAds
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }

        }

        public static bool IsEnableCopyToMusicHub
        {
            get
            {
                var output = TrialHelper.GetStatus();
                return !output;
            }

        }

        public static bool IsLimitParalelDownload
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }
        }


        public static bool IsLimitDownload
        {
            get 
            {
                var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }
        }



        //public static bool IsFreePointMode { get; set; }
        

        public static bool IsLimitStreaming
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }
        }

        public static bool IsShowBuyButton
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return isTrial;
            }
        }


        public static bool IsWifiSyncEnable
        {
            get
            {
                var isTrial = TrialHelper.GetStatus();
                return !isTrial;
            }
        }

    




    }
}