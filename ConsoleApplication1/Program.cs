using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicLogicWp7;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Caller();
            ;
            Console.ReadLine();
        }

        public static async Task Caller()
        {
            Program p = new Program();
            await p.TestSc();
            
        }
        public async Task TestSc()
        {
            var c = new SoundCloudWebSongSearchResultCommand();
            var d = await c.Execute("big");
        }
    }
}
