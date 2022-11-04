using System.IO;
using System.IO.IsolatedStorage;

namespace SharedLogicNoAsync
{
    public class FileHelper
    {

        public static void TextToFile(string fileName, string inputs)
        {


            //Open existing file
            //          IsolatedStorageFileStream fileStream = myIsolatedStorage.OpenFile(fileName, FileMode.Open, FileAccess.Write);


            IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            if (myIsolatedStorage.FileExists(fileName))
            {
                myIsolatedStorage.DeleteFile(fileName);
            }


            //create new file
            using (StreamWriter writeFile = new StreamWriter(new IsolatedStorageFileStream(fileName,
                                                                                           FileMode.OpenOrCreate, FileAccess.Write, myIsolatedStorage)))
            {
                string someTextData = inputs;
                writeFile.WriteLine(someTextData);
                writeFile.Close();
            }



        }

        public static void StreamToFile(Stream stream, string FileName)
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (myIsolatedStorage.FileExists(FileName))
                {
                    myIsolatedStorage.DeleteFile(FileName);
                }

                using (IsolatedStorageFileStream fileStream = new IsolatedStorageFileStream(FileName, FileMode.Create, myIsolatedStorage))
                {
                    using (BinaryWriter writer = new BinaryWriter(fileStream))
                    {
                        Stream resourceStream = stream;
                        long length = resourceStream.Length;
                        byte[] buffer = new byte[32];
                        int readCount = 0;
                        using (BinaryReader reader = new BinaryReader(stream))
                        {
                            // read file in chunks in order to reduce memory consumption and increase performance
                            while (readCount < length)
                            {
                                int actual = reader.Read(buffer, 0, buffer.Length);
                                readCount += actual;
                                writer.Write(buffer, 0, actual);
                            }
                        }
                    }
                }
            }
        }

        public static string ReadFromFile(string fileName)
        {
            var output = "";
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream =
                    myIsolatedStorage.OpenFile(fileName, FileMode.Open, FileAccess.Read))
                {

                    using (StreamReader reader = new StreamReader(fileStream))
                    {    //Visualize the text data in a TextBlock text
                        output = reader.ReadToEnd();
                    }


                }
            }

            return output;
        }
    }
}