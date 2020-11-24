using LocalSpirits.Data;
using LocalSpirits.Models.Event;
using LocalSpirits.Models.Visited;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
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
        public ActionResult CreateOne(int id, EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateEventService();
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

            var service = CreateEventService();
            string result = service.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Event was created.";
                return RedirectToAction($"Details/{model.BusinessID}", "Business");
            };
            ModelState.AddModelError("", "Event could not be created.");

            return View(model);
        }

        public ActionResult CheckIfOnCalendar(int id)
        {
            var eventService = CreateEventService();
            var visitedService = CreateVisitedService();
            var thisEvent = eventService.GetByID(id);
            var foundVisit = visitedService.GetVisitByEventID(id);
            ModelState.Clear();
            if (foundVisit.AddToCalendar == true)
                return View("OnCalendar", thisEvent);
            return View("NotOnCalendar", thisEvent);
        }
        public ActionResult AddToProfileCalendar(int id)
        {
            var eventService = CreateEventService();
            var visitedService = CreateVisitedService();
            var thisEvent = eventService.GetByID(id);
            var foundVisit = visitedService.GetVisitByEventID(id);

            if (foundVisit.EventID != null)
            {
                if (foundVisit.AddToCalendar == true)
                {
                    foundVisit.AddToCalendar = false;
                    visitedService.UpdateEventVisit(foundVisit, id);
                    return RedirectToAction($"Details/{foundVisit.EventID}", "Event");
                }
                foundVisit.AddToCalendar = true;
                visitedService.UpdateEventVisit(foundVisit, id);
                return RedirectToAction($"Details/{foundVisit.EventID}", "Event");
            }
            else
            {

                var model = new VisitedCreate();
                model.AddToCalendar = true;
                model.EventID = id;
                visitedService.CreateVisit(model);
                return RedirectToAction($"Details/{id}", "Event");
            }
        }
        public ActionResult Details(int id)
        {
            var service = CreateEventService();
            var model = service.GetByID(id);
            ModelState.Clear();

            return View(model);
        }
        private VisitedService CreateVisitedService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new VisitedService(userId);
            return service;
        }
        private EventService CreateEventService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new EventService(userId);
            return service;
        }

    }
}