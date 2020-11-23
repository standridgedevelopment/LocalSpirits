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
        Like, AddToCalendar, Comment, Share, Review
    }
    public class ActivityFeed
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? UserID { get; set; }
        public virtual Profile Profile { get; set; }
        public TypeOfActivity Activity { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Modified { get; set; }

    }
}
