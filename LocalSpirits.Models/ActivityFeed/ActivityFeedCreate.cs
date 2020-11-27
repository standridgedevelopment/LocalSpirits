using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.ActivityFeed
{
    public class ActivityFeedCreate
    {
        public TypeOfActivity Activity { get; set; }
        public int ObjectID { get; set; }
        public string ObjectType { get; set; }
        public string Content { get; set; }
        public int? BusinessID { get; set; }

    }
}
