using System;
using System.Linq;
using System.Threading.Tasks;
using MSPToolkit.Utilities;

namespace MusicLogicExtendedWp7.Repository
{
    //business model case
    //Free Point
    //App2
    //Pro Version


    public static class AppVersionValidationRepository
    {

        public static readonly string AppVer2FileName = "AppVer2.xml";
        private static bool? _isAppVer2 = null;

    

        public static async Task SetOneTimeCall(string fileName, bool input)
        {
            try
            {
                IsolatedStorageHelper.SaveSerializableObject<bool>(input, fileName);
            }
            catch (Exception exception)
            {

            }

        }
    }
}