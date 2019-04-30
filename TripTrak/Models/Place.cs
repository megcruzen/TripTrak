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
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PlaceUrl { get; set; }
        public string Notes { get; set; }
        public bool Favorite { get; set; }

        [Required]
        public int SubcategoryId { get; set; }
        [Display(Name = "Subcategory")]
        public Subcategory Subcategory { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ICollection<SavedPlace> SavedPlaces { get; set; }
    }
}
