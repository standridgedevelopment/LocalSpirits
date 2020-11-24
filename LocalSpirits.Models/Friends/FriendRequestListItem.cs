using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Friends
{
    public class FriendRequestListItem
    {
        public string FullName { get; set; }
        public DateTimeOffset TimeSent { get; set; }
    }
}
