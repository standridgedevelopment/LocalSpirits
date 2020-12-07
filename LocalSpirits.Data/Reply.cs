using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Reply
    {
        [Key]
        public int ReplyID { get; set; }
        //[ForeignKey(nameof(Profile))]
        //public Guid? SenderID { get; set; }
        //public virtual Profile Profile { get; set; }
        [ForeignKey(nameof(Comment))]
        public int? CommentID { get; set; }
        public virtual Comment Comment { get; set; }
        public string ReplyContent { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public string SenderProfilePicture { get; set; }
        public DateTimeOffset Created { get; set; }

        public DateTimeOffset? Modified { get; set; }
    }
}
