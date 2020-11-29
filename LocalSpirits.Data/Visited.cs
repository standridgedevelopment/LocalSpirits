using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Visited
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? Profile_ID { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(Business))]
        public int? BusinessID { get; set; }
        public virtual Business Business { get; set; }
        [ForeignKey(nameof(Event))]
        public int EventID { get; set; }
        public virtual Event Event { get; set; }
        public bool AddToFavorites { get; set; }
        public bool AddToCalendar { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
