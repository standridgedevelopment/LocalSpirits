using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Brewery
{
    public class BreweryListItem
    {
        public int ID { get; set; }

        public string TypeOfEstablishment { get; set; }
        [DisplayName("Brewery Name")]
        public string Name { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
    }
}
