using System.Threading.Tasks;
using MusicLogicExtendedWp7.Helper;
using MusicLogicExtendedWp7.Repository;


namespace MusicManiaWp7
{

    public class FreeShareBonusRepository
    {
        static string fileName = "free-share";

        public static async Task<bool> IsAlreadyUsed()
        {
            var folderName = "globalfolder";
            var s = new FileHelper(folderName);
            var t = await s.IsFileExist(fileName);
            return t;
        }

        public async Task SetAsActive()
        {
            var folderName = "globalfolder";
            var s = new FileHelper(folderName);
            await s.Save(fileName,"data");
        }
    }


    public class MainParam : IMainParam
    {
        public async Task<bool> GetDirectCommitDownload()
        {
            var output = false;
            
            var isPaid =  GlobalManager.IsDirectCommitDownload;;


            if (isPaid)
            {
                output = true;
            }
            else{
            var isAlreadyUsed = await FreeShareBonusRepository.IsAlreadyUsed();

                if (isAlreadyUsed)
                {
                    output = true;
                }
            }
            
            return output;
            
        }

        public async Task<bool> GetWp8Status()
        {
            return ParameterRepository.IsThisWp8Version;
        }
    }
}