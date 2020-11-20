using LocalSpirits.Data;
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
                allDay = true,

            }) ;

              
            
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
    }
}