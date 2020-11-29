using LocalSpirits.Data;
using LocalSpirits.Models.Event;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class EventService
    {
        private readonly Guid _userId;

        public EventService(Guid userId)
        {
            _userId = userId;
        }
        readonly List<EventDetail> searchResults = new List<EventDetail>();
        public string Create(EventCreate model)
        {
          
            var entity = new Event()
            {
                TypeOfEvent = $"{model.TypeOfEvent}",
                BusinessID = model.BusinessID,
                StartDay = model.StartDay,
                StartMonth = model.StartMonth,
                StartYear = model.StartYear,
                start = model.Start,
                end = model.Start,
                ThirdPartyWebsite = model.Url,
                color = model.Color,
                description = model.Description,
            };

            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    ctx.Events.Add(entity);
                    ctx.SaveChanges();
                    return "okay";
                }
                catch { }
                return "Error";
            }
        }
   
        public EventDetail GetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Events.Single(e => e.id == id);
                    return new EventDetail
                    {
                        id = entity.id,
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.Business.City.Name}",
                        TypeOfEvent = entity.TypeOfEvent,
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,                      
                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
                        StartDay = entity.StartDay,
                        StartMonth = entity.StartMonth,
                        StartYear = entity.StartYear,
                        color = entity.City,
                        Description = entity.description,
                    };
                }
                catch { }
                return new EventDetail();
            }
        }
        public EventDetail GetByBusinessIDAndStart(int id, string start)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Events.Single(e => e.BusinessID == id && e.StartDay == start);
                    return new EventDetail
                    {
                        id = entity.id,
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.Business.City.Name}",
                        TypeOfEvent = entity.TypeOfEvent,
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,                    
                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
                        color = entity.City,
                    };
                }
                catch { }
                return new EventDetail();
            }
        }

        public List<EventDetail> GetByCityName(string cityName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var events = ctx.Events.Where(e => e.Business.City.Name == cityName).ToList();
                foreach (var entity in events)
                {
                    var found = new EventDetail
                    {
                        id = entity.id,
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.City}",
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,
                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
                        color = entity.City,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<EventDetail> GetByStateName(string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var events = ctx.Events.Where(e => e.Business.City.State == stateName).ToList();
                foreach (var entity in events)
                {
                    var found = new EventDetail
                    {
                        id = entity.id,
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.City}",
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,                     
                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
                        color = entity.City,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<EventDetail> GetByZipCode(int zipCode)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var events = ctx.Events.Where(e => e.Business.ZipCode == zipCode).ToList();
                foreach (var entity in events)
                {
                    var found = new EventDetail
                    {
                        id = entity.id,
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.City}",
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,
                        ThirdPartyWebsite = entity.ThirdPartyWebsite,
                        color = entity.City,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public string Update(EventEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Events.Single(e => e.id == id);

                entity.id = model.ID;
                entity.BusinessID = model.BusinessID;
                entity.City = model.City;
                entity.start = model.Start;
                entity.end = model.Start;
                entity.StartDay = model.StartDay;
                entity.StartMonth = model.StartMonth;
                entity.StartYear = model.StartYear;
                entity.ThirdPartyWebsite = model.ThirdPartyWebsite;
                entity.color = model.Color;

                try
                {
                    ctx.SaveChanges();
                    return "Okay";
                }
                catch { }
                return "True";

            }
        }
        public bool Delete(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Events.Single(e => e.id == id);

                    ctx.Events.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
