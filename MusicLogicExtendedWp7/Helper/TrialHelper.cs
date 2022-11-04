namespace MusicLogicExtendedWp7.Helper
{
    public static class TrialHelper
    {
        private static bool? _isTrial = null;

        public static void ResetStatus()
        {
            _isTrial = null;
        }

        public static bool GetStatus()
        {
            //if (isPaidMode)

            if (_isTrial == null)
            {

               
#if DEBUG
                _isTrial = true;
#else
                var license = new Microsoft.Phone.Marketplace.LicenseInformation();
                _isTrial =  license.IsTrial();
#endif

               
            }

            
            return (bool)_isTrial;
        }
    }
}