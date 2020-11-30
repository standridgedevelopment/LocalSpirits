using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Notification
{
    public class NotificationListItem
    {
        public int? ActivityFeedID { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
    }
}
