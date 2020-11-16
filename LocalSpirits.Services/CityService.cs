using LocalSpirits.Data;
using LocalSpirits.Models.City;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class CityService
    {
        readonly List<CityListItem> searchResults = new List<CityListItem>();

        public bool CreateCity(CityCreate model)
        {
            var state = new State();
            var entity = new City()
            {
                Name = model.Name,
                State = $"{model.State}"
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cities.Add(entity);
                //var testing = ctx.SaveChanges();
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CityListItem> GetCities()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Cities.Select
                    (e => new CityListItem
                    {
                        ID = e.ID,
                        Name = e.Name,
                        State = e.State
                    }
                    );
                return query.ToArray();
            }

        }
        public CityDetail GetCityByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Cities.Single(e => e.ID == id);
                    return new CityDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        State = entity.State
                    };
                }
                catch { }
                return new CityDetail();
            }
        }
        public List<CityListItem> GetCityByName(string cityName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cities = ctx.Cities.Where(e => e.Name.Contains(cityName)).ToList();
                foreach (var city in cities)
                {
                    var foundCity = new CityListItem
                    {
                        ID = city.ID,
                        Name = city.Name,
                        State = city.State
                    };
                    searchResults.Add(foundCity);
                }
                return searchResults;
            }
        }
        public List<CityListItem> GetCitiesByState(string state)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cities = ctx.Cities.Where(e => e.State.Equals(state)).ToList();
                foreach (var city in cities)
                {
                    var foundCity = new CityListItem
                    {
                        ID = city.ID,
                        Name = city.Name,
                        State = city.State
                    };
                    searchResults.Add(foundCity);
                }
                return searchResults;
            }
        }
        public bool UpdateCity(CityEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Cities.Single(e => e.ID == model.ID);

                    entity.Name = model.Name;
                    entity.State = model.State;
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCity(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Cities.Single(e => e.ID == id);

                    ctx.Cities.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
