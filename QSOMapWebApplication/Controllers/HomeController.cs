using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QSOMapWebApplication.Models;
using QSOMapWebApplication.Helpers;
using System.Xml.Serialization;
using QSOMapWebApplication.Extensions;
using QSOMapWebApplication.Data.Contracts;
using System.IO;
using Newtonsoft.Json;
using QSOMapWebApplication.Services;

namespace QSOMapWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Map()
        {
            ViewData["Message"] = "Map page.";
            var googlescript = String.Format(Constants.GoogleMapsApiEndPoint,
                                             _configuration[Constants.GoogleMapsApiKey]);
            ViewBag.GoogleMapsApi = String.Format(Constants.GoogleMapsApiEndPoint,
                                                  _configuration[Constants.GoogleMapsApiKey]);
            GoogleMapsLocation home = new GoogleMapsLocation() { lat = 30.2672, lng = -97.7431 };
            List<GoogleMapsLocation> coordinates;
            using (var reader = new StreamReader(_configuration[Constants.LocationOutputFile]))
            {
                var json = await reader.ReadToEndAsync();
                ViewBag.Coordinates = json;
                coordinates = JsonConvert.DeserializeObject<List<GoogleMapsLocation>>(json);
            }
            CoordinatesToKMLService svc = new CoordinatesToKMLService("C:\\Users\\ryanr\\Documents\\qso-map\\kml\\");
            await svc.CreateKMLfromDestinations(home, coordinates);

            return View();
            //QRZ api
            using (HttpClient client = new HttpClient())
            {
                String session = String.Empty;
                UriBuilder builder = new UriBuilder(_configuration[Constants.QrzApiEndPoint]);
                builder.Query = String.Format("username={0};password={1};agent={2}",
                                              _configuration[Constants.QrzUserName],
                                              _configuration[Constants.QrzPassword],
                                              _configuration[Constants.QrzAgent]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/xml"));
                HttpResponseMessage response = await client.GetAsync(builder.Uri);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var qrzdb = data.XmlDeserializeFromString<QRZDatabase>();
                    session = qrzdb.Session.Key;

                    List<GoogleMapsLocation> locations = new List<GoogleMapsLocation>();
                    using (var reader = new StreamReader(_configuration[Constants.CallSignListTextLocation]))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            builder = new UriBuilder(_configuration[Constants.QrzApiEndPoint]);
                            builder.Query = String.Format("s={0};callsign={1}",
                                                          session,
                                                          line.Trim());
                            response = await client.GetAsync(builder.Uri);
                            if (response.IsSuccessStatusCode)
                            {
                                var csData = await response.Content.ReadAsStringAsync();
                                qrzdb = csData.XmlDeserializeFromString<QRZDatabase>();
                                var qrzcs = qrzdb.Callsign;
                                if (qrzcs != null)
                                {
                                    double lat = qrzcs.lat;
                                    double lon = qrzcs.lon;
                                    locations.Add(new GoogleMapsLocation()
                                                    {
                                                        lat = lat,
                                                        lng = lon
                                                    });
                                }
                            }
                        }
                        string json = JsonConvert.SerializeObject(locations);
                        await System.IO.File.WriteAllTextAsync(_configuration[Constants.LocationOutputFile], json);
                    }
                }


            }
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
