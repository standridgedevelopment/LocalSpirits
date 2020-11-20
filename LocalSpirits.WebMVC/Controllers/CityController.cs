using LocalSpirits.Data;
using LocalSpirits.Models.Business;
using LocalSpirits.Models.City;
using LocalSpirits.Models.Profile;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        public ActionResult Index()
        {
            var service = CreateService();
            var State = new CityByState();
            State.State = "Search by City or State";
            return View(State);
        }
        [HttpPost]
        public ActionResult Index(string id ,CityByState city)
        {
            var service = CreateService();
            bool parseResult = Enum.TryParse($"{id}", out StateName state);

            if (parseResult == false) city.StateResult = city.AbreviateState(id);
            if (parseResult) city.StateResult = state;

            if (city.StateResult == null)
            {
                var searchByCity = service.GetCitiesByName(id);
                if (searchByCity.Count == 1)
                    return RedirectToAction($"Details/{searchByCity[0].ID}");
                if (searchByCity.Count != 0)
                    return RedirectToAction($"CityResults/{id}");
            } 
            return RedirectToAction($"State/{city.StateResult}") ;
        }

        // OLD!!!!!!
        //public ActionResult State(string id)
        //{
        //    var service = CreateService();
        //    var model = service.GetCitiesByState(id);
        //    ModelState.Clear();
        //    return View(model);
        //}
        public ActionResult State(string id)
        {
            var service = CreateService();
            var businessService = new BusinessService();
            var model = businessService.GetByStateName(id);
            ModelState.Clear();
            return View(model);
        }

        public ActionResult CityResults(string id, CityByState city)
        {
            if (id != null)
            {
                var service = CreateService();
                var model = service.GetCitiesByName(id);
                ModelState.Clear();
                return View(model);
            }
            //string state =  $"{city.State}"
            else return RedirectToAction($"State/{city.State}");
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();

            if (service.CreateCity(model))
            {
                TempData["SaveResult"] = "City was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "City could not be created.");

            return View(model);
        }
        public ActionResult Details(int id)
        {
            var businessService = new BusinessService();
            var service = CreateService();
            var city = service.GetCityByID(id);
            var model = businessService.GetByCityName(city.Name, city.State);
            if (model.Count() == 0)
            {
                var baseBusiness = new BusinessListItem();
                baseBusiness.City = city.Name;
                model.Add(baseBusiness);
            }
            ModelState.Clear();

            return View(model);
        }

        // Old!!!
        //public ActionResult Details(int id)
        //{
        //    var service = CreateService();
        //    var model = service.GetCityByID(id);

        //    return View(model);
        //}
        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var detail = service.GetCityByID(id);
            var model =
                new CityEdit
                {
                    ID = detail.ID,
                    Name = detail.Name,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

            if (service.UpdateCity(model))
            {
                TempData["SaveResult"] = "City was updated.";
                return RedirectToAction($"State/{model.State}");
            }

            ModelState.AddModelError("", "City could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            var model = service.GetCityByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCity(int id)
        {
            var service = CreateService();

            service.DeleteCity(id);

            TempData["SaveResult"] = "City was deleted";

            return RedirectToAction("Index");
        }
        private CityService CreateService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService();
            return service;
        }
 
    }
}
