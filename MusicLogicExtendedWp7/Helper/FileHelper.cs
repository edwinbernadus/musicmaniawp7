using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
using MusicLogicExtendedWp7.ViewModels;

namespace MusicLogicExtendedWp7.Helper
{
    public class FileHelper
    {
        public static bool CheckIfRealFileIsExists(string downloadFileNameWithPath)
        {
            IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication();

            return file.FileExists(downloadFileNameWithPath);

        }



        IFileSystem service = ServiceLocator.Get<IFileSystem>();
        //IGetRootFileSystem service = ServiceLocator.Get<IGetRootFileSystem>();
        public async Task PreInit()
        {
            await service.MakeSureFolderAsync(this.FolderName);
        }


        public FileHelper(string folderName)
        {
            this.FolderName = folderName;
        }
        public string FolderName { get; private set; }

        public async Task<List<string>> GetFileNames()
        {
            var output = await service.GetFileNames(FolderName);
            return output;
        }

        public async Task<Dictionary<string, string>> GetFileNameAndPath()
        {
            var output = await service.GetFileNameAndPath(FolderName);
            return output;
        }
        public async Task Save(string fileName, string inputData)
        {
            await service.WriteTextAsync(this.FolderName, fileName, inputData);
        }

        public async Task<string> Load(string fileName)
        {

            var output = await service.ReadTextAsync(this.FolderName, fileName);
            return output;
        }

        public async Task<bool> IsFileExist(string fileName)
        {
            var output = await service.IsFileExist(FolderName, fileName);
            return output;
        }

        public async Task Delete(string fileName)
        {
            await service.DeleteFileAsync(FolderName, fileName);
        }
    }
}