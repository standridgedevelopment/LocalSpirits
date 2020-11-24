﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class Friend
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey(nameof(Profile))]
        public Guid? ProfileID { get; set; }
        public virtual Profile Profile { get; set; }

        [ForeignKey(nameof(FriendsProfile))]
        public Guid? FriendsID { get; set; }
        public virtual Profile FriendsProfile { get; set; }
    }
}
