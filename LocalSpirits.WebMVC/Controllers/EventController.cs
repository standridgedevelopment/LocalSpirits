using LocalSpirits.Data;
using LocalSpirits.Models.ActivityFeed;
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
        public ActionResult Create(int id)
        {
            var model = new EventCreate();
            model.BusinessID = id;
            ModelState.Clear();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var eventService = CreateEventService();
            var profileService = CreateProfileService();
            string result = eventService.Create(model);
            if (result == "okay")
            {
                var newEvent = eventService.GetByBusinessIDAndStart(model.BusinessID, model.StartDay);

                var activityFeedItem = new ActivityFeedCreate();
                activityFeedItem.Activity = TypeOfActivity.NewEvent;
                activityFeedItem.Content = $"{newEvent.title} on {model.StartMonth}/{model.StartDay}/{model.StartYear}";
                activityFeedItem.BusinessID = model.BusinessID;
                activityFeedItem.ObjectID = newEvent.id;
                activityFeedItem.ObjectType = "Event";
                profileService.CreateBusinessFeedItem(activityFeedItem);

                TempData["SaveResult"] = "Event was created.";
                return RedirectToAction($"Details/{model.BusinessID}", "Business");
            };
            ModelState.AddModelError("", "Event could not be created.");

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
        public ActionResult CreateRecurring(int id, EventCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var eventService = CreateEventService();
            string result = eventService.Create(model);
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
            var profileService = CreateProfileService();
            var thisEvent = eventService.GetByID(id);
            var foundVisit = visitedService.GetVisitByEventID(id);

            var activityFeedItem = new ActivityFeedCreate();
            activityFeedItem.Activity = TypeOfActivity.AddToCalendar;
            activityFeedItem.Content = $"{thisEvent.title}";
            activityFeedItem.ObjectID = thisEvent.id;
            activityFeedItem.ObjectType = "Event";

            if (foundVisit.EventID != null)
            {
                if (foundVisit.AddToCalendar == true)
                {
                    foundVisit.AddToCalendar = false;
                    profileService.RemoveFeedItem(activityFeedItem);

                    visitedService.UpdateEventVisit(foundVisit, id);
                    return RedirectToAction($"Details/{foundVisit.EventID}", "Event");
                }
                foundVisit.AddToCalendar = true;

               
                profileService.CreateFeedItem(activityFeedItem);

                visitedService.UpdateEventVisit(foundVisit, id);
                return RedirectToAction($"Details/{foundVisit.EventID}", "Event");
            }
            else
            {

                var model = new VisitedCreate();
                model.AddToCalendar = true;
                model.EventID = id;

                profileService.CreateFeedItem(activityFeedItem);
                visitedService.CreateVisit(model);
                return RedirectToAction($"Details/{id}", "Event");
            }
        }
        public ActionResult Details(int id)
        {
            var eventService = CreateEventService();
            var model = eventService.GetByID(id);
            ModelState.Clear();

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var eventService = CreateEventService();
            var detail = eventService.GetByID(id);
            var model =
                new EventEdit
                {
                    ID = detail.id,
                    City = detail.city,
                    BusinessID = detail.BusinessID,
                    ThirdPartyWebsite = detail.ThirdPartyWebsite,
                    Description = detail.Description,
                    StartDay = detail.StartDay,
                    StartMonth = detail.StartMonth,
                    StartYear = detail.StartYear,
                };
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var eventService = CreateEventService();

            string result = (eventService.Update(model, id));
            if (result == "Okay")
            {
                TempData["SaveResult"] = "Event updated!";
                return RedirectToAction($"Details/{model.BusinessID}", "Business");
            }
           
            else ModelState.AddModelError("", "Event could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var eventService = CreateEventService();
            var model = eventService.GetByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEvent(int id)
        {
            var eventService = CreateEventService();
            var profileService = CreateProfileService();
            var deleteFeedItem = new ActivityFeedCreate();

            var deletedEvent = eventService.GetByID(id);
            deleteFeedItem.ObjectType = "Event";
            deleteFeedItem.ObjectID = deletedEvent.id;
            eventService.Delete(id);
            profileService.RemoveBusinessFeedItem(deleteFeedItem);

            TempData["SaveResult"] = "Event was deleted";

            return RedirectToAction($"Details/{deletedEvent.BusinessID}", "Business");
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
        private ProfileServices CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }

    }
}