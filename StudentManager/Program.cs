using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StudentManager.Data;

//using StudentManager.Route;

namespace StudentManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var mvcBuilder = builder.Services.AddRazorPages();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<StudentManagerContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManager")));

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            if (builder.Environment.IsDevelopment())
            {
                mvcBuilder.AddRazorRuntimeCompilation();
            }
            
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            /*// Cấu hình chính sách phân quyền
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireStudentRole", policy => policy.RequireRole("student"));
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("admin"));
                options.AddPolicy("RequireTeacherRole", policy => policy.RequireRole("teacher"));
            });*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern:"""{controller=LoginForm}/{action=Index}""");

            app.MapControllerRoute(
                name: "home",
                pattern: """{controller=Home}/{action=Index}/{id?}""");

            app.MapControllerRoute(
                name: "roleRoute",
                pattern: """{controller=Home}/{action=Index}/{role?}/{id?}""");

            app.Run();
        }
    }
}
