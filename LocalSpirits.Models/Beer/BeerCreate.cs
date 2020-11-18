using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Beer
{
    public class BeerCreate
    {

        [Required]
        public string Name { get; set; }
        public string Brewery { get; set; }
        public string BreweryCity {get;set;}
        public string Aroma { get; set; }
        public string Taste { get; set; }
        public int ABV { get; set; }
        public string Hops { get; set; }
        public string Package { get; set; }
        [DisplayName("Is this available in a keg?")]
        public bool KegsAvailable { get; set; }
        public string Availability { get; set; }
        public string Website { get; set; }
    }
}
