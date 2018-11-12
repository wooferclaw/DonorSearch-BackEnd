using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL
{
    public class Donation
    {
        [Key]
        public int id { get; set; }
        public int? ds_Id { get; set; }//id в donorsearch
        [Required]
        public int vk_id { get; set; }
        public DateTime appointment_date_from { get; set; }//период с для даты записи
        public DateTime appointment_date_to { get; set; }//период по для даты записи
        public DateTime? donation_date { get; set; } //дата когда человек хочет сдать кровь или уже сдал
        public bool? donation_success { get; set; } //донация проведена успешно
        public BloodClass blood_class_ids { get; set; } //какой типы сдачи хочет сдать/сдавал
        [Column(TypeName = "text")]
        [MaxLength]
        public string Img { get; set; }//фото справки в base64
        public int? station_id { get; set; }
        public string station_title { get; set; }
        public string station_address { get; set; }
        public DateTime? recomendation_timestamp { get; set; } //время, когда надо напоминать о сдаче анализа
        [DefaultValue("false")]
        public bool finished { get; set; }//закончился ли наш трек по этой задаче?
        public ValidationVisit confirm_visit { get; set; }//повторный визит для проверки анализа через 3-6 месяцев
        public DateTime? previous_donation_date { get; set; }//дата последней успешной донации
    }
}
