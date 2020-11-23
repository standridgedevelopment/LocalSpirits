using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Visited
{
    public class VisitedEdit
    {
        public int ID { get; set; }
        public Guid? UserID { get; set; }
        public int? BusinessID { get; set; }
        public int? EventID { get; set; }
        public bool AddToFavorites { get; set; }
        public bool AddToCalendar { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
