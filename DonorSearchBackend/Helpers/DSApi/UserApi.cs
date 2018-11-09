using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public class BloodClass
    {
        public string id { get; set; }
        public string title { get; set; }
    }
    public class BloodType
    {
        public string id { get; set; }
        public string title { get; set; }
        public string display_title { get; set; }
    }
    public class DonationStatistic
    { public int? count { get; set; }
        public BloodClass blood_class { get; set; }
    }
    public class User
    {
        public string id { get; set; }
        public string avatar { get; set; }
        public int? bdate { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string second_name { get; set; }
        public int? gender { get; set; }
        public List<BloodClass> blood_classes { get; set; }
        public BloodType blood_type { get; set; }
        public City city { get; set; }
        public List<DonationStatistic> donationStat { get; set; }
    }
    public static class UserApi
    { 
        //TODO: errors
        public static async Task<string> GetUserByVKId(int vkId)
        {
            var apiKey = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiKey"];
            var apiPath = ConfigurationManager.AppSetting["AppSettings:DonorSearchApiPath"];
            string userJson;

            using (var graphQlClient = new GraphQLClient(apiPath + apiKey))
            {
                var userGet = new GraphQLRequest
                {
                    Query = @"query user($id: Int!) 
                            { user(id: $id) { 
                              id avatar bdate first_name last_name second_name gender
                              blood_type {id title display_title} 
                              blood_classes {id title}
                              city { id title }
                              donationStat {count blood_class{id title}}
                              }}",
                    OperationName = "user",
                    Variables = new
                    {
                        id = vkId
                    }
                };
                var graphQlResponse = await graphQlClient.PostAsync(userGet);
                var user = graphQlResponse.GetDataFieldAs<User>("user");
                userJson = JsonConvert.SerializeObject(user);
            }
            return userJson;
        }
    }
}
