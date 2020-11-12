using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Aroma { get; set; }
        public string Taste { get; set; }
        public int ABV { get; set; }
        public string Hops { get; set; }
        public string Package { get; set; }
        public bool KegsAvailable { get; set; }
        public string Availability { get; set; }
        public string website { get; set; }
        public int Rating { get; set; }
    }
}
