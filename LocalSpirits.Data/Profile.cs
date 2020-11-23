using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    
    public class Profile
    {

        [Key]
        public Guid ID { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public StateName State { get; set; }

        public int ZipCode { get; set; }

        public virtual List<Visited> Favorites
        {
            get
            {
                List<Visited> favorites = new List<Visited>();
                foreach (var visit in AllVisits)
                {
                    if (visit.AddToFavorites == true)
                    {
                        var visitdetails = new Visited
                        {
                            BusinessID = visit.BusinessID,
                        };
                        favorites.Add(visitdetails);
                    }
                }

                return favorites;
            }
            set { }
        }

        public virtual List<Event> Events
        {
            get
            {
                List<Event> events = new List<Event>();
                foreach (var visit in AllVisits)
                {
                    if (visit.AddToCalendar == true)
                    {
                        var eventDetails = new Event
                        {
                            TypeOfEvent = $"{visit.Event.TypeOfEvent}",
                            BusinessID = visit.Event.BusinessID,
                            start = visit.Event.start,
                            end = visit.Event.end,
                            DaysOfWeekInput = visit.Event.DaysOfWeekInput,
                            startRecur = visit.Event.startRecur,
                            endRecur = visit.Event.endRecur,
                            url = visit.Event.url,
                            color = visit.Event.color
                        };
                        events.Add(eventDetails);
                    }
                }

                return events;
            }
            set { }
        }
        public virtual List<Visited> AllVisits { get; set; } = new List<Visited>();

    }
}
