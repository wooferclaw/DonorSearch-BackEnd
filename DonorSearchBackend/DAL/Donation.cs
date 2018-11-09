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
        public int Id { get; set; }
        [Required]
        public int VkId { get; set; }
        [Required]
        public DateTime DonationTimestamp{ get; set; }
        [Required]
        public int StationId { get; set; }
        [Required]
        public int StatusId { get; set; }
        public int DonationType { get; set; } //какой типы сдачи хочет сдать/сдавал
        //id в donorsearch
        public int DonationId { get; set; }
        public bool Succeed { get; set; }
        public DateTime RecomendationTimestamp { get; set; } //время, когда надо напоминать о сдаче анализа

    }
    public enum Statuses
    {
        Appointment = 1,//запись + противопоказания
        Recomendations = 2,//рекомендации
        Donation = 3,//результат
        CheckAfterDonation = 4//проверка крови
    }
}
