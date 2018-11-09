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
    public class City
    {
        private static async Task<string> GetCityByTitleTask(string cityTitle)
        {

           var accessKey = ConfigurationManager<>

            var graphQlClient = new GraphQLClient("https://developer.donorsearch.org/graph_ql_test/main_test?access_key=" + accessKey);

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
            var citiesJson = JsonConvert.SerializeObject(cities);

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
