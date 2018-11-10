using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DonorSearchBackend.DAL
{
    public class User
    {
        [Key]
        public int? vk_id { get; set; }
        public string second_name { get; set; }
        public string maiden_name { get; set; }
        public DateTime? bdate { get; set; }
        public int? gender { get; set; }
        public int? city_id { get; set; }
        public string about_self { get; set; }
        public int? blood_type_id { get; set; }
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

    }
    
}
