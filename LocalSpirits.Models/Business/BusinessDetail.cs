using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LocalSpirits.Data;

namespace LocalSpirits.Models.Business
{
    public class BusinessDetail
    {
        public int ID { get; set; }
        [DisplayName("Business Type")]
        public string TypeOfEstablishment { get; set; }
        [DisplayName("Business Name")]
        public string Name { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public int? CityID { get; set; }
        public string State { get; set; }
        [DisplayName("Zip Code")]
        public int? ZipCode { get; set; }
        public string Hours { get; set; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        [DisplayName("Does this Business have live music?")]
        public ICollection<LocalSpirits.Data.Event> Events { get; set; } 
    }
}
