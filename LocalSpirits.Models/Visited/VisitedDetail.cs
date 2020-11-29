using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Visited
{
    public class VisitedDetail
    {
        public int ID { get; set; }
        public Guid? UserID { get; set; }
        public int? BusinessID { get; set; }
        public int? EventID { get; set; }
        public bool AddToFavorites { get; set; }
        public bool AddToCalendar { get; set; }
        [DisplayName("Rating (1-5)")]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Review { get; set; }
 

    }
}
