using System.Collections.Generic;
namespace ItemOneSearchResult
{

    public class Artist
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public IList<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
        public object trans { get; set; }
    }

    public class Artist2
    {
        public int id { get; set; }
        public string name { get; set; }
        public object picUrl { get; set; }
        public IList<object> alias { get; set; }
        public int albumSize { get; set; }
        public int picId { get; set; }
        public object trans { get; set; }
    }

    public class Album
    {
        public int id { get; set; }
        public string name { get; set; }
        public Artist2 artist { get; set; }
        public object publishTime { get; set; }
        public int size { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public object picId { get; set; }
    }

    public class Song
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<Artist> artists { get; set; }
        public Album album { get; set; }
        public int duration { get; set; }
        public int copyrightId { get; set; }
        public int status { get; set; }
        public IList<string> alias { get; set; }
        public int mvid { get; set; }
    }

    public class Result
    {
        public int songCount { get; set; }
        public IList<Song> songs { get; set; }
    }

    public class SearchResult
    {
        public Result result { get; set; }
        public int code { get; set; }
    }
}