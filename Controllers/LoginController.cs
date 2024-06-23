using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System;
using MvcRestauracja.Data;
using MvcRestauracja.Models; // Dodajemy referencję do przestrzeni nazw zawierającej User
using MvcRestauracja.Utilities;

namespace MvcRestauracja.Controllers
{
    public class LoginController : Controller

    {
        private readonly MvcRestauracjaContext _context;

        public LoginController(MvcRestauracjaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (ViewBag.IsLoggedIn)
            {
                return RedirectToAction("LoggedIn");
            }
            else
            {
                // Ustawienie ViewBag.IsLoggedIn na false, jeśli użytkownik nie jest zalogowan
                return View();
            }
        }




        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Wyszukujemy użytkownika po nazwie użytkownika
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            // Sprawdzamy czy użytkownik istnieje i czy hasło jest poprawne
            if (user != null && VerifyPassword(password, user.Password))
            {
                HttpContext.Session.SetString("User", username);
                HttpContext.Session.SetString("isAdmin", user.Admin.ToString());
                ViewBag.IsLoggedIn = HttpContext.Session.GetString("User") != null;
                ViewBag.IsAdmin = user.Admin;
                // ViewBag.IsLoggedIn = true;
                return RedirectToAction("Index", "Home");
            }
            // Jeśli nie, wracamy do widoku logowania
            return View("Index");
        }

        public IActionResult CreateUser()
        {
            return RedirectToAction("Create", "User");
        }


        public IActionResult LoggedIn()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Jeśli użytkownik nie jest zalogowany, przekierowujemy go do strony logowania
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Logout()
        {
            // Usuwamy informację o zalogowanym użytkowniku z sesji
            HttpContext.Session.Remove("User");
            return RedirectToAction("Index");
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return storedPassword == PasswordHelper.HashPassword(enteredPassword);
        }
    }
}
