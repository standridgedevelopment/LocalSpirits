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
        [Required]
        [DisplayName("Type of Event")]
        public TypeOfEvent TypeOfEvent { get; set; }
       
        public int BusinessID { get; set; }
        [Required]
        public string City { get; set; }
        public string Description { get; set; }
        [DisplayName("Day")]
        public string StartDay { get; set; }
        [DisplayName("Month")]
        public string StartMonth { get; set; }
        [DisplayName("Year")]
        public string StartYear { get; set; }
        public string ThirdPartyWebsite { get; set; }
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
        public string Start
        {
            get => $"{StartYear}-{StartMonth}-{StartDay}";
            set { }
        }
    }
}
