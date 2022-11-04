using System.Linq;

namespace Model
{
    public class BillboardResultOld
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    
        public string FormatDescription
        {
            get
            {
                //Radioactive by Imagine Dragons ranks #4
                var output = Description.Replace("by", "|").Replace("ranks #", "|");
                return output;
            }
        }
    
        public string Song
        {
            get
            {
                var output = "";
                var t = FormatDescription.Split('|');
                output = t[0].Trim();
                return output;
            }
        }
    
        public string Artist
        {
            get
            {
                var output = "";
                var t = FormatDescription.Split('|');
                output = t[1].Trim();
                return output;
            }
        }
    
        public string Number
        {
            get
            {
                var output = "";
                var t = FormatDescription.Split('|');
                output = t[2];
                return output;
            }
        }
    
    
        public string OldSong
        {
            get {
    
                var output = "";
                var temp = Title.Split(',').ToList();
    
                if (temp.Count > 0)
                {
                    var z = temp.First();
                    
                    output = z.Split(':')[1].Trim();
                }
                return output;
    
            }
        }
    
    
        public string OldNumber
        {
            get
            {
    
                var output = "";
                var temp = Title.Split(',').ToList();
    
                if (temp.Count > 0)
                {
                    var z = temp.First();
    
                    output = z.Split(':')[0];
                }
                return output;
    
            }
        }
    
        public string OldArtist
        {
            get
            {
    
                var output = "";
                var temp = Title.Split(',').ToList();
    
                if (temp.Count > 1)
                {
                    temp.RemoveAt(0);
                }
    
                output = string.Join(",", temp);
                return output;
            }
        }
    }
}
