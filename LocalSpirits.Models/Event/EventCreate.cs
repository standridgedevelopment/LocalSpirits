﻿using LocalSpirits.Data;
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
        public string Start
        {
            get => $"{StartYear}-{StartMonth}-{StartDay}";
            set { }
        }
        public string End
        {
            get => $"{EndYear}-{EndMonth}-{EndDay}";
            set { }
        }
        public string DaysOfWeek { 
            get 
            {
                StringBuilder fullDays = new StringBuilder();
                if (Sunday == true) fullDays.Append("Sunday");
                if (Monday == true) fullDays.Append("Monday");
                if (Tuesday == true) fullDays.Append("Tuesday");
                if (Wednesday == true) fullDays.Append("Wednesday");
                if (Thursday == true) fullDays.Append("Thursday");
                if (Friday == true) fullDays.Append("Friday");
                if (Saturday == true) fullDays.Append("Saturday");
                return $"{fullDays}";
            }
        }
        public string StartRecur
        {
            get => $"{StartRecurYear}-{StartRecurMonth}-{StartRecurDay}";
            set { }
        }
        public string EndRecur
        {
            get => $"{EndRecurYear}-{EndRecurMonth}-{EndRecurDay}";
            set { }
        }

        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string EndDay { get; set; }
        public string EndMonth { get; set; }
        public string EndYear { get; set; }

        public string StartRecurDay { get; set; }
        public string StartRecurMonth { get; set; }
        public string StartRecurYear { get; set; }
        public string EndRecurDay { get; set; }
        public string EndRecurMonth { get; set; }
        public string EndRecurYear { get; set; }

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }


        public string Url { get; set; }
        public string Color
        {
            get
            {
                if (TypeOfEvent == TypeOfEvent.Karaoke) return "Green";
                if (TypeOfEvent == TypeOfEvent.Trivia) return "Red";
                if (TypeOfEvent == TypeOfEvent.Music)  return "Blue";
                return "Grey";
            }
            set { }
        }
        public List<int> DaysOfWeekConverted
        {
            get
            {
                List<int> converted = new List<int>();
                if (DaysOfWeek != null)
                {
                    if (DaysOfWeek.ToLower().Contains("monday")) converted.Add(0);
                    if (DaysOfWeek.ToLower().Contains("tuesday")) converted.Add(1);
                    if (DaysOfWeek.ToLower().Contains("wednesday")) converted.Add(2);
                    if (DaysOfWeek.ToLower().Contains("thursday")) converted.Add(3);
                    if (DaysOfWeek.ToLower().Contains("friday")) converted.Add(4);
                    if (DaysOfWeek.ToLower().Contains("saturday")) converted.Add(5);
                    if (DaysOfWeek.ToLower().Contains("sunday")) converted.Add(6);
                }
                
             
                return converted;
            }
            set 
            {
            }
        }
    }
}
