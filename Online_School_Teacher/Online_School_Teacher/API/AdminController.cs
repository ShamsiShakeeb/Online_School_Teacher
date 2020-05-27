using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.API
{

    public class AdminController : Controller
    {
        private readonly DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("api/getTeachers")]
        [HttpGet]
        public async Task<ActionResult<ArrayList>> Get_Approve_Teacher()
        {
            var data = await _context.Teacher.Where(x => (x.JWT_Token != "0" && x.JWT_Token != "Block")).Select(x => new { x.Email, x.Name }).ToListAsync();
            ArrayList list = new ArrayList(data);
            return Ok(new { data = list });
        }
        [Route("api/nonApproveTeachers")]
        [HttpGet]
        public async Task<ActionResult<ArrayList>> Get_Non_Approve_Teacher()
        {
            var data = await _context.Teacher.Where(x => x.JWT_Token == "0").Select(x => new { x.Email, x.Name }).ToListAsync();
            ArrayList list = new ArrayList(data);
            return Ok(new { data = list });
        }

        [Route("api/BlockTeacher")]



        [HttpGet]
        public async Task<ActionResult<ArrayList>> Get_Block_Teacher()
        {
            var data = await _context.Teacher.Where(x => x.JWT_Token == "Block").Select(x => new { x.Email, x.Name }).ToListAsync();
            ArrayList list = new ArrayList(data);
            return Ok(new { data = list });
        }

        [Route("api/Teacher/Block/{email}")]
        [HttpGet("{email}")]
        public async Task<ActionResult> Block_Teacher(String email)
        {
            var data = await _context.Teacher.Where(x => x.Email == email).FirstOrDefaultAsync<Teacher>();

            if (data != null)
            {
                data.JWT_Token = "Block";
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Teacher is Blocked" });
            }
            else
            {
                return BadRequest();
            }

            //  return View();
        }
        [Route("api/Teacher/Renew/{email}")]
        [HttpPut("{email}")]
        public async Task<ActionResult> Renew_Teacher(String email)
        {

            var data = await _context.Teacher.Where(x => x.Email == email).FirstOrDefaultAsync<Teacher>();

            if (data != null)
            {
                data.JWT_Token = GenerateToken(email);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Teacher is Renewed" });
            }
            else
            {
                return BadRequest();
            }

            //  return View();
        }

        [Route("api/Teacher/Delete/{email}")]
        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete_Teacher(String email)
        {
            var data = await _context.Teacher.Where(x => x.Email == email).FirstOrDefaultAsync<Teacher>();

            if (data != null)
            {
                _context.Teacher.Remove(data);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Teacher is Deleted" });
            }
            else
            {
                return BadRequest();
            }

            //  return View();
        }

        [Route("api/Course/New_Course")]
        [HttpPost]
        public async Task<IActionResult> Add_New_Course(Course course)
        {
            if (ModelState.IsValid)
            {
                var data = await _context.Course.Where(x => x.Title == course.Title).FirstOrDefaultAsync<Course>();

                if (data == null)
                {

                    _context.Course.Add(course);
                    await _context.SaveChangesAsync();
                    return Ok(new { success = true, message = "New Course Added" });
                }
                else
                {
                    return Ok(new { success = false, message = "This Course Already Exisits" });

                }
            }
            else
            {
                return Json(new { success = false, message = "Something Went Wrong" });
            }
           
        }

        [Route("api/Course/CourseList")]
        [HttpGet]
        public async Task<ActionResult<ArrayList>> Get_Course_List()
        {
            var data = await _context.Course.Select(x => x).ToListAsync<Course>();
            ArrayList list = new ArrayList(data);
            return Json(new { data = list});
        }

        [Route("api/Course/Update_Course/{id}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update_Course(int id,Course course)
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
                        return Json(new { success = true, message = "Course Updated Successfully" });
                  
                }
                else
                {
                    return Json(new { success = false, message = "Course Didn't Update" });
                   /// return View(course);
                }

                
                
            }
        }
        [Route("api/Course/Delete_Course/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete_Course(int id)
        {
            var data = await _context.Course.Where(x => x.ID == id).FirstOrDefaultAsync<Course>();
            if (data == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Course.Remove(data);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Course Deleted Successfully" });
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
                new Claim(ClaimTypes.Role, "Teacher")
                },
                notBefore: DateTime.Now,
                expires: DateTime.UtcNow.AddMonths(2));

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(secToken);
        }


    }
}