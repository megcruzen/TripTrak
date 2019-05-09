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
                .Where(s => s.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SavedPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPlace = await _context.SavedPlace
                .Include(s => s.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedPlace == null)
            {
                return NotFound();
            }

            return View(savedPlace);
        }

        // GET: SavedPlaces/Create
        public IActionResult Create()
        {
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Name");
            return View();
        }

        // POST: SavedPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlaceId,UserId")] SavedPlace savedPlace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(savedPlace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Name", savedPlace.PlaceId);
            return View(savedPlace);
        }

        // GET: SavedPlaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPlace = await _context.SavedPlace.FindAsync(id);
            if (savedPlace == null)
            {
                return NotFound();
            }
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Name", savedPlace.PlaceId);
            return View(savedPlace);
        }

        // POST: SavedPlaces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlaceId,UserId")] SavedPlace savedPlace)
        {
            if (id != savedPlace.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(savedPlace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SavedPlaceExists(savedPlace.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceId"] = new SelectList(_context.Place, "Id", "Name", savedPlace.PlaceId);
            return View(savedPlace);
        }

        // GET: SavedPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var savedPlace = await _context.SavedPlace
                .Include(s => s.Place)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (savedPlace == null)
            {
                return NotFound();
            }

            return View(savedPlace);
        }

        // POST: SavedPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var savedPlace = await _context.SavedPlace.FindAsync(id);
            _context.SavedPlace.Remove(savedPlace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SavedPlaceExists(int id)
        {
            return _context.SavedPlace.Any(e => e.Id == id);
        }
    }
}
