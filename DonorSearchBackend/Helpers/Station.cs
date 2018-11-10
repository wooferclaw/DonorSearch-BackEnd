using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class NeedRequest
    {
        public int id { get; set; }
        public string blood_type_name { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }

        public int need_intensity { get; set; }

        public int one_plus_demand { get; set; }
        public int one_minus_demand { get; set; }

        public int two_plus_demand { get; set; }
        public int two_minus_demand { get; set; }

        public int three_plus_demand { get; set; }
        public int three_minus_demand { get; set; }

        public int four_plus_demand { get; set; }
        public int four_minus_demand { get; set; }

        public bool is_disabled { get; set; }
        public DateTime when_is_it { get; set; }
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

    public class BloodStation
    {
        public bool accept_first_timers { get; set; }
        public string address { get; set; }
        public string blood_type_name { get; set; }
        public bool can_get_pdr { get; set; }
        public City city { get; set; }
        public int created_at { get; set; }
        public string email { get; set; }
        public List<Error> errors { get; set; }
        public string fax { get; set; }
        public string id { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public List<NeedRequest> need_requests { get; set; }
        public bool no_registration { get; set; }
        public string phones { get; set; }
        public string site { get; set; }
        public string title { get; set; }
        public int updated_at { get; set; }
        public bool without_registration { get; set; }
        public bool works_on_monday { get; set; }
        public bool works_on_tuesday { get; set; }
        public bool works_on_wednesday { get; set; }
        public bool works_on_thursday { get; set; }
        public bool works_on_friday { get; set; }
        public bool works_on_saturday { get; set; }
        public bool works_on_sunday { get; set; }

        public int requrement_of_user_blood { get; set; }

        public static async Task<string> GetBloodStationsByVkIdTask(int vkId)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            string stationsJson;

            var currentUserJson = await UserApi.GetUserByVKId(vkId);
            var currentUser = JsonConvert.DeserializeObject<DAL.User>(currentUserJson);

            //TODO getcityidforuserfromdatabase

            var cityId = currentUser.city_id;

            using (var graphQlClient = new GraphQLClient(apiPath + apiKey))
            {
                var stationsRequest = new GraphQLRequest
                {
                    Query = @"query blood_stations($city_id: Int) {
                                                          blood_stations(city_id: $city_id) {
                                                                accept_first_timers
                                                                address
                                                                blood_type_name
                                                                can_get_pdr
                                                                city {
                                                                  id
                                                                  title
                                                            }
                                                                created_at
                                                                email
                                                                errors {
                                                                  key
                                                            }
                                                            fax
                                                            id
                                                            lat
                                                            lng
                                                            need_requests {
                                                              blood_station {
                                                                id
                                                              }
                                                              blood_type_name
                                                              end_time
                                                              four_plus_demand
                                                              four_minus_demand
                                                              id
                                                              intensity
                                                              is_disabled
                                                              one_plus_demand
                                                              one_plus_demand
                                                              start_time
                                                              three_plus_demand
                                                              three_minus_demand
                                                              two_plus_demand
                                                              two_minus_demand
                                                              when_is_it
                                                            }
                                                            no_registration
                                                            phones
                                                            site
                                                            title
                                                            updated_at
                                                            without_registration
                                                            works_on_monday
                                                            works_on_tuesday
                                                            works_on_wednesday
                                                            works_on_thursday
                                                            works_on_friday
                                                            works_on_saturday
                                                            works_on_sunday
                                                          }
                                                        }",
                    OperationName = "blood_stations",
                    Variables = new
                    {
                        id = cityId
                    }
                };

                var graphQlResponse = await graphQlClient.PostAsync(stationsRequest);
                var stations = graphQlResponse.GetDataFieldAs<List<BloodStation>>("blood_stations");

                foreach (var station in stations)
                {
                    //if (currentUser.blood_type.title == station.need_requests.First().blood_type_name)
                    //    station.requrement_of_user_blood = 1;
                    //else station.requrement_of_user_blood = 0;
                    station.requrement_of_user_blood = GetRandomNumber(0,2);
                }

                stationsJson = JsonConvert.SerializeObject(stations);
            }
            return stationsJson;
        }

        private static readonly Random getrandom = new Random();

        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }
    }
}

