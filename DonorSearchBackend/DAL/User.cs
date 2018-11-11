using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DonorSearchBackend.Helpers;
using DonorSearchBackend.Helpers.DSApi;
using System.ComponentModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace DonorSearchBackend.DAL
{
    public class User
    {
        [Key]
        public int vk_id { get; set; }
        public string second_name { get; set; }
        public string maiden_name { get; set; }
        public DateTime? bdate { get; set; }
        public int? gender { get; set; }
        public int? city_id { get; set; }
        public string city_title { get; set; }
        public string region_title { get; set; }
        public string about_self { get; set; }
        public string blood_type { get; set; }
        [Required]
        public BloodClass blood_class_ids { get; set; }
        public bool? bone_marrow { get; set; }
        public bool? cant_to_be_donor { get; set; }
        public DateTime? donor_pause_to { get; set; }
        public bool? has_registration { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [DefaultValue("false")]
        public bool? is_first_donor { get; set; }

        public int CheckForBloodNeccesity(List<DSNeedRequest> needRequests)
        {
            if (needRequests?.LastOrDefault() != null)
            {
                //TODO time sorted blood requests
                if (blood_type == needRequests.LastOrDefault().blood_type.title) return needRequests.LastOrDefault().intensity;
            }
            return 49;
        }

        public async Task<string> SendPushNotification(string message, string fragment)
        {
            string accessToken = ConfigurationManager.AppSetting["AppSettings:VkAccessToken"];

            var httpClient = new HttpClient { BaseAddress = new Uri("https://api.vk.com/method/") };
            httpClient.DefaultRequestHeaders.Add("User-Agent", "DonorSearchBackend");

            var httpResult = await httpClient.GetAsync(
                string.Format("notifications.sendMessage?user_ids={0}&message={1}&access_token={2}", vk_id, message, accessToken));

            return httpResult.ReasonPhrase;
        }
    }
}
