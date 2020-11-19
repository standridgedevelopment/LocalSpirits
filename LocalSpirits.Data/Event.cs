using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace LocalSpirits.Data
{
    public class EventViewModel
    {
        public int id { get; set; }
        public string color { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }
        public int[] daysOfWeek { get; set; }
        public string startRecur { get; set; }
        public string endRecur { get; set; }

        public bool allDay { get; set; }
        
    }
}
