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
            var user = await GetCurrentUserAsync();

            List<ApplicationUser> friendList = new List<ApplicationUser>();

            /*
            var friends = await _context.Friend
                .Include(f => f.FriendA)
                .Include(f => f.FriendB)
                .Where(f => f.FriendAId == user.Id || f.FriendBId == user.Id)
                .ToListAsync();
                */

            /*
            var friends = await _context.ApplicationUsers
                .Include(u => u.FriendList)
                    //.ThenInclude(fl => fl.FriendA)
                //.Where(f => f.FriendAId == user.Id || f.FriendBId == user.Id)
                .ToListAsync();
                */


            var friends = await _context.Friend
                .Include(f => f.FriendA)
                    .ThenInclude(fA => fA.TripList)
                .Include(f => f.FriendB)
                    .ThenInclude(fB => fB.TripList)
                .Where(f => f.FriendAId == user.Id || f.FriendBId == user.Id)
                .ToListAsync();

            
            foreach (Friend friend in friends)
            {
                if (friend.FriendB.Id != user.Id)
                {
                    friendList.Add(friend.FriendB);
                }

                if (friend.FriendA.Id != user.Id)
                {
                    friendList.Add(friend.FriendA);
                }
            }

            //var friends2 = await _context.Friend
            //    .Include(f => f.FriendA)
            //        .ThenInclude(fA => fA.TripList)
            //    .Include(f => f.FriendB)
            //        .ThenInclude(fB => fB.TripList)
            //    .Where(f => f.FriendBId == user.Id)
            //    .ToListAsync();

            //foreach (Friend friend in friends2)
            //{
            //    var friendA = friend.FriendA;
            //    friendList.Add(friendA);
            //}
            

            /*
            var friends = _context.ApplicationUsers
                .Join(
                    _context.Friend.Where(x => x.FriendAId == user.Id),
                    u => u.Id,
                    f => f.FriendAId,
                    (u, f) => u)
                .Take(int.MaxValue);
            //.ToList();
            */

            /*
            var friends = _context.Friend
                .Join(
                    _context.ApplicationUsers.Where(x => x.Id == user.Id),
                    f => f.FriendBId,
                    u => u.Id,
                    (f, u) => f)
                .Take(int.MaxValue);
                */

            /*
            var robotDogs = context.RobotDogs
            .Join(
                context.RobotFactories.Where(x => x.Location == "Texas"),
                d => d.RobotFactoryId,
                f => f.RobotFactoryId,
                (d, f) => d)
            .ToList();
            */

            //return View(friends);
            return View(friendList);
        }

        // GET: Friends/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
        }

        // GET: Friends/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Friends/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Friends/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }
            return View(friend);
        }

        // POST: Friends/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FriendA,FriendB,Status")] Friend friend)
        {
            if (id != friend.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(friend);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FriendExists(friend.Id))
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
            return View(friend);
        }

        // GET: Friends/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            return View(friend);
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
