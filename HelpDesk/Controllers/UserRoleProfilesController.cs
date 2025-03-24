using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpDesk.Data;
using HelpDesk.Models;
using HelpDesk.Data.Migrations;
using System.Security.Claims;
using HelpDesk.Services;
using HelpDesk.ViewModels;

namespace HelpDesk.Controllers
{
    public class UserRoleProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRoleProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserRoleProfiles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserRoleProfiles.Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserRoleProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles
                .Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }

            return View(userRoleProfile);
        }

        [HttpGet]
        public async Task<IActionResult> UserRights(string id)
        {

            ProfileViewModel vm = new();

            vm.RoleId = id;

            var allroles =  await _context.Roles.OrderBy(x=>x.Name).ToListAsync();

            ViewBag.RoleId = new SelectList(allroles, "Id", "Name",id);

            vm.SystemTasks = await _context.SystemTasks
                .Include("ChildTasks.ChildTasks.ChildTasks")
                .OrderBy(x=>x.OrderNumber)
                .Where(x=>x.Parent == null)
                .ToListAsync();

            vm.RightsIdsAssigned = await _context.UserRoleProfiles
                .Where(x=>x.RoleId == id).Select(x=>x.TaskId).ToListAsync();


            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserRights(ProfileViewModel vm)
        {

            try
            {
                var allprofiles = _context.UserRoleProfiles.Where(x => x.RoleId == vm.RoleId).ToList();
                _context.UserRoleProfiles.RemoveRange(allprofiles);
                foreach(var taskId in vm.Ids)
                {
                    var rightprofile = new UserRoleProfile
                    {
                        TaskId = taskId,
                        RoleId = vm.RoleId,
                        CreatedOn = DateTime.Now,
                        CreatedById = User.GetUserId()
                    };
                    _context .UserRoleProfiles.Add(rightprofile);
                }


                await _context.SaveChangesAsync(User.GetUserId());
                TempData["MESSAGE"] = "Role Rights Assigned Successfully"; 
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = "There was an issue assigning rights to the Role" + ex.Message;
            }


            var allroles = await _context.Roles.OrderBy(x => x.Name).ToListAsync();

            ViewBag.RoleId = new SelectList(allroles, "Id", "Name", vm.RoleId);

            vm.SystemTasks = await _context.SystemTasks
                .Include("ChildTasks.ChildTasks.ChildTasks")
                .OrderBy(x => x.OrderNumber)
                .Where(x => x.Parent == null)
                .ToListAsync();

            vm.RightsIdsAssigned = await _context.UserRoleProfiles
                .Where(x => x.RoleId == vm.RoleId).Select(x => x.TaskId).ToListAsync();

            return View(vm);
        }


        // GET: UserRoleProfiles/Create
        public IActionResult Create()
        {
          
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name");
            return View();
        }

        // POST: UserRoleProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserRoleProfile userRoleProfile)
        {

            var userId = User.GetUserId();
            userRoleProfile.CreatedOn = DateTime.Now;
            userRoleProfile.CreatedById = userId;

            _context.Add(userRoleProfile);
                await _context.SaveChangesAsync(userId);
                return RedirectToAction(nameof(Index));
         
          
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // GET: UserRoleProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles.FindAsync(id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }
         
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // POST: UserRoleProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserRoleProfile userRoleProfile)
        {
            if (id != userRoleProfile.Id)
            {
                return NotFound();
            }

                try
                {
                var userId = User.GetUserId();
                userRoleProfile.ModifiedOn = DateTime.Now;
                userRoleProfile.ModifiedById = userId;

                _context.Update(userRoleProfile);
                    await _context.SaveChangesAsync(userId);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRoleProfileExists(userRoleProfile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
         
          
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", userRoleProfile.RoleId);
            ViewData["TaskId"] = new SelectList(_context.SystemTasks, "Id", "Name", userRoleProfile.TaskId);
            return View(userRoleProfile);
        }

        // GET: UserRoleProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRoleProfile = await _context.UserRoleProfiles
                .Include(u => u.CreatedBy)
                .Include(u => u.ModifiedBy)
                .Include(u => u.Role)
                .Include(u => u.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userRoleProfile == null)
            {
                return NotFound();
            }

            return View(userRoleProfile);
        }

        // POST: UserRoleProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.GetUserId();

            var userRoleProfile = await _context.UserRoleProfiles.FindAsync(id);
            if (userRoleProfile != null)
            {
                _context.UserRoleProfiles.Remove(userRoleProfile);
            }

            await _context.SaveChangesAsync(userId);
            return RedirectToAction(nameof(Index));
        }

        private bool UserRoleProfileExists(int id)
        {
            return _context.UserRoleProfiles.Any(e => e.Id == id);
        }
    }
}
