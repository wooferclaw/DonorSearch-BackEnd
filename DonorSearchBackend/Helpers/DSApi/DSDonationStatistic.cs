using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.Helpers.DSApi
{
    public class DSDonationStatistic
    {
        public int? count { get; set; }
        public DSBloodClass blood_class { get; set; }
    }
}
