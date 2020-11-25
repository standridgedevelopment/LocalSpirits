using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class FriendRequest
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? ProfileID { get; set; }
        public virtual Profile Profile { get; set; }
        public Guid? FriendsID { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string SendersUsername { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}
