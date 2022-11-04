using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MusicLogicExtendedWp7.ViewModels;
using Newtonsoft.Json;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    

    public class PlayListRepository
    {



        //public ObservableCollection<PlayList> PlayLists = new ObservableCollection<PlayList>();
        //private bool _isInit = false;

        public async Task<List<PlayList>> Load()
        {
            Debug.WriteLine("TRY load play list");

            Debug.WriteLine("load play list");
            var t = await PlayListManager.LoadAsync();
            t = t ?? new List<PlayList>();
            return t;
        }





        private int saveCounter = 0;
        private string fileName = "playList.json";


        
        public async Task Delete(string playlistName)
        {
           // try
            {
                saveCounter++;

                if (saveCounter == 1)
                {
                    while (saveCounter > 0)
                    {
                        var z = await Load();
                        var input2 = z.First(x => x.Name == playlistName);
                        z.Remove(input2);
                        var t = JsonConvert.SerializeObject(z);
                        await FileHelperAsync.TextToFile(fileName, t);
                        saveCounter--;
                    }

                }
            }
          //  catch (Exception exception)
            {


            }

        }


        public async Task Rename(PlayList oldItem,PlayList newItem)
        {
            // try
            {

                saveCounter++;

                if (saveCounter == 1)
                {
                    while (saveCounter > 0)
                    {
                        var z = await Load();
                        if (z.Any(x => x.Name == newItem.Name))
                        {
                            throw new ArgumentException("Playlist name already exist");
                        }

                        z.Add(newItem);
                        var remoteItem = z.FirstOrDefault(x => x.Name == oldItem.Name);
                        if (remoteItem != null)
                        {
                            z.Remove(remoteItem);
                        }
                        var t = JsonConvert.SerializeObject(z);
                        await FileHelperAsync.TextToFile(fileName, t);
                        saveCounter--;
                    }

                }
            }
            //  catch (Exception exception)
            {


            }

        }

        public async Task Add(PlayList input2)
        {
           // try
            {

                saveCounter++;

                if (saveCounter == 1)
                {
                    while (saveCounter > 0)
                    {
                        var z = await Load();
                        if (z.Any(x => x.Name == input2.Name))
                        {
                            throw new ArgumentException("Playlist name already exist");
                        }

                        z.Add(input2);
                        var t = JsonConvert.SerializeObject(z);
                        await FileHelperAsync.TextToFile(fileName, t);
                        saveCounter--;
                    }

                }
            }
          //  catch (Exception exception)
            {


            }

        }

    }

}
