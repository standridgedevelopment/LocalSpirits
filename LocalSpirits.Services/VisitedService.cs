using LocalSpirits.Data;
using LocalSpirits.Models.ActivityFeed;
using LocalSpirits.Models.Visited;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class VisitedService
    {
        private readonly Guid _userId;

        public VisitedService(Guid userId)
        {
            _userId = userId;
        }
        public string CreateVisit(VisitedCreate model)
        {
            var entity = new Visited()
            {
                Profile_ID = _userId,
                BusinessID = model.BusinessID,
                EventID = model.EventID,
                Rating = model.Rating,
                Review = model.Review,
                AddToFavorites = model.AddToFavorites,
                AddToCalendar = model.AddToCalendar,
            };

            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    ctx.Visits.Add(entity);
                    ctx.SaveChanges();
                    return "Okay";
                }
                catch
                { }
                return "Error";
            }
        }
        public VisitedDetail GetVisitByEventID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Visits.Single(e => e.EventID == id && e.Profile_ID == _userId);
                    return new VisitedDetail
                    {
                        EventID = entity.EventID,
                        BusinessID = entity.Event.BusinessID,
                        AddToCalendar = entity.AddToCalendar,
                        Rating = entity.Rating,
                        Review = entity.Review,
                        AddToFavorites = entity.AddToFavorites
                    };
                }
                catch { }
                return new VisitedDetail();
            }
        }
        public VisitedDetail GetVisitByBusinessID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Visits.Single(e => e.BusinessID == id && e.Profile_ID == _userId);
                    return new VisitedDetail
                    {
                        EventID = entity.EventID,
                        BusinessID = entity.BusinessID,
                        AddToCalendar = entity.AddToCalendar,
                        Rating = entity.Rating,
                        Review = entity.Review,
                        AddToFavorites = entity.AddToFavorites
                    };
                }
                catch { }
                return new VisitedDetail();
            }
        }
        public string UpdateEventVisit(VisitedDetail model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Visits.Single(e => e.EventID == id && e.Profile_ID == _userId);

                    entity.Rating = model.Rating;
                    entity.Rating = model.Rating;
                    entity.AddToCalendar = model.AddToCalendar;
                    entity.AddToFavorites = model.AddToFavorites;

                    
                    try
                    {
                        ctx.SaveChanges();
                        return "Okay";
                    }
                    catch { }
                }
                catch { }

                return "Update Error";
            }
        }
        public string UpdateBusinessFollow(VisitedDetail model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Visits.Single(e => e.BusinessID == id && e.Profile_ID == _userId);

                    entity.Rating = model.Rating;
                    entity.Rating = model.Rating;
                    entity.AddToCalendar = model.AddToCalendar;
                    entity.AddToFavorites = model.AddToFavorites;


                    try
                    {
                        ctx.SaveChanges();
                        return "Okay";
                    }
                    catch { }
                }
                catch { }

                return "Update Error";
            }
        }
    }
}
