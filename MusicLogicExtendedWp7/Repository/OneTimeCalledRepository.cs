using System;
using System.Threading.Tasks;
using MSPToolkit.Utilities;

namespace MusicLogicExtendedWp7.Repository
{
    public static  class OneTimeCalledRepository
    {
        

        public static readonly string RatingFileName = "ratingCalled.xml";
        public static readonly string SaveMusicFileName = "saveMusic.xml";
        public static async Task SetOneTimeCall(string fileName,bool input=true)
        {
            try
            {
                IsolatedStorageHelper.SaveSerializableObject<bool>(input, fileName);
            }
            catch (Exception exception)
            {

            }
            
        }

        public static async Task<bool> GetOneTimeCall(string fileName)
        {
            bool isCalled = false;
            bool hasError = false;
            try
            {
                isCalled = IsolatedStorageHelper.LoadSerializableObject<bool>(fileName);
            }
            catch (System.IO.FileNotFoundException)
            {
                isCalled = false;
                hasError = true;
            }

            if (hasError)
            {
                await SetOneTimeCall(fileName, false);
            }
            return isCalled;

        }
    }
}