using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DatabaseContext _context;

        public StudentsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Name,Password,ConfrimPassword,JWT_Token")] Student student)
        {

            if (ModelState.IsValid)
            {
                student.JWT_Token = GenerateToken(student.Email);
                _context.Add(student);
                await _context.SaveChangesAsync();
                ViewBag.Message = "Registration Complete!";
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Please Provide Valid Information");
            }
            return View(student);
        }

        [AcceptVerbs("Get", "Post")]
        
        public async Task<IActionResult> StudentExists(String email)
        {
            var data = await _context.Student.Where(x => x.Email == email).FirstOrDefaultAsync<Student>();

            if (data == null)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }

        private static string GenerateToken(String Email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constent.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var secToken = new JwtSecurityToken(
                signingCredentials: credentials,
                issuer: Constent.Issuer,
                audience: Constent.Audiance,
                claims: new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, Email),
                new Claim(ClaimTypes.Role, "Student")
                },
                notBefore: DateTime.Now);
               /// expires: DateTime.UtcNow.AddMonths(2));

            var handler = new JwtSecurityTokenHandler();
            
            return handler.WriteToken(secToken);
        }

        /*
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials);
         
       */






    }
}
