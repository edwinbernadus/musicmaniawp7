using System.Text;

namespace Model
{
    public class WebUtility2
    {
        public static string UrlEncodeSpecial(string input_url)
        {
            StringBuilder b = new StringBuilder(100);
            foreach (char c in input_url)
            {
                b.Append(replace(c));
            }
            return b.ToString();
        }

        private static string replace(char c)
        {
            switch (c)
            {
                case '~':
                    return "-";
                    //return "%7E";
                case '!':
                    return "-";
                //return "%21";
                case '@':
                    return "-";
                //return "%40";
                case '#':
                    return "-";
                //return "%23";
                case '$':
                    return "-";
                    //return "%24";
                case '%':
                    return "-";
                    //return "%25";
                case '^':
                    return "-";
                    //return "%5E";
                case '&':
                    return "-";
                    //return "%26";
                case '*':
                    return "-";
                    //return "%2A";
                case '(':
                    return "-";
                    //return "%28";
                case ')':
                    return "-";
                    //return "%29";
                case '{':
                    return "-";
                    //return "%7B";
                case '}':
                    return "-";
                    //return "%7D";
                case '[':
                    return "-";
                    //return "%5B";
                case ']':
                    return "-";
                    //return "%5D";
                case '=':
                    return "-";
                    //return "%3D";
                case ':':
                    return "-";
                //return "%3A";
                case '/':
                    return "-";
                    //return "/";
                case ',':
                    return "-";
                    //return "%2C";
                case ';':
                    return "-";
                //return "%3B";
                case '?':
                    return "-";
                    //return "%3F";
                case '+':
                    return "-";
                //return "%2B";
                case '\'':
                    return "-";
                    //return "%27";
                case '"':
                    return "-";
                    //return "%22";
                case '\\':
                    return "-";
                    //return "%5C";
                default:
                    return c.ToString();
            }

        }
    }
}