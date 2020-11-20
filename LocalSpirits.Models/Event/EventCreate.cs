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
    public class EventCreate
    {
        [Required]
        [DisplayName("Type of Event")]
        public TypeOfEvent Title { get; set; }
        [Required]
        [DisplayName("Name of Business")]
        public string BusinessName { get; set; }
        [Required]
        public string State { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string DaysOfWeek { get; set; }
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
        public int? Monday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("monday")) return 0;
                return null;
            }
            set { }
        }
        public int? Tuesday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("tuesday")) return 1;
                return null;
            }
            set { }
        }
        public int? Wednesday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("wednesday")) return 2;
                return null;
            }
            set { }
        }
        public int? Thursday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("thursday")) return 3;
                return null;
            }
            set { }
        }
        public int? Friday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("friday")) return 4;
                return null;
            }
            set { }
        }
        public int? Saturday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("saturday")) return 5;
                return null;
            }
            set { }
        }
        public int? Sunday
        {
            get
            {
                if (DaysOfWeek.ToLower().Contains("sunday")) return 6;
                return null;
            }
            set { }
        }


    }
}
