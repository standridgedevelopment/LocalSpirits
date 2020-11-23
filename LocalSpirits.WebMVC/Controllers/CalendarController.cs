using LocalSpirits.Data;
using LocalSpirits.Models.Business;
using LocalSpirits.Services;
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

        public JsonResult GetEvents(int id, DateTime start, DateTime end)
        {
            var businessService = new BusinessService();
            var cityService = new CityService();
            var city = cityService.GetCityByID(id);
            var Businessess = businessService.GetByCityName(city.Name, city.State);
            var events = new List<Event>();

            foreach (var business in Businessess)
            {
                foreach (var cEvent in business.Events)
                {
                    if (cEvent.start != null)
                    {
                        events.Add(new Event()
                        {
                            title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                            start = cEvent.start,
                            end = cEvent.end,
                            color = cEvent.color,
                            url = cEvent.url,

                        });
                    }
                    if (cEvent.startRecur != null)
                    {
                        events.Add(new Event()
                        {
                            title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                            startRecur = cEvent.startRecur,
                            endRecur = cEvent.endRecur,
                            daysOfWeek = cEvent.DaysOfWeekConverted.ToArray(),
                            color = cEvent.color,
                            //startRecur = $"2020-11-22",

                        });
                    }
                }  
            }
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBusinessEvents(int id, DateTime start, DateTime end)
        {
            var businessService = new BusinessService();
            var business = businessService.GetByID(id);
            var events = new List<Event>();

            foreach (var cEvent in business.Events)
            {
                if (cEvent.start != null)
                {
                    events.Add(new Event()
                    {
                        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                        start = cEvent.start,
                        end = cEvent.end,
                        color = cEvent.color,
                        url = cEvent.url,

                    });
                }
                if (cEvent.startRecur != null)
                {
                    events.Add(new Event()
                    {
                        title = $"{cEvent.TypeOfEvent} at {cEvent.Business.Name}, {cEvent.City}",
                        startRecur = cEvent.startRecur,
                        endRecur = cEvent.endRecur,
                        daysOfWeek = cEvent.DaysOfWeekConverted.ToArray(),
                        color = cEvent.color,
                        //startRecur = $"2020-11-22",

                    });
                }

            }


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}