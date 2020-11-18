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
        public int ID { get; set; }
        [DisplayName("City")]
        public string Name { get; set; }
        [DisplayName("State")]
        public StateName State { get; set; }

    }
}
