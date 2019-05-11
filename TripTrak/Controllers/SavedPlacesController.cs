using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripTrak.Data;
using TripTrak.Models;

namespace TripTrak.Controllers
{
    public class SavedPlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SavedPlacesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: SavedPlaces
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var applicationDbContext = _context.SavedPlace
                .Include(s => s.Place)
                    .ThenInclude(p => p.User)
                .Include(s => s.Place)
                    .ThenInclude(p => p.Subcategory)
                .Include(s => s.Place)
                    .ThenInclude(p => p.City)
                .Where(s => s.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SavedPlaces/GetSavedPlaces
        public async Task<JsonResult> GetSavedPlaces(int PlaceId)
        {
            var user = await GetCurrentUserAsync();

            var savedPlaces = _context.SavedPlace
                .Where(p => p.PlaceId == PlaceId && p.UserId == user.Id).ToList();

            return Json(savedPlaces);
        }

        // POST: SavedPlaces/SavePlace
        public async Task<JsonResult> SavePlace(int PlaceId)
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            // Create new saved place
            SavedPlace savedPlace = new SavedPlace();
            savedPlace.UserId = user.Id;
            savedPlace.PlaceId = PlaceId;

            _context.Add(savedPlace);
            await _context.SaveChangesAsync();

            return Json(savedPlace);
        }

        // POST: SavedPlaces/UnsavePlace
        public async Task<JsonResult> UnsavePlace(int PlaceId)
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            // Find saved place
            var savedPlace = await _context.SavedPlace
                .Where(p => p.PlaceId == PlaceId && p.UserId == user.Id)
                .FirstOrDefaultAsync();

            _context.Remove(savedPlace);
            await _context.SaveChangesAsync();

            return Json(savedPlace);
        }

        // POST: SavedPlaces/RemovePlace/5
        [HttpPost, ActionName("RemovePlace")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemovePlace(int id)
        {
            var savedPlace = await _context.SavedPlace.FindAsync(id);
            _context.SavedPlace.Remove(savedPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
