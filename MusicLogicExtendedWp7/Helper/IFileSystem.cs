using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicLogicExtendedWp7.Helper
{
    public interface IFileSystem
    {
        Task WriteTextAsync(string folderName, string fileName, string inputFile);
        Task<string> ReadTextAsync(string folderName, string fileName);
        Task MakeSureFolderAsync(string folderName);
        Task<List<string>> GetFileNames(string folderName);
        Task<Dictionary<string, string>> GetFileNameAndPath(string folderName);
        Task<bool> IsFileExist(string folderName, string fileName);
        Task DeleteFileAsync(string folderName, string fileName);

    }
}