using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Friends
{
    public class FriendRequestCreate
    {
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public Guid? ProfileID { get; set; }
        public Guid? FriendsID { get; set; }
    }
}
