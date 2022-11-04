using System;
using System.Threading.Tasks;
using System.Windows;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;
using MusicLogicWp7;


namespace MusicManiaWp7
{
    public static class GlobalManager
    {
        public static bool IsDirectCommitDownload
        {
            get { return false; }
        }

        public static bool IsShowRingToneLink
        {
            get
            {
                
                return true;
            }
        }

        public static bool ShowAds
        {
            get
            {
#if DEBUG
             //  return true;
#endif
                return true;
            }

        }

        public static bool IsEnableCopyToMusicHub
        {
#if DEBUG
            get { return true; }
#else
                   get { return false; }
#endif

        }

        public static bool IsLimitParalelDownload
        {
            
            
            get { return true; }
            
        }


        public static bool IsLimitDownload
        {
            get { return false; }
        }



        //public static bool IsFreePointMode { get; set; }
        

        public static bool IsLimitStreaming
        {
            get { return false; }
        }

        public static bool IsShowBuyButton
        {
            get { return true; }
        }


        public static bool IsWifiSyncEnable
        {
            get
            {
#if DEBUG
              //  return true;
#endif
                return false;
            }
        }

    




    }
}