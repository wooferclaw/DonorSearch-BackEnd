using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.Helpers.DSApi
{
    public class DSNeedRequest
    {
        public DSBloodStation blood_station { get; set; }
        public DSBloodType blood_type { get; set; }
        public object end_time { get; set; }
        public int id { get; set; }
        public int intensity { get; set; }
        public object is_disabled { get; set; }
        public object start_time { get; set; }
        public DateTime when_is_it { get; set; }
    }
}
