using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class Tutorial
    {
        [Key]

        public int ID { set; get; }
        [Required]
        [MaxLength(100)]
        public String Title { set; get; }
        [Required]
        public String Description { set; get; }

        [ForeignKey("CID")]
       
        int CID { set; get; }

        //[Required]
        //[MaxLength(100)]
        //public String VideoFileName { set; get; }
        //[Required]
        //[MaxLength(10)]
        //public String Approve { set; get; }
    }
}
