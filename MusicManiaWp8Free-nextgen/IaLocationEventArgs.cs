using System;

namespace MusicManiaWp7
{
    public class IaLocationEventArgs : EventArgs
    {
        public IaLocationEventArgs(string location)
        {
            this.location = location;
        }

        public string location;
    }
}