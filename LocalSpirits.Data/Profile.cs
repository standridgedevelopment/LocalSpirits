using LocalSpirits.WebMVC.Data;
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
        public Guid UserID { get; set; }
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
                List<Event> favorites = new List<Event>();
                foreach (var visit in AllVisits)
                {
                    if (visit.AddToCalendar == true)
                    {
                        var visitdetails = new Event
                        {
                            id = visit.Event.id,
                            title = $"{visit.Event.TypeOfEvent} at {visit.Event.Business.Name}, {visit.Event.City}",
                            TypeOfEvent = visit.Event.TypeOfEvent,
                            BusinessID = visit.Event.BusinessID,
                            Business = visit.Event.Business,
                            start = visit.Event.start,
                            end = visit.Event.end,
                            daysOfWeek = visit.Event.daysOfWeek,
                            startRecur = visit.Event.startRecur,
                            endRecur = visit.Event.endRecur,
                            ThirdPartyWebsite = visit.Event.ThirdPartyWebsite,
                            color = visit.Event.color,
                        };
                        favorites.Add(visitdetails);
                    }
                }

                return favorites;
            }
            set { }
        }
        public virtual List<Visited> AllVisits { get; set; } = new List<Visited>();


        //public virtual List<Visited> AllVisits
        //{
        //    get
        //    {
        //        var results = new List<Visited>();
        //        using (var ctx = new ApplicationDbContext())
        //        {
        //            var Visits = ctx.Visits.Where(e => e.UserID == ID);
        //            foreach (var visit in Visits)
        //            {
        //                var found = new Visited()
        //                {
        //                    ID = visit.ID,
        //                    EventID = visit.EventID,
        //                    BusinessID = visit.BusinessID,
        //                    Event = visit.Event,
        //                    Business = visit.Business,
        //                    UserID = visit.UserID,
        //                    AddToFavorites = visit.AddToFavorites,
        //                    AddToCalendar = visit.AddToCalendar,
        //                    Rating = visit.Rating,
        //                    Review = visit.Review,
        //                };
        //                results.Add(found);
        //            }
        //            return results;
        //        }
        //    }
        //    set { }

        //}
        //public virtual List<Visited> Favorites
        //{
        //    get
        //    {
        //        List<Visited> favorites = new List<Visited>();
        //        foreach (var visit in AllVisits)
        //        {
        //            if (visit.AddToFavorites == true)
        //            {
        //                var visitdetails = new Visited
        //                {
        //                    BusinessID = visit.BusinessID,
        //                };
        //                favorites.Add(visitdetails);
        //            }
        //        }

        //        return favorites;
        //    }
        //    set { }
        //}

        //public virtual List<Event> Events
        //{
        //    get
        //    {
        //        List<Event> events = new List<Event>();
        //        using (var ctx = new ApplicationDbContext())
        //        {
        //            foreach (var visit in AllVisits)
        //            {
        //                if (visit.AddToCalendar == true)
        //                {
        //                    var entity = ctx.Events.Single(e => e.id == visit.EventID);
        //                    var eventDetails = new Event()
        //                    {
        //                        id = entity.id,
        //                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.City}",
        //                        TypeOfEvent = entity.TypeOfEvent,
        //                        BusinessID = entity.BusinessID,
        //                        Business = entity.Business,
        //                        start = entity.start,
        //                        end = entity.end,
        //                        daysOfWeek = entity.daysOfWeek,
        //                        startRecur = entity.startRecur,
        //                        endRecur = entity.endRecur,
        //                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
        //                        color = entity.color,
        //                    };

        //                    events.Add(eventDetails);
        //                }
        //            }
        //        }


        //        return events;

        //    }
        //    set { }
        //}
    }
}
