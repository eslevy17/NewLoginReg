using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using LoginReg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace LoginReg.Controllers
{
    public class LoginRegController : Controller
    {
        private LoginRegContext _context;

        public LoginRegController(LoginRegContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return View("Index");
            }
            else
            {
                return RedirectToAction("Success", "Rank");
            }
        }

        [HttpPost("register")]
        public IActionResult Register(ViewUser newuser)
        {
            if(ModelState.IsValid && _context.users.Where(u => u.email == newuser.email).ToList().Count == 0)
            {
                PasswordHasher<ViewUser> Hasher = new PasswordHasher<ViewUser>();
                newuser.password = Hasher.HashPassword(newuser, newuser.password);

                User insertuser = new User();

                insertuser.first_name = newuser.first_name;
                insertuser.last_name = newuser.last_name;
                insertuser.email = newuser.email;
                insertuser.password = newuser.password;

                _context.users.Add(insertuser);
                _context.SaveChanges();

                TempData["registrationsuccess"] = "Registration successful!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["registrationfailed"] = "Registration failed.";
                return View("Index");
            }
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            User founduser = null;
            List<User> matchingusers = _context.users.Where(u => u.email == email).ToList();
            if (matchingusers.Count > 0)
            {
                founduser = matchingusers.First();
                var Hasher = new PasswordHasher<User>();
                if (Hasher.VerifyHashedPassword(founduser, founduser.password, password) != 0)
                {
                    HttpContext.Session.SetInt32("User", founduser.id);
                    return RedirectToAction("Success", "Rank");
                }
                else
                {
                    TempData["loginfail"] = "Login failed.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["loginfail"] = "Login failed.";
                return RedirectToAction("Index");
            }
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
