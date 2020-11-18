using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Beer
{
    public class BeerListItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brewery { get; set; }
        public int Rating { get; set; }
        public string Website { get; set; }
        
    }
}
