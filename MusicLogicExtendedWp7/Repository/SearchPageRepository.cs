using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using MSPToolkit.Utilities;
using Model;
using MusicLogicWp7;
using SharedLogic;

namespace MusicLogicExtendedWp7.Repository
{
    public class SearchPageRepository
    {
        
        private string hybridFileName = "hybridSearchResult.xml";
        private string soundcloudFileName = "soundSearchResult.xml";
        

        
        public List<WebSongSearchResult> ExternalSearchResult = new List<WebSongSearchResult>();
        public List<WebSongSearchResult> SoundCloudSearchResult = new List<WebSongSearchResult>();
        

        private void Save()
        {
            try
            {
                IsolatedStorageHelper.SaveSerializableObject<List<WebSongSearchResult>>
                   (SoundCloudSearchResult, soundcloudFileName);
                IsolatedStorageHelper.SaveSerializableObject<List<WebSongSearchResult>>
                    (ExternalSearchResult, hybridFileName);
                

            }
            catch (Exception exception)
            {
            }
        }

        public void Update(List<WebSongSearchResult> inputExternal, 
            List<WebSongSearchResult> inputSoundCloud)
        {
            this.ExternalSearchResult= inputExternal;
            this.SoundCloudSearchResult= inputSoundCloud;
            Save();
        }

     
    }
}