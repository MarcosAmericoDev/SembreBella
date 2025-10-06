using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Services.Implementations;
using SempreBella.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using SempreBella.Repositories.Interfaces;
using SempreBella.Repositories.Implementations;

namespace SempreBella
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                ));

            builder.Services.AddScoped<IRoupaRepository, RoupaRepository>();

            builder.Services.AddScoped<IRoupaService, RoupaService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Administrador/Login";
                    options.AccessDeniedPath = "/AcessDenied";
                });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
