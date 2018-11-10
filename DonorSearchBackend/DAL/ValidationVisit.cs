using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL
{
    public class ValidationVisit
    {
        [Key]
        public int id { get; set; }
        public DateTime? date_from { get; set; }//дата с которой донор должен прийти для повторного анализа
        public DateTime? date_to { get; set; }//дата по которую донор должен прийти для повторного анализа
        public DateTime? visit_date { get; set; }//дата, когда пользователь хочет пойти на повторный приём (для напоминаний)
        public bool? success { get; set; } //пользователь пришёл сдать повторный анализ
        public bool? without_donation { get; set; }//сдаёт только анализ из пальца?
    }
}
