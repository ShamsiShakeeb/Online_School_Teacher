using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class Teacher
    {
        [Key]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("[\\w-]+@([\\w-]+\\.)+[\\w-]+",ErrorMessage = "Invalid Email")]
        [Remote(action: "UniqueEmail",controller: "Teachers" ,ErrorMessage = "This Email is Already Exist")]
        public String Email { set; get; }

        [Required]
        [MinLength(8,ErrorMessage ="The Name Must be In Between 8-50 Character Long")]
        [MaxLength(50, ErrorMessage = "The Name Must be In Between 8-50 Character Long")]
      
        public String Name { set; get; }
     
        public List<TeacherBasicInformation> Teacher_Basic_Information { set; get; } = new List<TeacherBasicInformation>();
        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*\\d).{8,50}$" , ErrorMessage = "Password must be between 8 and 50 digits long and include at least one numeric digit.")]
        public String Password { set; get; }

        public List<Course> course { set; get; } = new List<Course>();
        [JsonIgnore]
        public String JWT_Token { set; get; }


        [NotMapped]
        [Compare("Password")]
        [Display(Name="Confrim Password")]
        [DataType(DataType.Password)]
        public String ConfrimPassword { set; get; }

    }
}
