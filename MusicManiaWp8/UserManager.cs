using System;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.Helper;

namespace MusicManiaWp7
{
    public static class UserManager
    {
        
        //0 -> start
        private static int _constUserLevelThisVersion = 0;

        private static int _userLevel = -1;

        public static async Task<int> GetUserLevel()
        {
            if (_userLevel > -1)
            {
                return _userLevel;
            }
            else
            {
                var isAvailable = await GetStatusAvailableOnPersistant();
                if (isAvailable)
                {
                    var result = await GetUserLevelFromPersistant();
                    _userLevel = result;
                }
                else
                {
                    _userLevel = _constUserLevelThisVersion;
                    await SaveOnPersistant();
                }
            }
            return 0;
        }

        private static async Task<bool> GetStatusAvailableOnPersistant()
        {
            var f = new FileHelper(MainParameter.GlobalFolderName);
            var output = await f.IsFileExist(MainParameter.UserFileNameConfig);
            return output;
        }

        private static async Task<int> GetUserLevelFromPersistant()
        {
            var f = new FileHelper(MainParameter.GlobalFolderName);
            var t = await f.Load(MainParameter.UserFileNameConfig);
            var output = _constUserLevelThisVersion;

            try
            {
                output = int.Parse(t);
            }
            catch (Exception exception)
            {

            }

            return output;
        }

        private static async Task SaveOnPersistant()
        {
            var t = _constUserLevelThisVersion;
            var f = new FileHelper(MainParameter.GlobalFolderName);
            await f.Save(MainParameter.UserFileNameConfig, t.ToString());

        }


    }
}