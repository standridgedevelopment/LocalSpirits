using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.ActivityFeed
{
    public class ActivityFeedListItem
    {
        public int ID { get; set; }
        public Guid? UserID { get; set; }
        public int? BusinessID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Activity { get; set; }
        public string Content { get; set; }
        public int ObjectID { get; set; }
        public string ObjectType { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
