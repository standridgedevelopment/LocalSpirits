using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Brewery
{
    public class BreweryEdit
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("Brewery Name")]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public StateName State { get; set; }
        [Required]
        [MinLength(5), MaxLength(5)]
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
        public string Hours { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        [DisplayName("Does this Brewery have live music?")]
        public bool LiveMusic { get; set; }
    }
}
