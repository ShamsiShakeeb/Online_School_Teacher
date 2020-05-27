using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class Admin
    {
        [Key]
        public int ID { set; get; }

        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public String Email { set; get; }

        [MaxLength(50)]
        [DataType(DataType.Password)]
        public String Password { set; get; }

        
        public String JWT_Token { set; get; }
    }
}
