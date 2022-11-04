using System.Collections.Generic;
using Model;

namespace MusicLogicExtendedWp7.Repository
{
    public class AllLTopListDictionaryDetail
    {
        public List<BillboardGenreResult> MainData = new List<BillboardGenreResult>();
        public bool IsUpdatedToMainView { get; set; }
        public string Url { get; set; }
    }
}