using System.Linq;

namespace Model
{
    public class BillboardResult
    {
        public string Number { get; set; }
        public string ImageUrl { get; set; }
        public string Song { get; set; }
        public string Singer { get; set; }

        public string Artist
        {
            get
            {
                return this.Singer;
            }
        }
    }
}