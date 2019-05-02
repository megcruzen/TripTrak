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
using TripTrak.Models.ViewModels;

namespace TripTrak.Controllers
{
    [Authorize]
    public class PlacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public  PlacesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Places
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Place.Include(p => p.Subcategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Places/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var place = await _context.Place
        //        .Include(p => p.Subcategory)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (place == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(place);
        //}

        // GET: Places/Create
        public async Task<IActionResult> Create(int cityId)
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            var place = new Place();
            place.CityId = cityId;

            City city = await _context.City
                .FirstOrDefaultAsync(m => m.Id == cityId);
            place.City = city;

            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name");

            List<Category> categoryList = new List<Category>();

            // ------- Get Data -------
            categoryList = (from category in _context.Category
                            select category).ToList();

            // ------- Insert Select Item in List -------
            categoryList.Insert(0, new Category { Id = 0, Name = "Select" });

            // ------- Assign categoryList to ViewBag.ListofCategory -------
            ViewBag.ListofCategory = categoryList;

            return View(place);
        }
        
        public JsonResult GetSubcategory(int CategoryId)
        {
            List<Subcategory> subcategoryList = new List<Subcategory>();

            // ------- Get Data -------
            subcategoryList = (from subcategory in _context.Subcategory
                               where subcategory.CategoryId == CategoryId
                               select subcategory).ToList();

            // ------- Insert Select Item in List -------
            subcategoryList.Insert(0, new Subcategory { Id = 0, Name = "Select" });
            
            return Json(new SelectList(subcategoryList, "Id", "Name"));
        }

        // POST: Places/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,StartDate,EndDate,PlaceUrl,Notes,Favorite,SubcategoryId,CityId,UserId")] Place place)
        {
            ModelState.Remove("User");
            ModelState.Remove("userId");
            var user = await GetCurrentUserAsync();
            place.UserId = user.Id;
            
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Cities", new { id = place.CityId });
            }
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name", place.SubcategoryId);
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();

            var place = await _context.Place
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (place == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            /*
            ViewData["SubcategoryId"] = new SelectList(_context.Subcategory, "Id", "Name");

            // Get current category (tied to the current subcategoryId)
            var currentCat = place.Subcategory.CategoryId;
            //ViewData["CategoryId"] = place.Subcategory.CategoryId;
            //ViewBag.SelectedCategory = currentCat;

            // Create list of categories
            List<Category> categoryList = new List<Category>();
            //categoryList = (from category in _context.Category
            //                select category).ToList();

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", currentCat);

            // Insert default select item in category list
            categoryList.Insert(0, new Category { Id = 0, Name = "Select" });

            // Assign categoryList to ViewBag.ListofCategory
            //ViewBag.ListofCategories = categoryList(currentCat);

            // Get all Subcategories in the selected Category
            List<Subcategory> subcatList = new List<Subcategory>();
            subcatList = (from subcategory in _context.Subcategory
                            select subcategory)
                            .Where(s => s.CategoryId == currentCat)
                            .ToList();
            ViewBag.ListofSubcategories = subcatList;

            return View(place);
            */

            var currentCatId = place.Subcategory.CategoryId;
            var viewModel = new CategorySubcategoryViewModel()
            {
                Place = place,
                CategoryOptions = new SelectList(_context.Category, "Id", "Name", currentCatId),
                SubcategoryOptions = new SelectList(_context.Subcategory.Where(s => s.CategoryId == currentCatId), "Id", "Name", place.SubcategoryId)
            };
            return View(viewModel);
        }

        // POST: Places/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategorySubcategoryViewModel viewModel)
        {
            var place = viewModel.Place;

            if (id != place.Id)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            ModelState.Remove("Place.User");
            ModelState.Remove("Place.UserId");
            ModelState.Remove("Category.Name");
            var user = await GetCurrentUserAsync();
            place.UserId = user.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.Id))
                    {
                        return RedirectToAction("PageNotFound", "Home");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Cities", new { id = place.CityId });
            }
            
            var currentCatId = place.Subcategory.CategoryId;

            viewModel = new CategorySubcategoryViewModel()
            {
                Place = place,
                CategoryOptions = new SelectList(_context.Category, "Id", "Name", currentCatId),
                SubcategoryOptions = new SelectList(_context.Subcategory, "Id", "Name", place.SubcategoryId)
            };

            return View(viewModel);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var place = await _context.Place
                .Include(p => p.Subcategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null || id == null)
            {
                return RedirectToAction("PageNotFound", "Home");
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Place.FindAsync(id);
            _context.Place.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Cities", new { id = place.CityId });
        }

        private bool PlaceExists(int id)
        {
            return _context.Place.Any(e => e.Id == id);
        }
    }
}
