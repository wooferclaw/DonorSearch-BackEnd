using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DonorSearchBackend.DAL;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class Address
    {
        private string Road { get; set; }
        private string Village { get; set; }
        private string State_District { get; set; }
        public string State { get; set; }
        private string Postcode { get; set; }
        private string Country { get; set; }
        private string Country_Code { get; set; }
    }

    public class OsmLocation
    {
            public string Place_Id { get; set; }
            public string Licence { get; set; }
            public string Osm_Type { get; set; }
            public string Osm_Id { get; set; }
            private double Lat { get; set; }
            private double Lon { get; set; }
            private string Place_Rank { get; set; }
            private string Category { get; set; }
            private string Type { get; set; }
            private string Importance { get; set; }
            private string Addresstype { get; set; }
            private string Name { get; set; }
            private string Display_Name { get; set; }
            public Address Address { get; set; }
            private List<string> Boundingbox { get; set; }

            public static async Task<string> GetStreetAddressByCoordinatesTask(double latitude, double longitude)
            {
                var httpClient = new HttpClient { BaseAddress = new Uri("https://nominatim.openstreetmap.org") };
                httpClient.DefaultRequestHeaders.Add("User-Agent", "DonorSearchBackend");

                var httpResult = await httpClient.GetAsync(
                    string.Format("reverse?format=jsonv2&lat={0}&lon={1}", latitude.ToString(new CultureInfo("en-US")), longitude.ToString(new CultureInfo("en-US"))));

                var osmJson = JsonConvert.DeserializeObject<OsmLocation>(await httpResult.Content.ReadAsStringAsync());

                return string.Format("{0}", osmJson.Address.State);  
            }

            public static async Task<string> GetCoordinatesByCityTitleTask(User currentUser)
            {
                if (string.IsNullOrEmpty(currentUser.city_title)) return "[]";
                var httpClient = new HttpClient { BaseAddress = new Uri("https://nominatim.openstreetmap.org") };
                httpClient.DefaultRequestHeaders.Add("User-Agent", "DonorSearchBackend");

                var httpResult = await httpClient.GetAsync(
                    string.Format("search?q={0}&format=json", currentUser.city_title));

                var osmJson = JsonConvert.DeserializeObject<OsmLocation>(await httpResult.Content.ReadAsStringAsync());

                return string.Format("[{0},{1}]", osmJson.Lat, osmJson.Lon); 
            }
    }
}
