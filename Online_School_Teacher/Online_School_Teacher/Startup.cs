using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Online_School_Teacher.Models;

namespace Online_School_Teacher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DevConnection")));

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Student";

            })
             .AddCookie("Student", options =>
             {

                 options.Cookie.Name = "Student.Says";
                 options.LoginPath = "/Student/Login";

             })
             .AddCookie("Teacher", options =>
             {
                 options.Cookie.Name = "Teacher.Says";
                ///// options.Cookie.Name = "Employee.Says";
                 options.LoginPath = "/Teachers/Login";
                 

             })
             .AddCookie("Admin", options =>
              {
                  options.Cookie.Name = "Admin.Says";
                  ///// options.Cookie.Name = "Employee.Says";
                  options.LoginPath = "/Admin/Login";
                
              });

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.IsEssential = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

 
            app.UseAuthorization();

            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
