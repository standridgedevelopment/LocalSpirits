using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.City
{
    public class CityListItem
    {
        [DisplayName("City")]
        public string Name { get; set; }
        public string State { get; set; }
    }
}
