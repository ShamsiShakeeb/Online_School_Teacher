using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.Controllers
{
    [Authorize(Roles = "Teacher", AuthenticationSchemes = "Teacher")]
    public class TeachersController : Controller
    {
        private readonly DatabaseContext _context;

        public TeachersController(DatabaseContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Email", "Name", "Password", "JWT_Token", "ConfrimPassword")]Teacher teacher)
        {

            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Registration Complete!";
            }
            return View(teacher);

        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> UniqueEmail(String email)
        {
            var data = await _context.Teacher.Where(x => x.Email == email).FirstOrDefaultAsync<Teacher>();

            if (data == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        private bool TeacherExists(string id)
        {
            return _context.Teacher.Any(e => e.Email == id);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login([Bind("Email,Password")] Teacher teacher)
        {

            var data = await _context.Teacher.Where((x => x.Email == teacher.Email && x.Password == teacher.Password)).FirstOrDefaultAsync<Teacher>();
            ClaimsIdentity identity = null;

            if (data != null)
            {
                if (data.JWT_Token.Equals("0"))
                {
                    ModelState.AddModelError(string.Empty, "Sorry Your not yet approved by the Admin");
                    return View(teacher);
                }
                else if (data.JWT_Token.Equals("Block"))
                {
                    ModelState.AddModelError(string.Empty, "Sorry Your Blocked By The Admin");
                    return View(teacher);
                }

                else if (ValidateToken(data.JWT_Token).Contains("Unable to decode the header"))
                {
                    ModelState.AddModelError(string.Empty, "Sorry Something Went Horror");
                    return View(teacher);
                }
                else if (ValidateToken(data.JWT_Token).Contains("Lifetime validation failed"))
                {
                    ModelState.AddModelError(string.Empty, "Your Deadline is Expired Please Contact with Admin");
                    return View(teacher);
                }
                else if (ValidateToken(data.JWT_Token).Contains("Decode_Error_1000323_Temparing_With_Token"))
                {
                    ModelState.AddModelError(string.Empty, "Decode_Error_1000323_Temparing_With_Token");
                    return View(teacher);
                }
                else
                {

                    HttpContext.Session.SetString("Token_Key", data.JWT_Token);
                    HttpContext.Session.SetString("User_Email", data.Email);

                    await HttpContext.SignOutAsync("Student");
                    await HttpContext.SignOutAsync("Teacher");
                    await HttpContext.SignOutAsync("Admin");

                    identity = new ClaimsIdentity(new[] {
                        new Claim(ClaimTypes.Email,teacher.Email),
                        new Claim(ClaimTypes.Role,"Teacher")


                     }, CookieAuthenticationDefaults.AuthenticationScheme);



                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync("Teacher", principal);



                    return Redirect("~/Teachers/Index/");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "False Login Attempt");
            }




            return View(teacher);
        }


        public IActionResult Answer_The_Qus()
        {
            return View();
        }

      
        [HttpPost]
        public async Task<IActionResult> Answer_The_Qus(String id, Tutorial tutorial)
        {

            var data = await _context.Course.Where(x => x.ID == Convert.ToInt32(id)).FirstOrDefaultAsync<Course>();
            if (data == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Tutorial.Add(tutorial);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Your Answer is Added"; 
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something Went Wrong");
            }

            return View(tutorial);
        }

        private  String ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            }
            catch (Exception e)
            {

                return e.ToString()+"Decode_Error_1000323_Temparing_With_Token"; 
            }
            return "Fine";
        }
        private  TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = Constent.Issuer,
                ValidAudience = Constent.Audiance,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constent.Secret)) // The same key as the one that generate the token
            };
        }
    }
}
