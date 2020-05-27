using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.Controllers
{
    public class CourseController : Controller
    {
        private readonly DatabaseContext _context;

        public CourseController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [AcceptVerbs("Get", "Post")]

        public async Task<IActionResult> CourseExists(String title)
        {
            var data = await _context.Course.Where(x => x.Title == title).FirstOrDefaultAsync<Course>();

            if (data == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
    }
}