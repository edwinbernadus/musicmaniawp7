using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Model;
using MusicLogicExtendedWp7.Repository;
using MusicLogicWp7;

namespace MusicLogicExtendedWp7.ViewModels
{
    //public interface ISearchFromTopList
    //{
    //    string GetAllSearchKey();
    //    string GetSongSearchKey();
    //    string GetSingerSearchKey();

    //}

    public class TopListViewModel : INotifyPropertyChanged
        //,ISearchFromTopList
    {

        public TopListViewModel()
        {
            VisibilityFlag = true;
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    NotifyPropertyChanged("ImageUrl");
                }
            }
        }

        private string _imageUrl;


        public TopListViewModel(BillboardGenreResult billboardResult, string specialCode = "")
        {
            VisibilityFlag = true;
            this.Singer = billboardResult.Singer;
            this.Song= billboardResult.Song;
            this.Number= int.Parse(billboardResult.Number);
            this.SpecialCode = specialCode;
            this.ImageUrl = billboardResult.ImgUrl;
        }


        private bool _imageVisibilityFlag;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool ImageVisibilityFlag
        {
            get { return _imageVisibilityFlag; }
            set
            {
                if (value != _imageVisibilityFlag)
                {
                    _imageVisibilityFlag = value;
                    NotifyPropertyChanged("ImageVisibilityFlag");
                }
            }
        }

        //private bool _mainVisibilityFlag;

        ///// <summary>
        ///// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        ///// </summary>
        ///// <returns></returns>
        //public bool MainVisibilityFlag
        //{
        //    get { return _mainVisibilityFlag; }
        //    set
        //    {
        //        if (value != _mainVisibilityFlag)
        //        {
        //            _mainVisibilityFlag = value;
        //            NotifyPropertyChanged("MainVisibilityFlag");
        //        }
        //    }
        //}

        private bool _visibilityFlag;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public bool VisibilityFlag
        {
            get { return _visibilityFlag; }
            set
            {
                if (value != _visibilityFlag)
                {
                    _visibilityFlag = value;
                    NotifyPropertyChanged("VisibilityFlag");
                }
            }
        }



        private string _song;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Song
        {
            get { return _song; }
            set
            {
                if (value != _song)
                {
                    _song = value;
                    NotifyPropertyChanged("Song");
                }
            }
        }

        public string SpecialCode { get; set; }

        public string GetAllSearchKey()
        {
            var t = FilteredSearchResult;
            return t;
        }

        public string GetSongSearchKey()
        {
            var t = Song;
            return t;
        }

        public string GetSingerSearchKey()
        {
            var t = Singer.Replace("Featuring", "");
            return t;
        }

        public string FilteredSearchResult
        {
            get
            {
                var t = this.Song + " " + this.Singer;
                t = t.Replace("Featuring", "");
                t = t.Replace("(", " ");
                t = t.Replace(")", " ");
                return t;
            }
        }

        private string _singer;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string Singer
        {
            get { return _singer; }
            set
            {
                if (value != _singer)
                {
                    _singer = value;
                    NotifyPropertyChanged("Singer");
                }
            }
        }

        private string _songUrl;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public string SongUrl
        {
            get { return _songUrl; }
            set
            {
                if (value != _songUrl)
                {
                    _songUrl = value;
                    NotifyPropertyChanged("SongUrl");
                }
            }
        }

        private int _number;

        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        public int Number
        {
            get { return _number; }
            set
            {
                if (value != _number)
                {
                    _number = value;
                    NotifyPropertyChanged("Number");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}