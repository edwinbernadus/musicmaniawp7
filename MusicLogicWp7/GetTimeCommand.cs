using System;
using System.Threading.Tasks;
using WebLogic;

namespace MusicLogicWp7
{
    public class GetTimeCommand
    {
        public DateTime ResultDateTime { get; set; }
        public async Task<string> Execute()
        {
            var httpClient = new HttpClientExtended();
            var uri = "http://www.timeapi.org/utc/now";
            var output = await httpClient.GetStringAsync(new Uri(uri));
            
            var z = DateTime.Parse(output);
#if DEBUG
            z = z.AddDays(1);
#endif
            ResultDateTime = z;
            //var y = z.ToUniversalTime().ToShortDateString();
            var y = z.ToUniversalTime().ToString("yyyyMMdd");
            return y;
        }
    }
}