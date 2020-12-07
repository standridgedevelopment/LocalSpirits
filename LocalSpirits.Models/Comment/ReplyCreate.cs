using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Comment
{
    public class ReplyCreate
    {
        public int? CommentID { get; set; }
        [DisplayName("Reply")]
        public string ReplyContent { get; set; }
        public string SenderFullName { get; set; }
        public string SenderUsername { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
