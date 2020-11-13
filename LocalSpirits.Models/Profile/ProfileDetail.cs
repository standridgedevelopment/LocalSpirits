using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Profile
{
    public class ProfileDetail
    {
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Name")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
            set { }
        }
       
        public string City { get; set; }
        public StateName State { get; set; }
        [DisplayName("Zip Code")]
        public int ZipCode { get; set; }
    }
}
