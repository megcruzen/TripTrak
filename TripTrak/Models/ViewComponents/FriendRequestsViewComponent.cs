using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripTrak.Data;

namespace TripTrak.Models.ViewComponents
{
    public class FriendRequestsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FriendRequestsViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        /**********/

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await GetCurrentUserAsync();

            var friends = (await _context.Friend
               .Include(f => f.FriendA)
               .Include(f => f.FriendB)
               .Where(f => f.FriendBId == user.Id && f.Status == "pending")
               .ToListAsync());

            return View(friends);
        }
    }
}