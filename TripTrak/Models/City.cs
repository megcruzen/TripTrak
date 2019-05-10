using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        public string Notes { get; set; }

        [Required]
        public int TripId { get; set; }
        public Trip Trip { get; set; }
        
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<Place> Places { get; set; } = new List<Place>();
        //public List<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
