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
using TripTrak.Models.ViewModels;

namespace TripTrak.Controllers
{
    public class FriendsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FriendsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        // GET: Friends
        public async Task<IActionResult> Index()
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            var friends = (await _context.Friend
                .Include(f => f.FriendA)
                    .ThenInclude(fA => fA.TripList)
                        .ThenInclude(t => t.Cities)
                .Include(f => f.FriendB)
                    .ThenInclude(fB => fB.TripList)
                        .ThenInclude(t => t.Cities)
                .Where(f => (f.FriendAId == user.Id || f.FriendBId == user.Id) && f.Status == "accepted")
                .ToListAsync())
                .Select(f => f.FriendA.Id == user.Id ? f.FriendB : f.FriendA);

            return View(friends);
        }

        // GET: Friends/FriendSearch
        public async Task<IActionResult> FriendSearch(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return View(new List<ApplicationUser>());
            }

            var users = _context.ApplicationUsers.AsQueryable();
            users = users.Where(u => u.LastName.Contains(searchString) || u.FirstName.Contains(searchString));
            return View(await users.AsNoTracking().ToListAsync());
        }

        // POST: Friends/AddFriend
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFriend(AddFriendViewModel viewModel)
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            // Create Friend connection with "pending" status
            Friend newFriend = new Friend();
            newFriend.FriendAId = user.Id;
            newFriend.FriendBId = viewModel.Id;
            newFriend.Status = "pending";
            
            if (ModelState.IsValid)
            {
                _context.Add(newFriend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        /*
        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FriendA,FriendB,Status")] Friend friend)
        {
            if (ModelState.IsValid)
            {
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(friend);
        }
        */

        // GET: Friends/AcceptRequest/5
        //public async Task<IActionResult> AcceptRequest(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var friend = await _context.Friend.FindAsync(id);
        //    if (friend == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(friend);
        //}

        // POST: Friends/AcceptRequest/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptRequest(int id)
        {
            // Get current friendship
            var friendship = await _context.Friend.FindAsync(id);

            // Update status in friendship
            friendship.Status = "accepted";

            _context.Update(friendship);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(string friendId)
        {
            // Get current user
            var user = await GetCurrentUserAsync();

            // Find friendship
            var friendship = await _context.Friend
                .Include(f => f.FriendA)
                .Include(f => f.FriendB)
                .Where(f => (f.FriendAId == friendId && f.FriendBId == user.Id) || (f.FriendBId == friendId && f.FriendAId == user.Id))
                .FirstOrDefaultAsync();

            return View(friendship);
        }

        // POST: Friends/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var friend = await _context.Friend.FindAsync(id);
            _context.Friend.Remove(friend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FriendExists(int id)
        {
            return _context.Friend.Any(e => e.Id == id);
        }
    }
}
