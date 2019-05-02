using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Category")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public List<Subcategory> Subcategories { get; set; }
    }
}
