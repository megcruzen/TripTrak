using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Website")]
        public string PlaceUrl { get; set; }
        public string Notes { get; set; }
        public bool Favorite { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a category and subcategory.")]
        [Display(Name = "Subcategory")]
        public int SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //public virtual ICollection<SavedPlace> SavedPlaces { get; set; }
    }
}
