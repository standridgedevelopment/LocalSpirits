using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Comment
{
    public class ReplyListItem
    {
        public int ReplyID { get; set; }
        public int? CommentID { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public string SenderProfilePicture { get; set; }
        public string ReplyContent { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
