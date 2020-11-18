using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Beer
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Business))]
        public int BreweryID { get; set; }
        public virtual Business Business { get; set; }
        public string Aroma { get; set; }
        public string Taste { get; set; }
        public int ABV { get; set; }
        public string Hops { get; set; }
        public string Package { get; set; }
        public bool KegsAvailable { get; set; }
        public string Availability { get; set; }
        public string Website { get; set; }
        public int Rating { get; set; }
    }
}
