using System.Collections.Generic;

namespace Model
{


    public class FreeBonus
    {
        public string Period { get; set; }
        public int FreeBonusAlreadyUsedForDownload { get; set; }
        public int FreeBonusNotUse { get; set; }

        public bool IsFreeAvailable
        {
            get
            {
                var t = FreeBonusNotUse - FreeBonusAlreadyUsedForDownload;
                if (t > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}