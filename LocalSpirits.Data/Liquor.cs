using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Liquor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int ABV { get; set; }
        public int Package { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Website { get; set; }
        public int Rating { get; set; }
    }
}
