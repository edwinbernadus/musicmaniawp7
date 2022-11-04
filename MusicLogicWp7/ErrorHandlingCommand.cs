using System;
using System.Threading.Tasks;

namespace MusicLogicWp7
{
    public static class ErrorHandlingCommand
    {
        public static string ErrorTranslateMessage()
        {
            return "Failed to translate";
        }
        public static async Task ErrorTranslate(string url)
        {
            //var z = new ArgumentException(ErrorTranslateMessage());
            //throw new ArgumentException(ErrorTranslateMessage());
        }
    }
}