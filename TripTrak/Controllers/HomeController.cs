using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTrak.Data;
using TripTrak.Models;
using TripTrak.Models.ViewModels;

namespace TripTrak.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public async Task<IActionResult> Index(HomeViewModel viewModel)
        {
            var currentUser = await GetCurrentUserAsync();
            var user = _context.ApplicationUsers
                .Where(u => u.Id == currentUser.Id)
                .FirstOrDefault();

            var received = (await _context.Friend
               .Include(f => f.FriendA)
               .Include(f => f.FriendB)
               .Where(f => f.FriendBId == user.Id && f.Status == "pending")
               .ToListAsync());

            var sent = (await _context.Friend
               .Include(f => f.FriendA)
               .Include(f => f.FriendB)
               .Where(f => f.FriendAId == user.Id && f.Status == "pending")
               .ToListAsync());

            viewModel = new HomeViewModel()
            {
                CurrentUser = user,
                SentRequests = sent,
                ReceivedRequests = received
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PageNotFound()
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
