using System;

namespace MusicLogicWp7
{
    public class WebTopList
    {
        public string RawSongName { get; set; }
        public string Url { get; set; }

        public string Song
        {
            get
            {

                var output = "";
                try
                {
                    var t1 = RawSongName.Replace("&mdash;", "|");
                    var t2 = t1.Split('|')[1];
                    output = t2.Trim();
                    output = StringWebHelper.ParseClean(output);
                }
                catch (Exception exception)
                {
                }

                return output;
            }
        }

        public string Singer
        {
            get
            {

                var output = "";
                try
                {
                    var t1 = RawSongName.Replace("&mdash;", "|");
                    var t2 = t1.Split('|')[0];
                    output = t2.Trim();
                }
                catch (Exception exception)
                {
                }

                return output;
            }
        }

        public string FullUrl
        {
            get
            {
                var url = "http://m.beemp3.com/";
                var url1 = Url ?? "";
                return url + url1;
            }

        }
    }
}