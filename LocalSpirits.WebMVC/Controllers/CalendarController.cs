﻿using LocalSpirits.Data;
using LocalSpirits.Models.Business;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetCityEvents(int id, string type, string eventType, DateTime start, DateTime end)
        {
            var businessService = new BusinessService();
            var cityService = new CityService();
            var Businessess = businessService.GetByCityAndType(id, type, eventType);
            var events = new List<Event>();

            foreach (var business in Businessess)
            {
                foreach (var cEvent in business.Events)
                {
                    if (cEvent.start != null)
                    {
                        events.Add(new Event()
                        {
                            id = cEvent.id,
                            title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                            start = cEvent.start,
                            end = cEvent.end,
                            color = cEvent.color,
                            url = cEvent.url,

                        });
                    }
                }  
            }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBusinessEvents(int id, string eventType, DateTime start, DateTime end)
        {
            var businessService = new BusinessService();
            var business = businessService.GetByIDAndEventType(id, eventType);
            var events = new List<Event>();

            foreach (var cEvent in business.Events)
            {
                if (cEvent.start != null)
                {
                    events.Add(new Event()
                    {
                        id = cEvent.id,
                        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                        start = cEvent.start,
                        end = cEvent.end,
                        color = cEvent.color,
                        url = cEvent.url,

                    });
                }
                //if (cEvent.startRecur != null)
                //{
                //    events.Add(new Event()
                //    {
                //        id = cEvent.id,
                //        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                //        startRecur = cEvent.startRecur,
                //        endRecur = cEvent.endRecur,
                //        daysOfWeek = cEvent.DaysOfWeekConverted.ToArray(),
                //        color = cEvent.color,
                //        //startRecur = $"2020-11-22",

                //    });
                //}
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPersonalEvents(string id, DateTime start, DateTime end)
        {
            var profileService = CreateProfileService();
            var profile = profileService.GetFullProfile();
            var events = new List<Event>();

            foreach (var cEvent in profile.Events)
                {
                    if (cEvent.start != "--")
                    {
                        events.Add(new Event()
                        {
                            id = cEvent.id,
                            title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                            start = cEvent.start,
                            end = cEvent.end,
                            color = cEvent.color,
                            url = cEvent.url,

                        });
                    }
                    //if (cEvent.startRecur != "--")
                    //{
                    //    events.Add(new Event()
                    //    {
                    //        id = cEvent.id,
                    //        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                    //        startRecur = cEvent.startRecur,
                    //        endRecur = cEvent.endRecur,
                    //        daysOfWeek = cEvent.daysOfWeek,
                    //        color = cEvent.color,

                    //    });
                    //}
                }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFriendsEvents(string id, DateTime start, DateTime end)
        {
            var profileService = CreateProfileService();
            var profile = profileService.GetByUsername(id);
            var events = new List<Event>();

            foreach (var cEvent in profile.Events)
            {
                if (cEvent.start != "--")
                {
                    events.Add(new Event()
                    {
                        id = cEvent.id,
                        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                        start = cEvent.start,
                        end = cEvent.end,
                        color = cEvent.color,
                        url = cEvent.url,

                    });
                }
                //if (cEvent.startRecur != "--")
                //{
                //    events.Add(new Event()
                //    {
                //        id = cEvent.id,
                //        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                //        startRecur = cEvent.startRecur,
                //        endRecur = cEvent.endRecur,
                //        daysOfWeek = cEvent.daysOfWeek,
                //        color = cEvent.color,

                //    });
                //}
            }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        private ProfileServices CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }
    }
}