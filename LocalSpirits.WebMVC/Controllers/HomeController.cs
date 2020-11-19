using LocalSpirits.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
	public class HomeController : Controller
	{
		[HttpGet]
		public ActionResult Index()
		{
			return View(new EventViewModel());
		}

		public JsonResult GetEvents(DateTime start, DateTime end)
		{
			var viewModel = new EventViewModel();
			var events = new List<EventViewModel>();
			start = DateTime.Today.AddDays(-14);
			end = DateTime.Today.AddDays(-11);

			for (var i = 1; i <= 5; i++)
			{
				events.Add(new EventViewModel()
				{
					id = i,
					title = "Event " + i,
					start = start.ToString(),
					end = end.ToString(),
					allDay = false
				});

				events.Add(new EventViewModel()
				{
					id = i,
					title = "Other Event " + i,
					start = start.ToString(),
					end = end.ToString(),
					allDay = false
				});
				events.Add(new EventViewModel()
				{
					id = i,
					title = "Other Other Event " + i,
					start = start.ToString(),
					end = end.ToString(),
					allDay = false
				});
				events.Add(new EventViewModel()
				{
					id = 10,
					title = "Recurring Event ",
					color = "Green",
					start = start.ToString(),
					startRecur = start.ToString(),
					allDay = false
				}); ;

				start = start.AddDays(7);
				end = end.AddDays(7);

			}
            


            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
		}
	}
	//Old!!!
	//public ActionResult Index()
	//{
	//    return View();
	//}

	//public ActionResult About()
	//{
	//    ViewBag.Message = "Your application description page.";

	//    return View();
	//}

	//public ActionResult Contact()
	//{
	//    ViewBag.Message = "Your contact page.";

	//    return View();
	//}
}
