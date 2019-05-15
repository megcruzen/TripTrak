using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TripTrak.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        //[Required]
        //[Display(Name = "Username")]
        //public string UserName { get; set; }

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

        public List<Trip> TripList { get; set; } = new List<Trip>();
        
    }
}