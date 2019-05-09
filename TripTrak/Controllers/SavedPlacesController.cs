﻿using System;
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