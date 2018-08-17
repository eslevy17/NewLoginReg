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
    public class RankController : Controller
    {
        private LoginRegContext _context;

        public RankController(LoginRegContext context)
        {
            _context = context;
        }

        [HttpGet("success")]
        public IActionResult Success()
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            else
            {
                ViewBag.CurrentUserId = (int)HttpContext.Session.GetInt32("User");
                ViewBag.AllPets = _context.pets.Include(l => l.likes).ToList();
                Pet NewPet = new Pet();
                ViewBag.NewPet = NewPet;
                return View("Success");
            }
        }

        [HttpPost("addpet")]
        public IActionResult AddPet(Pet pet)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            if (_context.pets.Where(i => i.image == pet.image).ToList().Count() == 0 && ModelState.IsValid)
            {
                pet.created_at = DateTime.Now;
                pet.posted_by = (int)HttpContext.Session.GetInt32("User");
                _context.pets.Add(pet);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Success");
            }
        }

        [HttpGet("likepet/{petid}")]
        public IActionResult LikePet(int petid)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Like NewLike = new Like();
            NewLike.pet_id = petid;
            NewLike.user_id = (int)HttpContext.Session.GetInt32("User");
            _context.likes.Add(NewLike);
            _context.SaveChanges();
            return RedirectToAction("Success");
        }

        [HttpGet("unlikepet/{petid}")]
        public IActionResult UnLikePet(int petid)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Like ThisLike = _context.likes.Where(i => i.user_id == (int)HttpContext.Session.GetInt32("User")).Where(i => i.pet_id == petid).ToList().First();
            _context.Remove(ThisLike);
            _context.SaveChanges();
            return RedirectToAction("Success");
        }

        [HttpGet("pet/{petid}")]
        public IActionResult PetDetails(int petid)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            else
            {
                ViewBag.CurrentUserId = (int)HttpContext.Session.GetInt32("User");
                Pet ThisPet = _context.pets.Where(i => i.id == petid).Include(i => i.likes).Include(i => i.user).ToList().First();
                return View("PetDetails", ThisPet);
            }
        }

        [HttpGet("pet/{petid}/like")]
        public IActionResult LikeThisPet(int petid)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Like NewLike = new Like();
            NewLike.pet_id = petid;
            NewLike.user_id = (int)HttpContext.Session.GetInt32("User");
            _context.likes.Add(NewLike);
            _context.SaveChanges();
            return RedirectToAction("PetDetails", new { petid = petid });
        }

        [HttpGet("pet/{petid}/unlike")]
        public IActionResult UnLikeThisPet(int petid)
        {
            if (HttpContext.Session.GetInt32("User") == null)
            {
                return RedirectToAction("Index", "LoginReg");
            }
            Like ThisLike = _context.likes.Where(i => i.user_id == (int)HttpContext.Session.GetInt32("User")).Where(i => i.pet_id == petid).ToList().First();
            _context.Remove(ThisLike);
            _context.SaveChanges();
            return RedirectToAction("PetDetails", new { petid = petid });
        }
    }
}