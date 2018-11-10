using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class Country
    {
        public string id { get; set; }
        public string title { get; set; }
    }

    public class City
    {
        public string id { get; set; }
        public string title { get; set; }
        public string area { get; set; }
        public object region { get; set; }
        public Country country { get; set; }

        public static async Task<string> GetCityByTitleTask(string cityTitle)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            string citiesJson;

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
                        title = cityTitle
                    }
                };
                var graphQlResponse = await graphQlClient.PostAsync(cityRequest);
                var cities = graphQlResponse.GetDataFieldAs<List<City>>("cities");
                citiesJson = JsonConvert.SerializeObject(cities);
            }
            return citiesJson;
        }

        public static async Task<string> GetCityByCoordinatesTask(double lat, double lon)
        {
            var cityTitleOsm = await OsmLocation.GetStreetAddressByCoordinatesTask(lat, lon);

            var cityTitleDonorSearch = await GetCityByTitleTask(cityTitleOsm);

            return cityTitleDonorSearch;
        }
    }
}
