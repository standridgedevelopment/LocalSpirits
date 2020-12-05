using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }
        //[ForeignKey(nameof(Profile))]
        //public Guid? SenderID { get; set; }
        //public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(ActivityFeed))]
        public int? FeedID { get; set; }
        public virtual ActivityFeed ActivityFeed { get; set; }
        public string CommentContent { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Modified { get; set; }
    }
}
