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
using HelpDesk.AuditsManager;
using System.Security.Claims;
using HelpDesk.Services;

namespace HelpDesk.Controllers
{
    public class SystemCodeDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SystemCodeDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SystemCodeDetails
        public async Task<IActionResult> Index()
        {
            var systemcodesdetails = await _context
                .SystemCodesDetails
                .Include(s => s.SystemCode)
                .Include(s => s.CreatedBy)
                .ToListAsync();

            return View(systemcodesdetails);
        }

        // GET: SystemCodeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodesDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Create
        public IActionResult Create()
        {
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Description");
            return View();
        }

        // POST: SystemCodeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( SystemCodeDetail systemCodeDetail)
        {
            var userId = User.GetUserId();
            systemCodeDetail.CreatedOn = DateTime.Now;
            systemCodeDetail.CreatedById = userId;


                _context.Add(systemCodeDetail);
               await _context.SaveChangesAsync(userId);

         

            return RedirectToAction(nameof(Index));
            
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Decription", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodesDetails.FindAsync(id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }
            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Decription", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  SystemCodeDetail systemCodeDetail)
        {
            if (id != systemCodeDetail.Id)
            {
                return NotFound();
            }


            try
            {

                var userId = User.GetUserId();
                systemCodeDetail.ModifiedOn = DateTime.Now;
                systemCodeDetail.ModifiedById = userId;

                _context.Update(systemCodeDetail);
                await _context.SaveChangesAsync(userId);



            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SystemCodeDetailExists(systemCodeDetail.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["SystemCodeId"] = new SelectList(_context.SystemCodes, "Id", "Decription", systemCodeDetail.SystemCodeId);
            return View(systemCodeDetail);
        }

        // GET: SystemCodeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemCodeDetail = await _context.SystemCodesDetails
                .Include(s => s.SystemCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (systemCodeDetail == null)
            {
                return NotFound();
            }

            return View(systemCodeDetail);
        }

        // POST: SystemCodeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.GetUserId();
            var systemCodeDetail = await _context.SystemCodesDetails.FindAsync(id);
            if (systemCodeDetail != null)
            {
                _context.SystemCodesDetails.Remove(systemCodeDetail);
            }

            await _context.SaveChangesAsync(userId);
            return RedirectToAction(nameof(Index));
        }

        private bool SystemCodeDetailExists(int id)
        {
            return _context.SystemCodesDetails.Any(e => e.Id == id);
        }
    }
}
