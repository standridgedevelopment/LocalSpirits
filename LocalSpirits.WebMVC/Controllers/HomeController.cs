using LocalSpirits.Data;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ApplicationServices;
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

		public ActionResult GetHomeFeed()
		{
			var profileService = CreateProfileService();
			var activityFeed = profileService.GetFullFriendsActivityFeed();
			return View("HomeFeed", activityFeed);
		}

		private ProfileServices CreateProfileService()
		{
			var userId = Guid.Parse(User.Identity.GetUserId());
			var service = new ProfileServices(userId);
			return service;
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
