using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Comment
{
    public class CommentListItem
    {
        public int CommentID { get; set; }
        public Guid? SendersID { get; set; }
        public int? ActivityFeedID { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public string CommentContent { get; set; }
        [UIHint("Liked")]
        public bool LikedByUser { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfReplies { get; set; }
        public string WhenPosted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Data.Reply> Replies { get; set; }
    }
}
