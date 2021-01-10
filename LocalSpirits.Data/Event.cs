using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string description { get; set; }
        public string url
        {
            get => $"https://localhost:44384/Event/Details/{id}";

            set { }
        }
        public string ThirdPartyWebsite { get; set; }
        public string color { get; set; }


    }
}
