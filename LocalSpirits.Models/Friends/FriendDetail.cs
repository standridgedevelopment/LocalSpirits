using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Friends
{
    public class FriendDetail
    {
        public int ID { get; set; }
        public Guid? ProfileID { get; set; }
        public Guid? FriendsID { get; set; }
    }
}
