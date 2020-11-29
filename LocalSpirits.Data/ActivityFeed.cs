using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public enum TypeOfActivity
    {
        Like, AddToCalendar, Comment, Share, Rating, NewEvent, Follow,
    }
    public class ActivityFeed
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? UserID { get; set; }
        public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(Business))]
        public int? BusinessID { get; set; }
        public virtual Business Business { get; set; }
        public string UsersFullName { get; set; }
        public string Username { get; set; }
        public string Activity { get; set; }
        public int ObjectID { get; set; }
        public string ObjectType { get; set; }
        public string Content { get; set; }
        public int AmountOfLikes
        {
            get
            {
                return Likes.Count;
            }

        }
        public DateTimeOffset Created { get; set; }
        public string HowLongAgo { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}
