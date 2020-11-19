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
    public class BusinessController : Controller
    {
        // GET: Business
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusinessCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateService();
            string result = service.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Business was created.";
                return RedirectToAction("Index");
            };
            if (result == "invalid city")
                ModelState.AddModelError("", $"{model.City} could not be found.");
            else ModelState.AddModelError("", "Business could not be created.");

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateService();
            var detail = service.GetByID(id);
            var model =
                new BusinessEdit
                {
                    ID = detail.ID,
                    Name = detail.Name,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode,
                    Hours = detail.Hours,
                    PhoneNumber = detail.PhoneNumber,
                    Website = detail.PhoneNumber,
                    LiveMusic = detail.LiveMusic,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BusinessEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateService();

            string result = (service.Update(model, id));
            if (result == "okay")
            {
                TempData["SaveResult"] = "Business updated!";
                return RedirectToAction($"State/{model.State}");
            }
            if (result == "invalid city")
                ModelState.AddModelError("", $"{model.City} could not be found.");
            else ModelState.AddModelError("", "Business could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var service = CreateService();
            var model = service.GetByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBusiness(int id)
        {
            var service = CreateService();

            service.Delete(id);

            TempData["SaveResult"] = "Business was deleted";

            return RedirectToAction("Index");
        }
        private BusinessService CreateService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BusinessService();
            return service;
        }
    }
}