using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Model;
using SharedLogic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicLogicExtendedWp7.Repository
{
    public class PlayListDetailRepository
    {



        //public ObservableCollection<PlayList> PlayLists = new ObservableCollection<PlayList>();
        //private bool _isInit = false;



        public async Task CleanUpDeletedStreamingSong(string downloadUrl)
        {
            var repo = new PlayListRepository();
            var list = await repo.Load();
            var list2 = list.Where(x => x.IsStreamingType == true).ToList();

            foreach (var playList in list2)
            {
                var playListItems = await Load(playList.Name);

                 var newResult =
                    playListItems.Where(x => x.PlayListUrl != downloadUrl).ToList();

                if (playListItems.Count != newResult.Count())
                {
                    await Update(newResult, playList.Name);
                }
            }
        }

        public async Task CleanUpDeletedPersistantSong(string downloadFileNameCalculation)
        {
            var repo = new PlayListRepository();
            var list = await repo.Load();
            var list2 = list.Where(x => x.IsStreamingType == false).ToList();

            foreach (var playList in list2)
            {
                var playListItems  = await Load(playList.Name);

                var newResult = 
                    playListItems.Where(x => x.DownloadFileNameCalculation != downloadFileNameCalculation).ToList();

                if (playListItems.Count != newResult.Count())
                {
                    await Update(newResult, playList.Name);
                }
            }
        }

        [Obsolete]
        public void CleanUpDeletedSongsVer1
            (List<PlayListDetail>  playListSongs, List<PlayListDetail> allSongs  )
        {

            //var result = playListSongs.Except(allSongs).ToList();
            //foreach (var playListDetail in result)
            //{
            //    playListSongs.Remove(playListDetail);
            //}
            
            
            List<PlayListDetail> RemoveItems = new List<PlayListDetail>();

            foreach (var playListDetail in playListSongs)
            {
                var isFound = false;
                foreach (var listDetail in allSongs)
                {
                    //DownloadFileNameCalculation
                    if (playListDetail.DownloadFileNameCalculation == 
                        listDetail.DownloadFileNameCalculation)
                    {
                        isFound = true;
                    }
                }

                if (!isFound)
                {
                    RemoveItems.Add(playListDetail);
                }
            }

            foreach (var playListDetail in RemoveItems)
            {
                playListSongs.Remove(playListDetail);
            }
             
        }

        public async Task<List<PlayListDetail>> Load(string fileName)
        {
            Debug.WriteLine("TRY load playlist detail");

            Debug.WriteLine("load playlist detail");
            var finalName = fileName+ ".list";
            var item = await FileHelperAsync.ReadFromFile(finalName);
            //var item = await FileHelperAsync.ReadFromFile(fileName);
            var t = JsonConvert.DeserializeObject<List<PlayListDetail>>(item);
            t = t ?? new List<PlayListDetail>();
            return t;
        }





        private int saveCounter = 0;
        //private string fileName = "playList.json";


       

        public async Task Update(List<PlayListDetail> input2,string fileName)
        {
            try
            {
                saveCounter++;

                if (saveCounter == 1)
                {
                    while (saveCounter > 0)
                    {
                        //var z = await Load();
                        //z.Add(input2);
                        var finalName = fileName + ".list";
                        
                        var t = JsonConvert.SerializeObject(input2);
                        await FileHelperAsync.TextToFile(finalName, t);
                        saveCounter--;
                    }

                }
            }
            catch (Exception exception)
            {


            }

        }

    }
}
