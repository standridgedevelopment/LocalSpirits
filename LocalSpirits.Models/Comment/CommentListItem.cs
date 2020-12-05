using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Comment
{
    public class CommentListItem
    {
        public int CommentID { get; set; }
        public int? ActivityFeedID { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public string CommentContent { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
