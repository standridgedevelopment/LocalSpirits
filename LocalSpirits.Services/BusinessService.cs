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
            var entity = new Business()
            {
                CityID = model.CityID,
                TypeOfEstablishment = $"{model.typeOfEstablishment}",
                Name = model.Name,
                ZipCode = model.ZipCode,
                Hours = model.Hours,
                PhoneNumber = model.PhoneNumber,
                Website = model.Website,
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
                        State = e.City.State,
                        ID = e.ID,
                        ZipCode = e.ZipCode,
                        Events = e.Events,
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
                    var ratings = new List<Visited>();
                    if (entity.AllVisits != null)
                    {

                        foreach (var rating in entity.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    return new BusinessDetail
                    {
                        ID = entity.ID,
                        Name = entity.Name,
                        TypeOfEstablishment = entity.TypeOfEstablishment,
                        City = entity.City.Name,
                        CityID = entity.CityID,
                        State = entity.City.State,
                        ZipCode = entity.ZipCode,
                        Hours = entity.Hours,
                        PhoneNumber = entity.PhoneNumber,
                        Website = entity.Website,
                        Events = entity.Events,
                        Ratings = ratings,
                        ReviewFromUser = false,
                  
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
                var ratings = new List<Visited>();
                var Businessess = ctx.Businesses.Where(e => e.Name.Contains(name)).ToList();
                foreach (var business in Businessess)
                {
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,
                        Ratings = ratings,
                       
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }


        public List<BusinessListItem> GetByCityName(string cityName, string stateName)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Businessess = ctx.Businesses.Where(e => e.City.Name == cityName && e.City.State == stateName).ToList();
                var ratings = new List<Visited>();
                foreach (var business in Businessess)
                {
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,
                        Ratings = ratings,
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
                var ratings = new List<Visited>();
                var Businessess = ctx.Businesses.Where(e => e.City.State == stateName).ToList();
                foreach (var business in Businessess)
                {
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,     
                        Ratings = ratings,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BusinessListItem> GetByStateAndType(string stateName, string type)
        {
            using (var ctx = new ApplicationDbContext())
            {
                
                var Businesses = new List<Business>();
                Businesses = ctx.Businesses.Where(e => e.City.State == stateName).ToList();
                if (type != null && type.Contains("Brewery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.State == stateName && e.TypeOfEstablishment == "Brewery").ToList();

                }
                if (type != null && type.Contains("Winery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.State == stateName && e.TypeOfEstablishment == "Winery").ToList();

                }
                if (type != null && type.Contains("Distillery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.State == stateName && e.TypeOfEstablishment == "Distillery").ToList();

                }
                if (type != null && type.Contains("Bar"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.State == stateName && e.TypeOfEstablishment == "Bar").ToList();

                }

                foreach (var business in Businesses)
                {
                    var ratings = new List<Visited>();
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,
                        Ratings = ratings,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }
        public List<BusinessListItem> GetByCityAndType(int id, string type)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var Businesses = new List<Business>();
                Businesses = ctx.Businesses.Where(e => e.City.ID == id).ToList();
                if (type != null && type.Contains("Brewery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.ID == id && e.TypeOfEstablishment == "Brewery").ToList();

                }
                if (type != null && type.Contains("Winery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.ID == id && e.TypeOfEstablishment == "Winery").ToList();

                }
                if (type != null && type.Contains("Distillery"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.ID == id && e.TypeOfEstablishment == "Distillery").ToList();

                }
                if (type != null && type.Contains("Bar"))
                {
                    Businesses = ctx.Businesses.Where(e => e.City.ID == id && e.TypeOfEstablishment == "Bar").ToList();

                }

                foreach (var business in Businesses)
                {
                    var ratings = new List<Visited>();
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,
                        Ratings = ratings,
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
                var ratings = new List<Visited>();
                var Businessess = ctx.Businesses.Where(e => e.ZipCode == zipCode).ToList();
                foreach (var business in Businessess)
                {
                    if (business.AllVisits != null)
                    {

                        foreach (var rating in business.AllVisits)
                        {
                            if (rating.Rating > 0)
                                ratings.Add(rating);
                        }

                    }
                    var found = new BusinessListItem
                    {
                        ID = business.ID,
                        Name = business.Name,
                        TypeOfEstablishment = business.TypeOfEstablishment,
                        Rating = business.Rating,
                        City = business.City.Name,
                        CityID = business.CityID,
                        State = business.City.State,
                        ZipCode = business.ZipCode,
                        Events = business.Events,   
                        Ratings = ratings,
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
                entity.ZipCode = model.ZipCode;
                entity.Hours = model.Hours;
                entity.PhoneNumber = model.PhoneNumber;
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
                    var entity = ctx.Businesses.Single(e => e.ID == id);

                    ctx.Businesses.Remove(entity);
                }
                catch { return false; }
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
