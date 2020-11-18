using LocalSpirits.Data;
using LocalSpirits.Models.Beer;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class BeerService
    {
        readonly List<BeerListItem> searchResults = new List<BeerListItem>();
        public string Create(BeerCreate model)
        {
            var service = new BusinessService();
            var foundBusiness = service.GetByNameAndCity(model.Brewery, model.BreweryCity);

            if (foundBusiness == null) return "invalid city";

            var entity = new Beer()
            {
                Name = model.Name,
                BreweryID = foundBusiness.ID,
                Aroma = model.Aroma,
                Taste = model.Taste,
                ABV = model.ABV,
                Hops = model.Hops,
                Package = model.Package,
                KegsAvailable = model.KegsAvailable,
                Availability = model.Availability,
                Website = model.Website
            };

            using (var ctx = new ApplicationDbContext())
            {


                try
                {
                    ctx.Beers.Add(entity);
                    ctx.SaveChanges();
                    return "okay";
                }
                catch { }
                return "Error";
            }
        }
        public IEnumerable<BeerListItem> GetAll()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Beers.Select
                    (e => new BeerListItem
                    {
                        ID = e.ID,
                        Brewery = e.Business.Name,
                        Name = e.Name,
                        Rating = e.Rating,
                        Website = e.Website,
                    }
                    );
                return query.ToArray();
            }

        }
        public BeerDetail GetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Beers.Single(e => e.ID == id);
                    return new BeerDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        Brewery = entity.Business.Name,
                        Aroma = entity.Aroma,
                        Taste = entity.Taste,
                        ABV = entity.ABV,
                        Package = entity.Package,
                        KegsAvailable = entity.KegsAvailable,
                        Availability = entity.Availability,
                        Website = entity.Website,
                        Rating = entity.Rating,
                    };
                }
                catch { }
                return new BeerDetail();
            }
        }
        public List<BeerListItem> GetByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var beers = ctx.Beers.Where(e => e.Name.Contains(name)).ToList();
                foreach (var beer in beers)
                {
                    var found = new BeerListItem
                    {
                        ID = beer.ID,
                        Brewery = beer.Business.Name,
                        Name = beer.Name,
                        Rating = beer.Rating,
                        Website = beer.Website,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BeerListItem> GetByCityName(string cityName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var beers = ctx.Beers.Where(e => e.Business.City.Name == cityName).ToList();
                foreach (var beer in beers)
                {
                    var found = new BeerListItem
                    {
                        ID = beer.ID,
                        Brewery = beer.Business.Name,
                        Name = beer.Name,
                        Rating = beer.Rating,
                        Website = beer.Website,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BeerListItem> GetByStateName(string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var beers = ctx.Beers.Where(e => e.Business.City.State == stateName).ToList();
                foreach (var beer in beers)
                {
                    var found = new BeerListItem
                    {
                        ID = beer.ID,
                        Brewery = beer.Business.Name,
                        Name = beer.Name,
                        Rating = beer.Rating,
                        Website = beer.Website,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BeerListItem> GetByZipCode(int zipCode)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var beers = ctx.Beers.Where(e => e.Business.ZipCode == zipCode).ToList();
                foreach (var beer in beers)
                {
                    var found = new BeerListItem
                    {
                        ID = beer.ID,
                        Brewery = beer.Business.Name,
                        Name = beer.Name,
                        Rating = beer.Rating,
                        Website = beer.Website,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public string Update(BeerEdit model, int id)
        {
            var service = new BusinessService();
            var foundBusiness = service.GetByNameAndCity(model.Brewery, model.BreweryCity);

            if (foundBusiness == null) return "invalid city";

            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Beers.Single(e => e.ID == id);

                entity.ID = model.ID;
                entity.Name = model.Name;
                entity.BreweryID = foundBusiness.ID;
                entity.Aroma = model.Aroma;
                entity.Taste = model.Taste;
                entity.ABV = model.ABV;
                entity.Hops = model.Hops;
                entity.Package = model.Package;
                entity.KegsAvailable = model.KegsAvailable;
                entity.Availability = model.Availability;
                entity.Website = model.Website;

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
                    var entity = ctx.Beers.Single(e => e.ID == id);

                    ctx.Beers.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
