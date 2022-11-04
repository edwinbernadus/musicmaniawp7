using System;
using System.Xml.Linq;

namespace MusicLogicExtendedWp7.Helper
{
    public class AppGuidHelper
    {



        public static Guid GetId()
        {
            Guid applicationId = Guid.Empty;

            try
            {
                var productId = XDocument.Load("WMAppManifest.xml").Root.Element("App").Attribute("ProductID");

                if (productId != null && !String.IsNullOrEmpty(productId.Value))
                    Guid.TryParse(productId.Value, out applicationId);
            }
            catch (Exception exception)
            {
            }


            return applicationId;
        }
    }
}