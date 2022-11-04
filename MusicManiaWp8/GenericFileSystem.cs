using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PCLStorage;
using PCLStorage.Exceptions;
using IFileSystem = MusicLogicExtendedWp7.Helper.IFileSystem;

namespace MusicManiaWp7
{
    public class GenericFileSystem : IFileSystem
    {
        public async Task WriteTextAsync(string folderName, string fileName, string inputFile)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder2 = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            IFile file2 =
                await folder2.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            await file2.WriteAllTextAsync(inputFile);
        }

        public async Task<string> ReadTextAsync(string folderName, string fileName)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder2 = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            IFile file2 =
                await folder2.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            var temp = await file2.ReadAllTextAsync();
            return temp;
        }

        public async Task MakeSureFolderAsync(string folderName)
        {

            IFolder rootFolder = FileSystem.Current.LocalStorage;

            ;
            await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);


        }

        public async Task<List<string>> GetFileNames(string folderName)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder2 = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            var result = await folder2.GetFilesAsync();
            var output = result.Select(x => x.Name).ToList();
            return output;
        }

        public async Task<Dictionary<string, string>> GetFileNameAndPath(string folderName)
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            IFolder folder2 = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);
            var result = await folder2.GetFilesAsync();

            var output = result.ToDictionary(x => x.Name, y => y.Path);
            return output;
        }

        public async Task<bool> IsFileExist(string folderName, string fileName)
        {

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder2 = await rootFolder.CreateFolderAsync(folderName,
                CreationCollisionOption.OpenIfExists);

            var output = false;
            try
            {
                await folder2.GetFileAsync(fileName);
                output = true;
            }
            catch (FileNotFoundException exception)
            {
                Debug.WriteLine("file not found-1");
                output = false;
            }
            catch (Exception exception)
            {
                Debug.WriteLine("file not found-2");
                output = false;
            }

            return output;
        }

        public async Task DeleteFileAsync(string FolderName, string fileName)
        {

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder2 = await rootFolder.CreateFolderAsync(FolderName,
                CreationCollisionOption.OpenIfExists);
            IFile file2 = await folder2.GetFileAsync(fileName);
            await file2.DeleteAsync();
        }
    }
}