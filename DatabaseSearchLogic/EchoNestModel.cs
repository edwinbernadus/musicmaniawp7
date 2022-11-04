using System.Collections.Generic;

namespace DatabaseSearchLogic
{
    public class EchoNestModel
    {
        public Response response { get; set; }



        public class Response
        {
            public Status status { get; set; }
            public List<Song> songs { get; set; }
            public List<Artist> artists { get; set; }
            public class Status
            {
                public string version { get; set; }
                public int code { get; set; }
                public string message { get; set; }
            }
            public class Artist
            {
                public string id { get; set; }
                public string name { get; set; }
            }
            public class Song
            {
                public string artist_id { get; set; }
                public string id { get; set; }
                public string artist_name { get; set; }
                public string title { get; set; }
            }
        }
    }
}