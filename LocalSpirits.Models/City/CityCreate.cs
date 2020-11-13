using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.City
{
    public class CityCreate
    {
        [Required]
        [DisplayName("City")]
        public string Name { get; set; }
        [Required]
        public StateName State { get; set; }
    }
}
