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
    public class CitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CitiesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Cities
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.City.ToListAsync());
        //}

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(int? id, int? catId)
        {
            var city = await _context.City
                .Include(c => c.Trip)
                .Include(c => c.Places)
                    .ThenInclude(p => p.Subcategory)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            // TO DO: Convert to .Join
            
            if (catId != null)
            {
                city.Places = city.Places.Where(p => p.Subcategory.CategoryId == catId).ToList();
            }

            if (city == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(city);
        }

        // GET: Cities/Create
        public async Task<IActionResult> Create(int tripId)
        {
            var city = new City();
            city.TripId = tripId;

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            Trip trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.Id == tripId);
            city.Trip = trip;

            return View(city);
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate,ImageUrl,Notes,TripId")] City city)
        {
            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            city.UserId = user.Id;

            if (ModelState.IsValid)
            {
                _context.Add(city);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Trips", new { id = city.TripId });
            }
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            var city = await _context.City.FindAsync(id);
            if (city == null || id == null || city.UserId != user.Id)
            {
                return RedirectToAction("PageNotFound", "Home");
            }
            return View(city);
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,EndDate,ImageUrl,Notes,TripId")] City city)
        {
            if (id != city.Id)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            city.UserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Id))
                    {
                        return RedirectToAction("PageNotFound", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Trips", new { id = city.TripId });
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await GetCurrentUserAsync();
            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Id == id);

            Trip trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.Id == city.TripId);

            if (city == null || id == null || city.UserId != user.Id)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var city = await _context.City.FindAsync(id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Trips", new { id = city.TripId });
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.Id == id);
        }
    }
}
