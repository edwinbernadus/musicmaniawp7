using System;
using System.Threading.Tasks;
using MSPToolkit.Utilities;

namespace MusicLogicExtendedWp7.Repository
{
    public static class OneValueRepository
    {
        public static readonly Tuple<string,bool> AutomaticlyCopyMusicHub = new Tuple<string, bool>
            ("AutomaticlyCopyMusicHub.xml",true);

        public static readonly Tuple<string, bool> LowBandwithMode = new Tuple<string, bool>
            ("LowBandwithMode.xml", false);

        public static async Task Set(string fileName, bool input)
        {
            try
            {
                IsolatedStorageHelper.SaveSerializableObject<bool>(input, fileName);
            }
            catch (Exception exception)
            {

            }

        }

        public static async Task<bool> Get(string fileName,bool valueIfNull=false)
        {
            bool output = false;
            bool hasError = false;
            try
            {
                output = IsolatedStorageHelper.LoadSerializableObject<bool>(fileName);
            }
            catch (System.IO.FileNotFoundException)
            {
                output = valueIfNull;
                hasError = true;
            }

            if (hasError)
            {
                await Set(fileName, valueIfNull);
            }
            return output;

        }
    }
}