using LocalSpirits.Data;
using LocalSpirits.Models.Event;
using LocalSpirits.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index(int id)
        {
            var businessService = new BusinessService();
            var model = businessService.GetByID(id);
            return View(model);
        }

        // CREATE
        public ActionResult CreateOne(int id)
        { 
            var model = new EventCreate();
            model.BusinessID = id;

            return View(model);
        }
        public ActionResult CreateRecurring(int id)
        {
            var model = new EventCreate();
            model.BusinessID = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOne(int id ,EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();
            string result = service.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Event was created.";
                return RedirectToAction($"Details/{model.BusinessID}", "Business");
            };
            ModelState.AddModelError("", "Event could not be created.");

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRecurring(int id, EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();
            string result = service.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Event was created.";
                return RedirectToAction($"Details/{model.BusinessID}", "Business");
            };
            ModelState.AddModelError("", "Event could not be created.");

            return View(model);
        }
        private EventService CreateService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService();
            return service;
        }

    }
}