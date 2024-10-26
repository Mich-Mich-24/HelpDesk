﻿using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpDesk.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser>userManager, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;

            
        }
        // GET: UsesController
        public async Task<ActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: UsesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(ApplicationUser user)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                ApplicationUser registeduser = new();
                registeduser.FirstName = user.FirstName;
                registeduser.UserName = user.UserName;
                registeduser.MiddleName = user.MiddleName;
                registeduser.LastName = user.LastName;  
                registeduser.NormalizedUserName = user.NormalizedUserName;
                registeduser.Email = user.Email;
                registeduser.EmailConfirmed = true;
                registeduser.Gender = user.Gender;
                registeduser.Country = user.Country;
                registeduser.City = user.City;
                registeduser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.CreateAsync(registeduser, user.PasswordHash);
                if (result.Succeeded)
                {

                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }


            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: UsesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
