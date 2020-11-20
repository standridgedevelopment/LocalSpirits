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
			return View(new Event());
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
