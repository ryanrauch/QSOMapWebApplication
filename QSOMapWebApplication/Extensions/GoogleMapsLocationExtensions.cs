using QSOMapWebApplication.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QSOMapWebApplication.Extensions
{
    public static class GoogleMapsLocationExtensions
    {
        public static String ToKMLCoordinates(this GoogleMapsLocation location)
        {
            return String.Format("{0},{1}",
                                 location.lng.ToString(),
                                 location.lat.ToString(),
                                 "9");
        }
    }
}
