using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Business
{
    public class BusinessEdit
    {
        public int ID { get; set; }
        [Required]
        [DisplayName("What type of business is this? (Brewery, Winery, Distillary)")]
        public TypeOfEstablishment typeOfEstablishment { get; set; }
        [Required]
        [DisplayName("Business Name")]
        public string Name { get; set; }
        public int? CityID { get; set; }
        public string State { get; set; }
        [Required]
        [DisplayName("Zip Code")]
        public int? ZipCode { get; set; }
        public string Hours { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }
}
