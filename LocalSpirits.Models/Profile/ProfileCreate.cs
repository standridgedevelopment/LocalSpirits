using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Profile
{
    public class ProfileCreate
    {
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
        [Required]
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
    }
}
