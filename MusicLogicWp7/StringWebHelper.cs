using System.Text;
using System.Net;

namespace MusicLogicWp7
{
    public class StringWebHelper
    {
        public static string ParseClean(string input)
        {
            var output = input.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            output = output.Trim();
            output = WebUtility.HtmlDecode(output);
            return output;
        }
    }

   
}