using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorSearchBackend.DAL;
using DonorSearchBackend.DAL.Repositories;
using GraphQL.Client;
using GraphQL.Common.Request;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers.DSApi
{
    public class DSBloodStation
    {
        public bool? accept_first_timers { get; set; }
        public string address { get; set; }
        public string blood_type_name { get; set; }
        public bool? can_get_pdr { get; set; }
        public DSCity city { get; set; }
        public int created_at { get; set; }
        public string email { get; set; }
        public string fax { get; set; }
        public string id { get; set; }
        public double? lat { get; set; }
        public double? lng { get; set; }
        public List<DSNeedRequest> need_requests { get; set; }
        public bool? no_registration { get; set; }
        public string phones { get; set; }
        public string site { get; set; }
        public string title { get; set; }
        public int updated_at { get; set; }
        public bool? without_registration { get; set; }
        public bool? works_on_monday { get; set; }
        public bool? works_on_tuesday { get; set; }
        public bool? works_on_wednesday { get; set; }
        public bool? works_on_thursday { get; set; }
        public bool? works_on_friday { get; set; }
        public bool? works_on_saturday { get; set; }
        public bool? works_on_sunday { get; set; }

        public int requrement_of_user_blood { get; set; }

        public static async Task<string> GetBloodStationsByVkIdTask(int vkId)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            string stationsJson;

            //var currentUserJson = await DSUser.GetUserByVKId(vkId);
            //var currentUser = JsonConvert.DeserializeObject<DAL.User>(currentUserJson);
            var currentUser = UserRepository.GetUserByVkId(vkId);

            var cityId = currentUser.city_id;

            if (!cityId.HasValue || cityId == 0)
            {
                return "[]";
            }

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
                          blood_type {
                            created_at
                            display_title
                            id
                            title
                            updated_at
                          }
                          end_time
                          id
                          intensity
                          is_disabled
                          start_time
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
                        city_id = cityId   //TODO  query
                    }
                };

                var graphQlResponse = await graphQlClient.PostAsync(stationsRequest);
                var stations = graphQlResponse.GetDataFieldAs<List<DSBloodStation>>("blood_stations");

                foreach (var station in stations)
                {
                    //station.requrement_of_user_blood = GetRandomNumber(0,2);

                    //check if station accepts first timers

                    station.accept_first_timers = DonationRepository.GetDonationByVkId(currentUser.vk_id)
                        .All(c => cityId != currentUser.city_id);

                    var necessity = currentUser.CheckForBloodNeccesity(station.need_requests);
                    //chech for requirement of blood for current user
                    if (necessity >= 50) station.requrement_of_user_blood = -2;
                    if (necessity < 50) station.requrement_of_user_blood = -1;
                    if (necessity == 0) station.requrement_of_user_blood = 0;

                    //check if station accepts users without registration
                    if (currentUser.has_registration.HasValue && !currentUser.has_registration.Value &&
                        station.without_registration.Value)
                        station.requrement_of_user_blood = 0;
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
