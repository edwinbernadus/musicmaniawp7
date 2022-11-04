using System;
using MSPToolkit.Utilities;

namespace MusicLogicExtendedWp7.Repository
{
    public class WifiParameterRepository
    {
        private string ipAddressFile= "ipAddress.xml";
        private string portNumberFile= "portNumber.xml";


        public  void Save(string ipAddress,string portNumber)
        {
            try
            {
                IsolatedStorageHelper.SaveSerializableObject<string>(ipAddress, this.ipAddressFile);
                IsolatedStorageHelper.SaveSerializableObject<string>(portNumber, this.portNumberFile);
            }
            catch (Exception exception)
            {
            }
        }


        public Tuple<string,string> Get()
        {
            var ipAddress = "192.168.1.1";
            var portNumber = "123";
            try
            {
                ipAddress = IsolatedStorageHelper.LoadSerializableObject<string>(this.ipAddressFile);
            }
            catch (Exception exception)
            {
            }

            try
            {
                portNumber = IsolatedStorageHelper.LoadSerializableObject<string>(this.portNumberFile);
            }
            catch (Exception exception)
            {
            }
            
            return new Tuple<string, string>(ipAddress, portNumber);
        }

    }
}