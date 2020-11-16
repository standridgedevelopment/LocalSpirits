using LocalSpirits.Data;
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
        [ValidateAntiForgeryToken]
        public ActionResult Index(CityByState city)
        {
            var service = CreateService();
            bool parseResult = Enum.TryParse($"{city.State}", out StateName state);

            if (parseResult == false) city.StateResult = city.AbreviateState(city.State);
            if (parseResult) city.StateResult = state;

            if (city.StateResult == null)
            {
                var searchByCity = service.GetCitiesByName(city.State);
                if (searchByCity.Count != 0)
                    return RedirectToAction($"CityResults/{city.State}");
            } 
            return RedirectToAction($"State/{city.StateResult}") ;
        }

        public ActionResult State(string id, CityByState city)
        {
            var service = CreateService();
            var model = service.GetCitiesByState(id);
            return View(model);
        }

        public ActionResult CityResults(string id, CityByState city)
        {
            if (id != null)
            {
                var service = CreateService();
                var model = service.GetCitiesByName(id);
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
            var service = CreateService();
            var model = service.GetCityByID(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var detail = service.GetCityByID(id);
            var model =
                new CityEdit
                {
                    ID = detail.ID,
                    Name = detail.Name,
                    State = detail.State
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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService();
            return service;
        }
    }
}