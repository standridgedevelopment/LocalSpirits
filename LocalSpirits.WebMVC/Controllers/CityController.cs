using LocalSpirits.Data;
using LocalSpirits.Models.City;
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
            var model = new CityByState();
            return View(model);
        }

        public ActionResult State(CityByState id)
        {
            //var state = $"{id.State}";
            var service = CreateService();
            var model = service.GetCitiesByState(id.State);
            return View(model);
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

        private CityService CreateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CityService();
            return service;
        }
    }
}