using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class StudentBasicInformation
    {
        [Key]
        
        public int ID { set; get; }
       
        [Required]
        [MaxLength(20)]
        public String Phone { set; get; }
        [Required]
        [MaxLength(100)]
        public String Address { set; get; }
    }
}
