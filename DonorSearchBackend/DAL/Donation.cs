using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL
{
    public class Donation
    {
        [Key]
        public int id { get; set; }
        [Required]
        public BloodClass blood_class_ids { get; set; } //какой типы сдачи хочет сдать/сдавал
        //id в donorsearch
        public int? ds_Id { get; set; }
        public bool? succeed { get; set; }
        public DateTime recomendation_timestamp { get; set; } //время, когда надо напоминать о сдаче анализа
        [Required]
        public int vk_id { get; set; }
        [Required]
        public DateTime donation_timestamp{ get; set; }
        public int? station_id { get; set; }
        [Required]
        public int status_id { get; set; }


    }
    public enum Statuses
    {
        Appointment = 1,//запись + противопоказания
        Recomendations = 2,//рекомендации
        Donation = 3,//результат
        CheckAfterDonation = 4//проверка крови
    }
}
