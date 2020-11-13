using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.City
{
    public class CityEdit
    {
        [DisplayName("City")]
        public string Name { get; set; }
        public StateName State { get; set; }
    }
}
