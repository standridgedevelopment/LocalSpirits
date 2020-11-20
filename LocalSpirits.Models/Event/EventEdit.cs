using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Event
{
    public class EventEdit
    {
        [DisplayName("Type of Event")]
        public TypeOfEvent Title { get; set; }
        [DisplayName("Name of Business")]
        public string BusinessName { get; set; }
        [Required]
        public string State { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public int[] DaysOfWeek { get; set; }
        public string StartRecur { get; set; }
        public string EndRecur { get; set; }
        public string Url { get; set; }
        public string Color
        {
            get
            {
                if (Title == TypeOfEvent.Karaoke) return "Green";
                if (Title == TypeOfEvent.Trivia) return "Red";
                if (Title == TypeOfEvent.Music) return "Red"; return "Blue";
            }
            set { }
        }
    }
}
