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
                start = model.Start,
                end = model.End,
                DaysOfWeekInput = model.DaysOfWeek,
                startRecur = model.StartRecur,
                endRecur = model.EndRecur,
                ThirdPartyWebsite = model.Url,
                color = model.Color
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
                        title = $"{entity.TypeOfEvent} at {entity.Business.Name}, {entity.City}",
                        TypeOfEvent = entity.TypeOfEvent,
                        Business = entity.Business.Name,
                        BusinessID = entity.BusinessID,
                        city = entity.Business.City.Name,
                        state = entity.Business.City.State,
                        start = entity.start,
                        end = entity.end,
                        daysOfWeek = entity.DaysOfWeekInput,
                        startRecur = entity.startRecur,
                        endRecur = entity.endRecur,
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
                        daysOfWeek = entity.DaysOfWeekInput,
                        startRecur = entity.startRecur,
                        endRecur = entity.endRecur,
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
                        daysOfWeek = entity.DaysOfWeekInput,
                        startRecur = entity.startRecur,
                        endRecur = entity.endRecur,
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
                        daysOfWeek = entity.DaysOfWeekInput,
                        startRecur = entity.startRecur,
                        endRecur = entity.endRecur,
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
            var service = new BusinessService();
            var foundBusiness = service.GetByNameAndCity(model.BusinessName, model.City);

            if (foundBusiness == null) return "invalid city";

            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Events.Single(e => e.id == id);

                entity.id = model.ID;
                entity.title = model.Title;
                entity.BusinessID = foundBusiness.ID;
                entity.City = model.City;
                entity.start = model.Start;
                entity.end = model.End;
                entity.daysOfWeek = model.DaysOfWeekConverted;
                entity.startRecur = model.StartRecur;
                entity.endRecur = model.EndRecur;
                entity.url = model.Url;
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
