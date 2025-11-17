using HelpDesk.Data;
using HelpDesk.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HelpDesk.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("~/identity/account/login");
                }

                // Get tickets with related data
                var tickets = await _context.Tickets
                    .Include(t => t.CreatedBy)
                    .Include(t => t.SubCategory)
                        .ThenInclude(sc => sc.Category)
                    .Include(t => t.Priority)
                    .Include(t => t.Status)
                    .Include(t => t.TicketComments)
                    .OrderByDescending(x => x.CreatedOn)
                    .Take(50)
                    .ToListAsync();

                // Calculate dashboard statistics
                var newTicketsStatus = await _context.SystemCodesDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "ResolutionStatus" && 
                               (x.Code == "AwaitingAssignment" || x.Code == "Assigned"))
                    .Select(x => x.Id)
                    .ToListAsync();

                var resolvedTicketsStatus = await _context.SystemCodesDetails
                    .Include(x => x.SystemCode)
                    .Where(x => x.SystemCode.Code == "ResolutionStatus" && 
                               (x.Code == "Resolved" || x.Code == "Closed"))
                    .Select(x => x.Id)
                    .ToListAsync();

                ViewData["NewTickets"] = await _context.Tickets
                    .CountAsync(t => newTicketsStatus.Contains(t.StatusId));

                ViewData["ResolvedTickets"] = await _context.Tickets
                    .CountAsync(t => resolvedTicketsStatus.Contains(t.StatusId));

                ViewData["UserRegistrations"] = await _context.Users.CountAsync();

                ViewData["UniqueVisitors"] = await _context.Tickets
                    .Select(t => t.CreatedById)
                    .Distinct()
                    .CountAsync();

                // Chart data - Tickets by Category
                var ticketsByCategory = await _context.Tickets
                    .Include(t => t.SubCategory)
                        .ThenInclude(sc => sc.Category)
                    .GroupBy(t => t.SubCategory.Category.Name)
                    .Select(g => new { name = g.Key ?? "Unknown", y = g.Count() })
                    .ToListAsync();

                ViewData["TicketsByCategoryJson"] = System.Text.Json.JsonSerializer.Serialize(ticketsByCategory);

                // Chart data - Tickets by Status
                var ticketsByStatus = await _context.Tickets
                    .Include(t => t.Status)
                    .GroupBy(t => t.Status.Description)
                    .Select(g => new { name = g.Key ?? "Unknown", y = g.Count() })
                    .ToListAsync();

                ViewData["TicketsByStatusJson"] = System.Text.Json.JsonSerializer.Serialize(ticketsByStatus);

                // Chart data - Tickets by SubCategory
                var ticketsBySubCategory = await _context.Tickets
                    .Include(t => t.SubCategory)
                    .GroupBy(t => t.SubCategory.Name)
                    .Select(g => new { name = g.Key ?? "Unknown", y = g.Count() })
                    .OrderByDescending(g => g.y)
                    .Take(5)
                    .ToListAsync();

                ViewData["TicketsBySubCategoryJson"] = System.Text.Json.JsonSerializer.Serialize(ticketsBySubCategory);

                // Chart data - Tickets by Priority
                var ticketsByPriority = await _context.Tickets
                    .Include(t => t.Priority)
                    .GroupBy(t => t.Priority.Description)
                    .Select(g => new { name = g.Key ?? "Unknown", y = g.Count() })
                    .ToListAsync();

                ViewData["TicketsByPriorityJson"] = System.Text.Json.JsonSerializer.Serialize(ticketsByPriority);

                return View(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                TempData["Error"] = "An error occurred while loading dashboard data. Please try again.";
                return View(new List<Ticket>());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
