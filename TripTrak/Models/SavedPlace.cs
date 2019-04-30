using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class SavedPlace
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PlaceId { get; set; }
        public Place Place { get; set; }

        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
