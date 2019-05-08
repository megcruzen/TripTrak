using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        public string FriendAId { get; set; }
        public string FriendBId { get; set; }

        public ApplicationUser FriendB { get; set; }
        public ApplicationUser FriendA { get; set; }

        public string Status { get; set; }
    }
}
