﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DisplayFormat(DataFormatString= "{0:MM/dd/yyyy}")]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString= "{0:MM/dd/yyyy}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public string Summary { get; set; }
        public string Notes { get; set; }

        [Required]
        public string UserId { get; set; }
        
        public ApplicationUser User { get; set; }

        public List<City> Cities { get; set; } = new List<City>();

    }
}
