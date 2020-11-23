using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Models.Event
{
    public class EventDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public string business { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string daysOfWeek { get; set; }
        public string startRecur { get; set; }
        public string endRecur { get; set; }
        public string url { get; set; }
        public string color { get; set; }
       
    }
}
