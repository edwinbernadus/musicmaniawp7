using System;
using System.Linq;
using System.Threading.Tasks;
using MSPToolkit.Utilities;

namespace MusicLogicExtendedWp7.Repository
{
//    public static class FreePointModeValidationRepository
//    {

//        public static readonly string FreePointModeValidationFileName = "FreePointModeValidation.xml";
//        private static bool? _isFreePointMode = null;

//        public static async Task<bool> Inquiry()
//        {
//#if DEBUG
//            //return true;
//#endif
//            if (_isFreePointMode == null)
//            {

//                bool hasError = false;
//                try
//                {
//                    var t  = IsolatedStorageHelper.LoadSerializableObject<bool>(FreePointModeValidationFileName);
//                    _isFreePointMode = t;
//                }
//                catch (System.IO.FileNotFoundException)
//                {
//                    //isCalled = false;
//                    hasError = true;
//                }

//                if (hasError)
//                {
//                    if (MainRepository.SavedViewModelRepository.SavedSongsViewModel.Any())
//                    {
//                        await SetOneTimeCall(FreePointModeValidationFileName, true);
//                        _isFreePointMode = true;
//                    }
//                    else
//                    {
//                        await  SetOneTimeCall(FreePointModeValidationFileName, false);
//                        _isFreePointMode = false;
//                    }

//                }
                
//            }

//            if (_isFreePointMode == null)
//            {
//                return false;
//            }
//            else
//            {
//                return (bool)_isFreePointMode;
//            }
            

//            var isHasBeenCheckedBefore = false;
//            //if file null then never check before
//            //Check
//            //GetResult
//            return false;
//        }



//        public static async Task SetOneTimeCall(string fileName, bool input)
//        {
//            try
//            {
//                IsolatedStorageHelper.SaveSerializableObject<bool>(input, fileName);
//            }
//            catch (Exception exception)
//            {

//            }

//        }
//    }
}