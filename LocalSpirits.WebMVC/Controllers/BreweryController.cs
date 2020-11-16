using LocalSpirits.Models.Brewery;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class BreweryController : Controller
    {
        // GET: Brewery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BreweryCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();
            string result = service.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Brewery was created.";
                return RedirectToAction("Index");
            };
            if (result == "invalid city")
                ModelState.AddModelError("", $"{model.City} could not be found.");
            else ModelState.AddModelError("", "Brewery could not be created.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var detail = service.GetByID(id);
            var model =
                new BreweryEdit
                {
                    ID = detail.ID,
                    Name = detail.Name,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode,
                    Hours = detail.Hours,
                    PhoneNumber = detail.PhoneNumber,
                    Website = detail.PhoneNumber,
                    LiveMusic = detail.LiveMusic,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BreweryEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

            string result = (service.Update(model, id));
            if (result == "okay")
            {
                TempData["SaveResult"] = "City was updated.";
                return RedirectToAction($"State/{model.State}");
            }
            if (result == "invalid city")
                ModelState.AddModelError("", $"{model.City} could not be found.");
            else ModelState.AddModelError("", "Brewery could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            var model = service.GetByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBrewery(int id)
        {
            var service = CreateService();

            service.Delete(id);

            TempData["SaveResult"] = "City was deleted";

            return RedirectToAction("Index");
        }
        private BreweryService CreateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BreweryService();
            return service;
        }
    }
}