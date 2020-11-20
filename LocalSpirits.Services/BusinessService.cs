using LocalSpirits.Data;
using LocalSpirits.Models.Business;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class BusinessService
    {
        readonly List<BusinessListItem> searchResults = new List<BusinessListItem>();
        public string Create(BusinessCreate model)
        {
            var service = new CityService();
            var foundCity = service.GetCityByName(model.City, model.State);

            if (foundCity == null) return "invalid city";
            var entity = new Business()
            {
                CityID = foundCity.ID,
                TypeOfEstablishment = $"{model.typeOfEstablishment}",
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
                    ctx.Businesses.Add(entity);
                    ctx.SaveChanges();
                    return "okay";
                }
                catch { }
                return "True";
            }
        }
        public IEnumerable<BusinessListItem> GetAll()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Businesses.Select
                    (e => new BusinessListItem
                    {
                        Name = e.Name,
                        TypeOfEstablishment = e.TypeOfEstablishment,
                        City = e.City.Name,
                        State = e.State,
                        ID = e.ID,
                        ZipCode = e.ZipCode
                    }
                    );
                return query.ToArray();
            }

        }
        public BusinessDetail GetByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var entity = ctx.Businesses.Single(e => e.ID == id);
                    return new BusinessDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        TypeOfEstablishment = entity.TypeOfEstablishment,
                        City = entity.City.Name,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Hours = entity.Hours,
                        PhoneNumber = entity.PhoneNumber,
                        Website = entity.Website,
                        Rating = entity.Rating,
                        Events = entity.Events
                        
                    };
                }
                catch { }
                return new BusinessDetail();
            }
        }
        public List<BusinessListItem> GetByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Businessess = ctx.Businesses.Where(e => e.Name.Contains(name)).ToList();
                foreach (var business in Businessess)
                {
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        State = business.State,
                        ZipCode = business.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }

        public Business GetByNameAndCity(string name, string city)
        {
            var business = new Business();
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    business = ctx.Businesses.Single(e => e.Name.Contains(name) && e.City.Name == city);

                }
                catch { return null; }
                return business;
            }
        }

        public List<BusinessListItem> GetByCityName(string cityName, string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Businessess = ctx.Businesses.Where(e => e.City.Name == cityName && e.City.State == stateName).ToList();
                foreach (var business in Businessess)
                {
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        State = business.State,
                        ZipCode = business.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BusinessListItem> GetByStateName(string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Businessess = ctx.Businesses.Where(e => e.City.State == stateName).ToList();
                foreach (var business in Businessess)
                {
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        State = business.State,
                        ZipCode = business.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BusinessListItem> GetByZipCode(int zipCode)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Businessess = ctx.Businesses.Where(e => e.ZipCode == zipCode).ToList();
                foreach (var business in Businessess)
                {
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.Name,
                        Rating = business.Rating,
                        City = business.City.Name,
                        State = business.State,
                        ZipCode = business.ZipCode,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public string Update(BusinessEdit model, int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var service = new CityService();
                var foundCity = service.GetCityByName(model.City, model.State);
                if (foundCity == null) return "invalid city";

                var entity = ctx.Businesses.Single(e => e.ID == id);

                entity.ID = model.ID;
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
                    var entity = ctx.Businesses.Single(e => e.ID == id);

                    ctx.Businesses.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
