using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Visited
{
    public class VisitedCreate
    {
        public int? EventID { get; set; }
        public int? BusinessID { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
      
        public string Review { get; set; }
        public bool AddToFavorites { get; set; }
        public bool AddToCalendar { get; set; }
    }
}
