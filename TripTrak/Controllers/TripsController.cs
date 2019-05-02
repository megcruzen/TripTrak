using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripTrak.Data;
using TripTrak.Models;

namespace TripTrak.Controllers
{
    [Authorize]
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TripsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trip
                .Include(t => t.User)
                .Include(t => t.Cities);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var trip = await _context.Trip
                .Include(t => t.User)
                .Include(t => t.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trip == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(trip);
        }

        // GET: Trips/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,Summary,Notes,UserId")] Trip trip)
        {
            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            trip.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
            
            return View(trip);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,Summary,Notes,UserId")] Trip trip)
        {
            if (id != trip.Id)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            trip.UserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
                    {
                        return RedirectToAction("PageNotFound", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var trip = await _context.Trip
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trip == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            _context.Trip.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trip.Any(e => e.Id == id);
        }
    }
}
