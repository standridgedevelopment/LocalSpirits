using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public enum TypeOfEstablishment
    {
        Winery, Brewery, Distillary, Bar
    }
    public class Business
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int ID { get; set; }
        [Required]
        public string TypeOfEstablishment{ get; set; }

        [ForeignKey(nameof(City))]
        public int? CityID { get; set; }
        public virtual City City { get; set; }
        public int? ZipCode { get; set; }
        public string Hours { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Visited> Ratings { get; set; }
    }
}
