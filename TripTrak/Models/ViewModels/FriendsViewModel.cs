using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models.ViewModels
{
    public class FriendsViewModel
    {
        public List<ApplicationUser> CurrentFriends { get; set; } = new List<ApplicationUser>();
        public List<Friend> ReceivedRequests { get; set; } = new List<Friend>();
        public List<Friend> SentRequests { get; set; } = new List<Friend>();
    }
}

