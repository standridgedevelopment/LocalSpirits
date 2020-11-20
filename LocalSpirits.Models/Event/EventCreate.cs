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
        public TypeOfEvent TypeOfEvent { get; set; }
        public int BusinessID { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string DaysOfWeek { get; set; } 
           
        public int[] DaysOfWeekConverted = new int[7];
        public string StartRecur { get; set; }
        public string EndRecur { get; set; }
        public string Url { get; set; }
        public string Color
        {
            get
            {
                if (TypeOfEvent == TypeOfEvent.Karaoke) return "Green";
                if (TypeOfEvent == TypeOfEvent.Trivia) return "Red";
                if (TypeOfEvent == TypeOfEvent.Music) return "Red"; return "Blue";
            }
            set { }
        }
        public int? Monday
        {
            get
            {
                return null;
            }
            set 
            {
                if (DaysOfWeek.ToLower().Contains("monday")) DaysOfWeekConverted.Append(0);
                if (DaysOfWeek.ToLower().Contains("tuesday")) DaysOfWeekConverted.Append(1);
                if (DaysOfWeek.ToLower().Contains("wednesday")) DaysOfWeekConverted.Append(2);
                if (DaysOfWeek.ToLower().Contains("thursday")) DaysOfWeekConverted.Append(3);
                if (DaysOfWeek.ToLower().Contains("friday")) DaysOfWeekConverted.Append(4);
                if (DaysOfWeek.ToLower().Contains("saturday")) DaysOfWeekConverted.Append(5);
                if (DaysOfWeek.ToLower().Contains("sunday")) DaysOfWeekConverted.Append(6);

            }
        }
    }
}
