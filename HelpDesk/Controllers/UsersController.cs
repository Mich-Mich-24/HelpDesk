using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.AuditsManager;
using System.Security.Claims;
using HelpDesk.Services;

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
            var users = await _context.Users
                .Include(x=>x.Role)
                .Include(x=>x.Gender)
                .ToListAsync();
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
            ViewData["GenderId"] = new SelectList(_context.SystemCodesDetails
                .Include(x=>x.SystemCode)
                .Where(x=>x.SystemCode.Code =="Gender"), "Id", "Description");
            ViewData["RoleId"] = new SelectList(_context.Roles.ToList(), "Id", "Name");
            return View();
        }

        // POST: UsesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>Create(ApplicationUser user)
        {
            try
            {
                var roledetails = await _context.Roles.Where(x=>x.Id == user.RoleId).FirstOrDefaultAsync();
                var userId = User.GetUserId();

                ApplicationUser registeduser = new();
                registeduser.FirstName = user.FirstName;
                registeduser.UserName = user.UserName;
                registeduser.MiddleName = user.MiddleName;
                registeduser.LastName = user.LastName;  
                registeduser.NormalizedUserName = user.NormalizedUserName;
                registeduser.Email = user.Email;
                registeduser.EmailConfirmed = true;
                registeduser.GenderId = user.GenderId;
                registeduser.RoleId = user.RoleId;
                registeduser.Country = user.Country;
                registeduser.City = user.City;
                registeduser.PhoneNumber = user.PhoneNumber;

                var result = await _userManager.CreateAsync(registeduser, user.PasswordHash);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(registeduser, roledetails.Name);
                    TempData["MESSAGE"] = "User Details successfully Created";

                    return RedirectToAction(nameof(Index));
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

            ViewData["GenderId"] = new SelectList(_context.SystemCodesDetails
               .Include(x => x.SystemCode)
               .Where(x => x.SystemCode.Code == "Gender"), "Id", "Description", user.GenderId);
            ViewData["RoleId"] = new SelectList(_context.Roles.ToList(), "Id", "Name", user.RoleId);
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
