using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using MvcRestauracja.Data;
using MvcRestauracja.Models;
using MvcRestauracja.Filters;
using MvcRestauracja.Utilities;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace MvcRestauracja
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new UserSessionFilter());
            });

            builder.Services.AddDbContext<MvcRestauracjaContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("MvcRestauracjaContext")));
            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            // Dodajemy użytkownika admin przy starcie aplikacji
            await AddAdminUserAsync(app);

            app.Run();
        }

        private static async Task AddAdminUserAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<MvcRestauracjaContext>();
                    context.Database.EnsureCreated();

                    // Sprawdzamy, czy użytkownik admin istnieje w bazie danych
                    if (!context.Users.Any(u => u.Username == "admin"))
                    {
                        // Tworzymy hasło dla użytkownika admin (np. "admin")
                        string password = "admin";
                        // Szyfrujemy hasło
                        string hashedPassword = PasswordHelper.HashPassword(password);

                        var adminUser = new User
                        {
                            Username = "admin",
                            Password = hashedPassword,
                            Email = "",
                            PhoneNumber = "",
                            Admin = true
                        };

                        // Dodajemy użytkownika admin do bazy danych
                        context.Users.Add(adminUser);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Logowanie błędu (tutaj można dodać logowanie błędu np. do pliku lub systemu logowania)
                    Console.WriteLine($"Error creating admin user: {ex.Message}");
                }
            }
        }
    }
}
