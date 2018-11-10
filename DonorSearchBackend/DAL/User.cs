using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DonorSearchBackend.Helpers;

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
        public string about_self { get; set; }
        public string blood_type { get; set; }
        [Required]
        public BloodClass blood_class_ids { get; set; }
        public bool? bone_marrow { get; set; }
        public bool? cant_to_be_donor { get; set; }
        public int? donor_pause_to { get; set; }
        public bool? has_registration { get; set; }
        [Required]
        public int ds_id { get; set; }
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }

        public int CheckForBloodNeccesity(List<NeedRequest> needRequests)
        {
            if (needRequests?.LastOrDefault() != null)
            {
                //TODO time sorted blood requests
                switch (blood_type)
                {
                    case "0+": return needRequests.LastOrDefault().one_plus_demand;
                    case "0-": return needRequests.LastOrDefault().one_minus_demand;
                    case "A+": return needRequests.LastOrDefault().two_plus_demand;
                    case "A-": return needRequests.LastOrDefault().two_minus_demand;
                    case "B+": return needRequests.LastOrDefault().three_plus_demand;
                    case "B-": return needRequests.LastOrDefault().three_minus_demand;
                    case "AB+": return needRequests.LastOrDefault().four_plus_demand;
                    case "AB-": return needRequests.LastOrDefault().four_minus_demand;

                    default: return -1;
                }
            }
            return -1;
        }
    }

    
}
