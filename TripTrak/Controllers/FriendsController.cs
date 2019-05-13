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

            var user = await GetCurrentUserAsync();

            // Get a list of current friends
            var friends = (await _context.Friend
                .Include(f => f.FriendA)
                .Include(f => f.FriendB)
                .Where(f => (f.FriendAId == user.Id || f.FriendBId == user.Id) && (f.Status == "accepted" || f.Status == "pending"))
                .ToListAsync())
                .Select(f => f.FriendA.Id == user.Id ? f.FriendB : f.FriendA);

            // Get a list of all users, minus current user
            var users = _context.ApplicationUsers
                .Where(u => u.Id != user.Id)
                .AsQueryable();
            
            // Create new list to hold filtered users
            var filteredUsers = users.Except(friends);

            // Search query
            filteredUsers = filteredUsers.Where(u => u.FullName.Contains(searchString, StringComparison.OrdinalIgnoreCase) || u.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            return View(filteredUsers);
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

        // POST: Friends/DeleteRequest/5
        [HttpPost, ActionName("DeleteRequest")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            var friend = await _context.Friend.FindAsync(id);
            _context.Friend.Remove(friend);
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

    }
}
