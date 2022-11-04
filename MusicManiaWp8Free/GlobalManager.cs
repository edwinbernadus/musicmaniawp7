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
                var isPaid = InAppPurchaseManager.IsPaid();

#if DEBUG
                //return false;
#endif
                return isPaid;
            }
        }

        public static bool IsShowRingToneLink
        {
            get
            {
                var isPaid = InAppPurchaseManager.IsPaid();

#if DEBUG
                //return false;
#endif
                return !isPaid;
            }
        }

        public static bool ShowAds
        {
            get
            {
                var isPaid= InAppPurchaseManager.IsPaid();

#if DEBUG
                //return false;
#endif
                return !isPaid;
            }

        }

        public static async Task<bool> IsEnableCopyToMusicHub()
        {
            var output = false;
            var isPaid = InAppPurchaseManager.IsPaid();
            output = isPaid;
            if (output == false)
            {
                var isAlreadyUsed = await FreeShareBonusRepository.IsAlreadyUsed();
                output = isAlreadyUsed;
            }
            return output;

        }

        public static bool IsLimitParalelDownload
        {
            get
            {
                var isPaid = InAppPurchaseManager.IsPaid();
                return !isPaid;
            }
        }


        public static bool IsLimitDownload
        {
            get
            {
                return false;
                //var isPaid = InAppPurchaseManager.IsPaid();
                //return !isPaid;
            }
        }



        //public static bool IsFreePointMode { get; set; }
        

        public static bool IsLimitStreaming
        {
            get
            {
                return false;
                //var isPaid = InAppPurchaseManager.IsPaid();
                //return !isPaid;
            }
        }

        public static bool IsShowBuyButton
        {
            get
            {
                var isPaid = InAppPurchaseManager.IsPaid();
                return !isPaid;
            }
        }


        public static bool IsWifiSyncEnable
        {
            get
            {
                var isPaid = InAppPurchaseManager.IsPaid();
                return isPaid;
            }
        }

    




    }
}