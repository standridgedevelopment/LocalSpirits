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
        public string Username { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
            set { }
        }
        [Required]
        public string City { get; set; }
        [Required]
        public StateName State { get; set; }

        public int ZipCode { get; set; }


        public virtual ICollection<Visited> AllVisits { get; set; } = new List<Visited>();
        public virtual ICollection<Friend> FriendsList { get; set; } = new List<Friend>();
        public virtual ICollection<FriendRequest> FriendRequests { get; set; } = new List<FriendRequest>();
        public virtual ICollection<ActivityFeed> Feed { get; set; } = new List<ActivityFeed>();



    }
}
