using LocalSpirits.Models.Profile;
using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            var service = CreateService();
            var model = service.GetProfile();
            return View(model);
        }

        //GET
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();

            if (service.CreateUser(model))
            {
                TempData["SaveResult"] = "Your profile was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Profile could not be created.");

            return View(model);
        }

        public ActionResult Edit()
        {
            var service = CreateService();
            var detail = service.GetProfile();
            var model =
                new ProfileEdit
                {
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();

            if (service.UpdateProfile(model))
            {
                TempData["SaveResult"] = "Your profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your profile could not be updated.");
            return View(model);
        }

        private ProfileServices CreateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }
    }
}