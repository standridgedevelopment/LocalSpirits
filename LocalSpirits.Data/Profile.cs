using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{

    public class Profile
    {

        [Key]
        public Guid ID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public StateName State { get; set; }

        public int ZipCode { get; set; }


        public virtual List<Visited> AllVisits { get; set; } = new List<Visited>();


        
    }
}
