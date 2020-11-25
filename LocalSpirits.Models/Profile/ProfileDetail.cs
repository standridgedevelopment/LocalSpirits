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


        public List<Data.Visited> Favorites { get; set; }
        public List<Data.Event> Events { get; set; }
        public List<Friend> FriendsList { get; set; } 
        public  List<FriendRequest> FriendRequests { get; set; }
        public virtual List<ActivityFeed> Feed { get; set; } 
    }
}
