using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL
{
    [Flags]
    public enum BloodClass
    {
        None = 0,
        WholeBlood = 1,
        Plasma = 2,
        Eritocites = 4,
        Granulocites = 8,
        Liekocites = 16,
        Trombocites = 32
    }
}
