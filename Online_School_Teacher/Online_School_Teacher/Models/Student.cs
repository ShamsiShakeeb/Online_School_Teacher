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
    public class Student
    {
        [Key]
        [Required]
        [MaxLength(50)]
        [RegularExpression("[\\w-]+@([\\w-]+\\.)+[\\w-]+", ErrorMessage = "Invalid Email")]

        [Remote(action: "StudentExists", controller: "Students" , ErrorMessage = "This Email is Already Exists")]
        public String Email { set; get; }

        [Required]
        [MinLength(8, ErrorMessage = "The Name Must be In Between 8-50 Character Long")]
        [MaxLength(50, ErrorMessage = "The Name Must be In Between 8-50 Character Long")]
        public String Name { set; get; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        [RegularExpression("^(?=.*\\d).{8,50}$", ErrorMessage = "Password must be between 8 and 50 digits long and include at least one numeric digit.")]
        public String Password { set; get; }

        [NotMapped]
        [MaxLength(50)]
        [Display(Name = "Confrim Password")]
        [Compare("Password",ErrorMessage = "Password Doesn't Match")]
        public String ConfrimPassword { set; get; }
        [JsonIgnore]
        [Required]
        public String JWT_Token { set; get; }

        public List<StudentBasicInformation> Student_Basic_Info { set; get; } = new List<StudentBasicInformation>();

    }
}
