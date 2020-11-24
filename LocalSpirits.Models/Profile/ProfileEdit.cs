using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Profile
{
    
    public class ProfileEdit
    {
        public Guid ID { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public StateName State { get; set; }
        public int ZipCode { get; set; }
    }
}
