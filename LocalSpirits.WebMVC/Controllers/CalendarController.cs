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

        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var viewModel = new Event();
            var events = new List<Event>();
            start = DateTime.Today;
            end = DateTime.Today;

            events.Add(new Event()
            {
                title = "All Day Event",
                startRecur = $"2020-11-22",
                daysOfWeek = new int[] { 5, 6 },
                color = "Green",

            });



            //events.Add(new EventViewModel()
            //{
            //    id = 10,
            //    title = "Recurring Event ",
            //    color = "Green",
            //    startRecur = start.ToString(),
            //    allDay = false
            //}); ;


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBusinessEvents(int id, DateTime start, DateTime end)
        {
            var businessService = new BusinessService();
            var business = businessService.GetByID(id);
            var events = new List<Event>();
            start = DateTime.Today;
            end = DateTime.Today;

            foreach (var cEvent in business.Events )
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
                        title = cEvent.title,
                        startRecur = cEvent.startRecur,
                        daysOfWeek = cEvent.daysOfWeek,
                        color = cEvent.color,
                        //startRecur = $"2020-11-22",

                    });
                }
                
            }

            
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}