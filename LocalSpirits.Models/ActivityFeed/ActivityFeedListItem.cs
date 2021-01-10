using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string ProfilePicture { get; set; }
        public string UsersFullName { get; set; }
        public string Activity { get; set; }
        [UIHint("Liked")]
        public bool LikedByUser { get; set; }
        public string Content { get; set; }
        public int ObjectID { get; set; }
        public string ObjectType { get; set; }
        public int AmountOfLikes { get; set; }
        public int AmountOfComments { get; set; }
        public string WhenPosted { get; set; }
        public virtual ICollection<Data.Profile> WhoLiked { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Data.Comment> Comments { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
