using backProject.Areas.Admin.Services;
using backProject.DAL;
using backProject.DAL.Entities;
using backProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;

namespace backProject
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddMvc();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    builder =>
                    {
                        builder.MigrationsAssembly(nameof(backProject));
                    });
            });

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 2;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.Configure<AdminUser>(builder.Configuration.GetSection("AdminUser"));

            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<SpeakerService>();

            Constants.RootPath = builder.Environment.WebRootPath;
            Constants.SliderPath = Path.Combine(Constants.RootPath, "assets", "img", "slider");
            Constants.TestimonialSliderPath = Path.Combine(Constants.RootPath, "assets", "img", "testimonial");
            Constants.TeacherPath = Path.Combine(Constants.RootPath, "assets", "img", "teacher");
            Constants.CoursePath = Path.Combine(Constants.RootPath, "assets", "img", "course");
            Constants.BlogPath = Path.Combine(Constants.RootPath, "assets", "img", "blog");
            Constants.SpeakerAndEventPath = Path.Combine(Constants.RootPath, "assets", "img", "event");

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

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var dataInitalizer = new DataInitializer(serviceProvider);
                await dataInitalizer.SeedData();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });

            await app.RunAsync();
        }
    }
}