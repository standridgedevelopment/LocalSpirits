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
            var viewModel = new EventViewModel();
            var events = new List<EventViewModel>();
            start = DateTime.Today;
            end = DateTime.Today;

                events.Add(new EventViewModel()
                {
                title = "All Day Event",
                startRecur = $"2020-11-19",
                daysOfWeek = new int[] {0, 4}
                
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
    }
}