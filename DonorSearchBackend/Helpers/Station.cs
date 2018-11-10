using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class Station
    {
        public class BloodStation
        {
            public object accept_first_timers { get; set; }
            public string address { get; set; }
            public object blood_type_name { get; set; }
            public object can_get_pdr { get; set; }
            public City city { get; set; }
            public int created_at { get; set; }
            public string email { get; set; }
            public List<object> errors { get; set; }
            public string fax { get; set; }
            public string id { get; set; }
            public double lat { get; set; }
            public double lng { get; set; }
            public List<object> need_requests { get; set; }
            public object no_registration { get; set; }
            public string phones { get; set; }
            public string site { get; set; }
            public string title { get; set; }
            public int updated_at { get; set; }
            public bool without_registration { get; set; }
            public object works_on_monday { get; set; }
            public object works_on_tuesday { get; set; }
            public object works_on_wednesday { get; set; }
            public object works_on_thursday { get; set; }
            public object works_on_friday { get; set; }
            public object works_on_saturday { get; set; }
            public object works_on_sunday { get; set; }
        }

        public class Data
        {
            public List<BloodStation> blood_stations { get; set; }
        }

        public class Location
        {
            public int line { get; set; }
            public int column { get; set; }
        }

        public class Error
        {
            public string message { get; set; }
            public List<Location> locations { get; set; }
            public List<object> path { get; set; }
        }

        public class RootObject
        {
            public Data data { get; set; }
            public List<Error> errors { get; set; }
        }


        private static async Task<string> GetStationsByVkIdTask(int vkId)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            string stationsJson;

            var currentUserJson = await UserApi.GetUserByVKId(vkId);
            var currentUser = JsonConvert.DeserializeObject<User>(currentUserJson);

            
            using (var graphQlClient = new GraphQLClient(apiPath + apiKey))
            {
                var stationsRequest = new GraphQLRequest
                {
                    Query = @"query blood_stations($title: String)
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
                       // id = cityTitle
                    }
                };
                var graphQlResponse = await graphQlClient.PostAsync(stationsRequest);
                var stations = graphQlResponse.GetDataFieldAs<List<Station>>("");
                stationsJson = JsonConvert.SerializeObject(stations);
            }

            //stationsJson

            return stationsJson;
        }

    }
}
