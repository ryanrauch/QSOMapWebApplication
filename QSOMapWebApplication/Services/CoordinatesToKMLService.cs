using QSOMapWebApplication.Data.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using QSOMapWebApplication.Extensions;

namespace QSOMapWebApplication.Services
{
    public class CoordinatesToKMLService
    {
        private readonly string _output;

        public CoordinatesToKMLService(String output)
        {
            _output = output;
        }

        public async Task CreateKMLfromDestinations(GoogleMapsLocation source, IList<GoogleMapsLocation> destinations)
        {
            String output = _output + DateTime.Now.Ticks.ToString() + ".kml";
            int name = 0;
            using (StreamWriter sw = File.AppendText(output))
            {
                await sw.WriteLineAsync("<?xml version=\"1.0\" encoding=\"UTF-8\"?><kml xmlns=\"http://www.opengis.net/kml/2.2\"><Document><name>Shapes</name><Style id=\"thickLine\"><LineStyle><width>1.0</width><color>190000FF</color></LineStyle></Style>");
                foreach (var dest in destinations)
                {
                    string placemark = String.Format("<Placemark><name>{2}</name><description>Shape</description><LineString><tessellate>1</tessellate><altitudeMode>clampToGround</altitudeMode><coordinates>{0} {1} </coordinates></LineString><styleUrl>#thickLine</styleUrl></Placemark>",
                        source.ToKMLCoordinates(),
                        dest.ToKMLCoordinates(),
                        (++name).ToString());
                    await sw.WriteLineAsync(placemark);
                }
                await sw.WriteLineAsync("</Document></kml>");
            }
        }
    }
}
