using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? Profile_ID { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(ActivityFeed))]
        public int? ActivityFeedID { get; set; }
        public virtual ActivityFeed ActivityFeed { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public DateTimeOffset TimeCreated { get; set; }
        public bool Recieved { get; set; }
    }
}
