using LocalSpirits.Data;
using LocalSpirits.Models.Brewery;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class BreweryService
    {
        readonly List<BreweryListItem> searchResults = new List<BreweryListItem>();
        public string Create(BreweryCreate model)
        {
            var service = new CityService();
            var foundCity = service.GetCityByName(model.City, model.State);

            if (foundCity == null) return "invalid city";
            var entity = new Brewery()
            {
                CityID = foundCity.ID,
                Name = model.Name,
                State = $"{model.State}",
                ZipCode = model.ZipCode,
                Hours = model.Hours,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website,
                LiveMusic = model.LiveMusic
            };

            using (var ctx = new ApplicationDbContext())
            {

 
                try
                {
                    ctx.Breweries.Add(entity);
                    ctx.SaveChanges();
                    return "okay";
                }
                catch { }
                return "True";
            }
        }
        public IEnumerable<BreweryListItem> GetAll()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Breweries.Select
                    (e => new BreweryListItem
                    {
                        Name = e.Name,
                        City = e.City.Name,
                        State = e.State,
                        ID = e.ID,
                        ZipCode = e.ZipCode
                    }
                    );
                return query.ToArray();
            }

        }
        public BreweryDetail GetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Breweries.Single(e => e.ID == id);
                    return new BreweryDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        City = entity.City.Name,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Hours = entity.Hours,
                        PhoneNumber = entity.PhoneNumber,
                        Website = entity.Website,
                        Rating = entity.Rating,
                        LiveMusic = entity.LiveMusic
                    };
                }
                catch { }
                return new BreweryDetail();
            }
        }
        public List<BreweryListItem> GetByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var breweries = ctx.Breweries.Where(e => e.Name.Contains(name)).ToList();
                foreach (var brewery in breweries)
                {
                    var found = new BreweryListItem
                    {
                        ID = brewery.ID,
                        Name = brewery.Name,
                        Rating = brewery.Rating,
                        City = brewery.City.Name,
                        State = brewery.State,
                        ZipCode = brewery.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BreweryListItem> GetByCityName(string cityName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var breweries = ctx.Breweries.Where(e => e.City.Name == cityName).ToList();
                foreach (var brewery in breweries)
                {
                    var found = new BreweryListItem
                    {
                        ID = brewery.ID,
                        Name = brewery.Name,
                        Rating = brewery.Rating,
                        City = brewery.City.Name,
                        State = brewery.State,
                        ZipCode = brewery.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BreweryListItem> GetByStateName(string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var breweries = ctx.Breweries.Where(e => e.City.State == stateName).ToList();
                foreach (var brewery in breweries)
                {
                    var found = new BreweryListItem
                    {
                        ID = brewery.ID,
                        Name = brewery.Name,
                        Rating = brewery.Rating,
                        City = brewery.City.Name,
                        State = brewery.State,
                        ZipCode = brewery.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BreweryListItem> GetByZipCode(int zipCode)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var breweries = ctx.Breweries.Where(e => e.ZipCode == zipCode).ToList();
                foreach (var brewery in breweries)
                {
                    var found = new BreweryListItem
                    {
                        ID = brewery.ID,
                        Name = brewery.Name,
                        Rating = brewery.Rating,
                        City = brewery.City.Name,
                        State = brewery.State,
                        ZipCode = brewery.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public string Update(BreweryEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var service = new CityService();
                var foundCity = service.GetCityByName(model.City, model.State);
                if (foundCity == null) return "invalid city";

                var entity = ctx.Breweries.Single(e => e.ID == id);

                entity.Name = model.Name;
                entity.CityID = foundCity.ID;
                entity.State = $"{model.State}";
                entity.ZipCode = model.ZipCode;
                entity.Hours = model.Hours;
                entity.PhoneNumber = model.PhoneNumber;
                entity.Website = model.Website;
                entity.LiveMusic = model.LiveMusic;

               

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
                    var entity = ctx.Breweries.Single(e => e.ID == id);

                    ctx.Breweries.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
