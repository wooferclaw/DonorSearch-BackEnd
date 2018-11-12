using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class DSCity
    {
        public string id { get; set; }
        public string title { get; set; }
        public string area { get; set; }
        public DSRegion region { get; set; }
        public DSCountry country { get; set; }

        public static async Task<string> GetCityByTitleTask(string cityTitle)
        {
            var cities = await GetCitiesByPattern(cityTitle);
            var citiesJson = JsonConvert.SerializeObject(cities);
            return citiesJson;
        }

        public static async Task<List<DSCity>> GetCitiesByPattern(string pattern)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            var cities = new List<DSCity>();

            using (var graphQlClient = new GraphQLClient(apiPath + apiKey))
            {
                var cityRequest = new GraphQLRequest
                {
                    Query = @"query cities($title: String)
                                {
                                    cities(title: $title)
                                    { 
                                        id title area region
                                        {
                                            id title
                                        }
                                        country
                                        {
                                            id title
                                        }
                                    }
                                }",
                    OperationName = "cities",
                    Variables = new
                    {
                        title = pattern
                    }
                };
                var graphQlResponse = await graphQlClient.PostAsync(cityRequest);
                cities = graphQlResponse.GetDataFieldAs<List<DSCity>>("cities");
            }
            return cities;
        }
        public static async Task<string> GetCityByCoordinatesTask(double lat, double lon)
        {
            var cityTitleOsm = await OsmLocation.GetStreetAddressByCoordinatesTask(lat, lon);
            var cityTitleDonorSearch = await GetCityByTitleTask(cityTitleOsm);
            return cityTitleDonorSearch;
        }
    }
}
