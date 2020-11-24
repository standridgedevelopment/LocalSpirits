using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Like
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? UserID { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(ActivityFeed))]
        public int? FeedID { get; set; }
        public virtual ActivityFeed ActivityFeed { get; set; }
        public bool Liked { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Modified { get; set; }
    }
}
