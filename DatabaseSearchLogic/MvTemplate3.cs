using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace DatabaseSearchLogic
{
    public abstract class MvTemplate3 : INotifyPropertyChanged
    {
        private bool _mainProgressBarProgress;
        private bool _mainProgressBarVisibility;
        private bool _refreshTextBlockVisibility;

        protected  bool IsLoaded { get; set; }
        public bool MainProgressBarVisibility
        {
            get { return _mainProgressBarVisibility; }
            set
            {
                _mainProgressBarVisibility = value;
                NotifyPropertyChanged("MainProgressBarVisibility");
            }
        }

        public bool MainProgressBarProgress
        {
            get { return _mainProgressBarProgress; }
            set
            {
                _mainProgressBarProgress = value;
                NotifyPropertyChanged("MainProgressBarProgress");
            }
        }

        public bool RefreshTextBlockVisibility
        {
            get { return _refreshTextBlockVisibility; }
            set
            {
                _refreshTextBlockVisibility = value;
                NotifyPropertyChanged("RefreshTextBlockVisibility");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetMainProgressBar(bool input)
        {
            MainProgressBarProgress = input;
            MainProgressBarVisibility = input;
        }

        public abstract Task Execute();
        public abstract Task Start();


        protected void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //[NotifyPropertyChangedInvocator]
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}