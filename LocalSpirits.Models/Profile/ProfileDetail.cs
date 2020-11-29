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
        public string Username { get; set; }
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


        public ICollection<Data.Event> Events { get; set; }
        public ICollection<Friend> FriendsList { get; set; } 
        public ICollection<FriendRequest> FriendRequests { get; set; }
        public virtual ICollection<Data.ActivityFeed> Feed { get; set; } 
    }
}
