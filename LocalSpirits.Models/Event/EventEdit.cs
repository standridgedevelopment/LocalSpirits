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
        public int ID { get; set; }
        public string Title
        {
            get
            {
                return $"{TypeOfEvent} at {BusinessName}, {City}";
            }
            set { }
        }
        [Required]
        [DisplayName("Type of Event")]
        public TypeOfEvent TypeOfEvent { get; set; }
        [Required]
        [DisplayName("Name of Business")]
        public string BusinessName { get; set; }
        [Required]
        public string City { get; set; }
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
