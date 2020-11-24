using LocalSpirits.Data;
using LocalSpirits.Models.Profile;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class ProfileServices
    {
        private readonly Guid _userId;

        public ProfileServices(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateUser(ProfileCreate model)
        {
            var entity = new Profile()
            {
                ID = _userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = (Data.StateName)model.State,
                ZipCode = model.ZipCode
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Profiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public ProfileDetail GetProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);

                    List<Data.Visited> favorites = new List<Data.Visited>();
                    foreach (var visit in entity.AllVisits)
                    {
                        if (visit.AddToFavorites == true)
                        {
                            var visitdetails = new Data.Visited
                            {
                                BusinessID = visit.BusinessID,
                            };
                            favorites.Add(visitdetails);
                        }
                    }

                    List<Data.Event> events = new List<Data.Event>();
                    foreach (var visit in entity.AllVisits)
                    {
                        if (visit.AddToCalendar == true)
                        {
                            var visitdetails = new Data.Event
                            {
                                id = visit.Event.id,
                                title = $"{visit.Event.TypeOfEvent} at {visit.Event.Business.Name}, {visit.Event.City}",
                                TypeOfEvent = visit.Event.TypeOfEvent,
                                BusinessID = visit.Event.BusinessID,
                                Business = visit.Event.Business,
                                start = visit.Event.start,
                                end = visit.Event.end,
                                daysOfWeek = visit.Event.DaysOfWeekConverted.ToArray(),
                                startRecur = visit.Event.startRecur,
                                endRecur = visit.Event.endRecur,
                                ThirdPartyWebsite = visit.Event.ThirdPartyWebsite,
                                color = visit.Event.color,
                            };
                            events.Add(visitdetails);
                        }
                    }

                    return new ProfileDetail
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Favorites = favorites,
                        Events = events,
                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }
        public bool UpdateProfile(ProfileEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity = ctx.Profiles.Single(e => e.ID == _userId);

                if (model.FirstName != null) entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.City = model.City;
                entity.State = model.State;
                entity.ZipCode = model.ZipCode;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Profiles.Single(e => e.ID == _userId);

                ctx.Profiles.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
