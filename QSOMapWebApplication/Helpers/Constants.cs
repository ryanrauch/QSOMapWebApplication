using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QSOMapWebApplication.Helpers
{
    public static class Constants
    {
        public const String GoogleMapsApiKey = "GoogleMapsApiKey";
        public const String GoogleMapsApiEndPoint = "https://maps.googleapis.com/maps/api/js?key={0}&callback=initMap";

        public const String QrzApiEndPoint = "QRZApiEndPoint";
        public const String QrzUserName = "QRZUserName";
        public const String QrzPassword = "QRZPassword";
        public const String QrzAgent = "QRZAgent";

        public const String CallSignListTextLocation = "CallSignTextLocation";
        public const String LocationOutputFile = "LocationOutputFile";
    }
}
