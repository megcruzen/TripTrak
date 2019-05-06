using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TripTrak.Models.ViewModels
{
    public class CityPlaceSubcatViewModel
    {
        public City City { get; set; }
        public List<Place> Places { get; set; }
        public List<Category> Categories { get; set; }
    }
}
