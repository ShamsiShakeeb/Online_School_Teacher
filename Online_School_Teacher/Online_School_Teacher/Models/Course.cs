using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class Course
    {
        [Key]
        public int ID { set; get; }
        [Required]
        [MaxLength(100)]
        [Remote(action: "CourseExists" , controller: "Course" , ErrorMessage = "This Course Name Already Exisit")]
        public String Title{ set; get; }
        [Required]
       
        public String Description { set; get; }

       
       //// public List<Tutorial> tutorial { set; get; } = new List<Tutorial>();

    }
}
