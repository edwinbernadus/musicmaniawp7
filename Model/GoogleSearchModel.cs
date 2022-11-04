using System.Collections.Generic;

namespace Model
{
    public class GoogleSearchModel
    {
        public responseData responseData { get; set; }
    }

    public class results
    {
        public string titleNoFormatting { get; set; }
        public string url { get; set; }
    }
    public class responseData
    {
        public List<results> results { get; set; }
    }
}