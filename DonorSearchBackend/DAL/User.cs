using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DonorSearchBackend.Helpers;
using DonorSearchBackend.Helpers.DSApi;

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
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }

        public int CheckForBloodNeccesity(List<DSNeedRequest> needRequests)
        {
            if (needRequests?.LastOrDefault() != null)
            {
                //TODO time sorted blood requests
                if (blood_type == needRequests.LastOrDefault().blood_type.title) return needRequests.LastOrDefault().intensity;

                //switch (blood_type)
                //{
                //    case "0(I) Rh+": return needRequests.LastOrDefault().blood_type.title;
                //    case "0(I) Rh-": return needRequests.LastOrDefault().intensity;
                //    case "A(II) Rh+": return needRequests.LastOrDefault().intensity;
                //    case "A(II) Rh-": return needRequests.LastOrDefault().intensity;
                //    case "B(III) Rh+": return needRequests.LastOrDefault().intensity;
                //    case "B(III) Rh-": return needRequests.LastOrDefault().intensity;
                //    case "AB(IV) Rh+": return needRequests.LastOrDefault().intensity;
                //    case "AB(IV) Rh-": return needRequests.LastOrDefault().intensity;

                //    default: return -1;
                //}
            }
            return 50;
        }
    }
}
