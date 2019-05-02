using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models.ViewModels
{
    public class CategorySubcategoryViewModel
    {
        public Place Place { get; set; }
        public Category Category { get; set; }
        public SelectList CategoryOptions { get; set; }
        public SelectList SubcategoryOptions { get; set; }
    }
}
