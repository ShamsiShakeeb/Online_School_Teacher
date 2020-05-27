using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Admin")]
    public class AdminController : Controller
    {

        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Admin");
            HttpContext.Session.SetString("Token_Key", "");
            return Redirect("~/Home/Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Teacher_List()
        {
            ////Used Web API & Ajax From Admin (Done)
            return View();
        }

        public IActionResult Add_Course()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add_Course(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Course.Add(course);
                await _context.SaveChangesAsync();
                ViewBag.Message = "New Course Added Successfully";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Data Information is in Wrong Formate");
            }


            return View();
        }

        public IActionResult Course_List()
        {
            ///Used Web API & Ajax
            return View();
        }


        
        public IActionResult From_Core_Update_Course(int id)
        {
            Course course = _context.Course.Where(x => x.ID == id).FirstOrDefault<Course>();

            if (course == null)
            {
                return BadRequest();
            }
           
            return View(course);
        }
        
       /* [HttpPut("{id}")]
        public async Task<IActionResult> From_Core_Update_Course(int id, Course course)
        {
            var data = await _context.Course.Where(x => x.ID == id).FirstOrDefaultAsync<Course>();
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    data.Title = course.Title;
                    data.Description = course.Description;
                    await _context.SaveChangesAsync();
                    ViewBag.Message = "Course Updated";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Data Informations");
                   
                }
                return View(course);


            }
        }
        */

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email","Password")]Admin admin)
        {
            if (ModelState.IsValid)
            {
            var data = await _context.Admin.Where((x => x.Email == admin.Email && x.Password == admin.Password)).FirstOrDefaultAsync<Admin>();
            ClaimsIdentity identity = null;

                if (data != null)
                {
                    HttpContext.Session.SetString("Token_Key", data.JWT_Token);

                    await HttpContext.SignOutAsync("Student");
                    await HttpContext.SignOutAsync("Teacher");
                    await HttpContext.SignOutAsync("Admin");

                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Email,admin.Email),
                    new Claim(ClaimTypes.Role,"Admin")


            }, CookieAuthenticationDefaults.AuthenticationScheme);



                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("Admin", principal);



                    return Redirect("~/Admin/Index/");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "False Login Attempt");
                }
            }
            

            return View(admin);
        }
     
    }
}