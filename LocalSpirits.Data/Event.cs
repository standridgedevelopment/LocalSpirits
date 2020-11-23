using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalSpirits.Data
{
    public enum TypeOfEvent
    {
        Karaoke, Music, Trivia
    }
    public class Event
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        [ForeignKey(nameof(Business))]
        public int BusinessID { get; set; }
        public virtual Business Business { get; set; }
        public string TypeOfEvent { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public int[] daysOfWeek { get; set; }
        public string DaysOfWeekInput { get; set; }
        public string startRecur { get; set; }
        public string endRecur { get; set; }
        public string url { get; set; }
        public string color { get; set; }
        public List<int> DaysOfWeekConverted
        {
            get
            {
                List<int> converted = new List<int>();
                if (DaysOfWeekInput != null)
                {
                    if (DaysOfWeekInput.ToLower().Contains("sunday")) converted.Add(0);
                    if (DaysOfWeekInput.ToLower().Contains("monday")) converted.Add(1);
                    if (DaysOfWeekInput.ToLower().Contains("tuesday")) converted.Add(2);
                    if (DaysOfWeekInput.ToLower().Contains("wednesday")) converted.Add(3);
                    if (DaysOfWeekInput.ToLower().Contains("thursday")) converted.Add(4);
                    if (DaysOfWeekInput.ToLower().Contains("friday")) converted.Add(5);
                    if (DaysOfWeekInput.ToLower().Contains("saturday")) converted.Add(6);
                }
                return converted;
            }
            set
            {
            }
        }

    }
}
