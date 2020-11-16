using LocalSpirits.Data;
using LocalSpirits.Models.Brewery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class BreweryService
    {
        readonly List<BreweryDetail> searchResults = new List<BreweryDetail>();
        public string CreatePark(BreweryCreate model)
        {
            var entity = new Brewery()
            {
                CityID = model.CityID,
                Name = model.Name,
                Acreage = model.Acreage,
                Hours = model.Hours,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website
            };

            using (var ctx = new ApplicationDbContext())
            {
                int result = 0;

                try
                {
                    var city = ctx.Cities.Single(e => e.ID == model.CityID);
                }
                catch
                {
                    if (entity.City == null) result += 1;
                }

                if (result == 1) return "Invalid City ID";

                try
                {
                    ctx.Parks.Add(entity);
                    ctx.SaveChanges();
                    return "Okay";
                }
                catch { }
                return "True";
            }
        }
        public IEnumerable<ParkListItem> GetParks()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Parks.Select
                    (e => new ParkListItem
                    {
                        Name = e.Name,
                        CityName = e.City.Name,
                        StateName = e.City.State.Name,
                        ID = e.ID,
                    }
                    );
                return query.ToArray();
            }

        }
        public ParkDetail GetParkByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Parks.Single(e => e.ID == id);
                    return new ParkDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        CityID = entity.CityID,
                        CityName = entity.City.Name,
                        StateID = entity.City.StateID,
                        StateName = entity.City.State.Name,
                        Acreage = entity.Acreage,
                        Hours = entity.Hours,
                        PhoneNumber = entity.PhoneNumber,
                        Website = entity.Website,
                        AverageTrailRating = entity.AverageTrailRating,
                        TrailsInPark = entity.TrailsInPark
                    };
                }
                catch { }
                return new ParkDetail();
            }
        }
        public List<ParkDetail> GetParkByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var parks = ctx.Parks.Where(e => e.Name.Contains(name)).ToList();
                foreach (var park in parks)
                {
                    var foundPark = new ParkDetail
                    {
                        ID = park.ID,
                        Name = park.Name,
                        CityID = park.CityID,
                        CityName = park.City.Name,
                        StateID = park.City.StateID,
                        StateName = park.City.State.Name,
                        Acreage = park.Acreage,
                        Hours = park.Hours,
                        PhoneNumber = park.PhoneNumber,
                        Website = park.Website,
                        AverageTrailRating = park.AverageTrailRating
                    };
                    searchResults.Add(foundPark);
                }
                return searchResults;
            }
        }
        public List<ParkDetail> GetParkByCityName(string cityName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var parks = ctx.Parks.Where(e => e.City.Name == cityName).ToList();
                foreach (var park in parks)
                {
                    var foundPark = new ParkDetail
                    {
                        ID = park.ID,
                        Name = park.Name,
                        CityID = park.CityID,
                        CityName = park.City.Name,
                        StateID = park.City.StateID,
                        StateName = park.City.State.Name,
                        Acreage = park.Acreage,
                        Hours = park.Hours,
                        PhoneNumber = park.PhoneNumber,
                        Website = park.Website,
                        AverageTrailRating = park.AverageTrailRating
                    };
                    searchResults.Add(foundPark);
                }
                return searchResults;
            }
        }
        public List<ParkDetail> GetParkByStateName(string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var parks = ctx.Parks.Where(e => e.City.State.Name == stateName).ToList();
                foreach (var park in parks)
                {
                    var foundPark = new ParkDetail
                    {
                        ID = park.ID,
                        Name = park.Name,
                        CityID = park.CityID,
                        CityName = park.City.Name,
                        StateID = park.City.StateID,
                        StateName = park.City.State.Name,
                        Acreage = park.Acreage,
                        Hours = park.Hours,
                        PhoneNumber = park.PhoneNumber,
                        Website = park.Website,
                        AverageTrailRating = park.AverageTrailRating
                    };
                    searchResults.Add(foundPark);
                }
                return searchResults;
            }
        }
        public List<ParkDetail> GetParkByAcreage(int minacreage, int maxacreage)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var parks = ctx.Parks.Where(e => e.Acreage >= minacreage && e.Acreage <= maxacreage).ToList();
                foreach (var park in parks)
                {
                    var foundPark = new ParkDetail
                    {
                        ID = park.ID,
                        Name = park.Name,
                        CityID = park.CityID,
                        CityName = park.City.Name,
                        StateID = park.City.StateID,
                        StateName = park.City.State.Name,
                        Acreage = park.Acreage,
                        Hours = park.Hours,
                        PhoneNumber = park.PhoneNumber,
                        Website = park.Website,
                        AverageTrailRating = park.AverageTrailRating
                    };
                    searchResults.Add(foundPark);
                }
            }

            return searchResults;
        }
        public string UpdatePark(ParkEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Parks.Single(e => e.ID == id);

                    if (model.Name != null) entity.Name = model.Name;
                    if (model.CityID != 0) entity.CityID = model.CityID;
                    if (model.Acreage != 0) entity.Acreage = model.Acreage;
                    if (model.Hours != null) entity.Hours = model.Hours;
                    if (model.PhoneNumber != null) entity.PhoneNumber = model.PhoneNumber;
                    if (model.Website != null) entity.Website = model.Website;

                    try
                    {
                        ctx.SaveChanges();
                        return "Okay";
                    }
                    catch
                    {
                        if (entity.City == null) return "Invalid City ID";
                    }
                }
                catch { }
                return "Update Error";
            }
        }
        public bool DeletePark(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Parks.Single(e => e.ID == id);

                    ctx.Parks.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
}
