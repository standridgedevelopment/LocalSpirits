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
        readonly List<CityDetail> searchResults = new List<CityDetail>();

        public bool CreateCities(CityCreate model)
        {
            var state = new State();
            using (var ctx = new ApplicationDbContext())
            {
                state = ctx.States.Single(e => e.Name.Contains(model.State));
            }
            var entity = new City()
            {
                Name = model.Name,
                StateID = state.ID
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
                        Name = e.Name,
                        State = e.State.Name
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
                        State = entity.State.Name
                    };
                }
                catch { }
                return new CityDetail();
            }
        }
        public List<CityDetail> GetCityByName(string name, int state)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cities = ctx.Cities.Where(e => e.Name.Contains(name) && e.State.Equals(state)).ToList();
                foreach (var city in cities)
                {
                    var foundCity = new CityDetail
                    {
                        ID = city.ID,
                        Name = city.Name,
                        State = city.State.Name
                    };
                    searchResults.Add(foundCity);
                }
                return searchResults;
            }
        }
        public List<CityDetail> GetCityByState(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var cities = ctx.Cities.Where(e => e.State.Name.Contains(name)).ToList();
                foreach (var city in cities)
                {
                    var foundCity = new CityDetail
                    {
                        ID = city.ID,
                        Name = city.Name,
                        State = city.State.Name
                    };
                    searchResults.Add(foundCity);
                }
                return searchResults;
            }
        }
        public bool UpdateCity(CityEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var state = ctx.States.Single(e => e.Name.Contains(model.State));
                    var entity = ctx.Cities.Single(e => e.ID == id);

                    entity.Name = model.Name;
                    entity.StateID = state.ID;
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
