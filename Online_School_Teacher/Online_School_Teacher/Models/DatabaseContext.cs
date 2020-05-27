using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_School_Teacher.Models
{
    public class DatabaseContext :  DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Course>  Course { get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<StudentBasicInformation> Student_Basic_Information { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<TeacherBasicInformation> Teacher_Basic_Information { get; set; }

        public DbSet<Tutorial> Tutorial { get; set; }

        public DbSet<Admin> Admin { get; set; }


    }
}
