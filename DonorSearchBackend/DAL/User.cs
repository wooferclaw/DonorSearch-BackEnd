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
        public int VkId { get; set; }
        [Required]
        public int DSId { get; set; }
    }
}
