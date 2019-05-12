using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TripTrak.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        [Required]
        [Display(Name = "Username")]
        public string DisplayName { get; set; }

        //public List<Friend> FriendList { get; set; } = new List<Friend>();
        public List<Trip> TripList { get; set; } = new List<Trip>();


        // Set up PK -> FK relationships to other objects
        //public virtual ICollection<SavedPlace> SavedPlaces { get; set; }
    }
}