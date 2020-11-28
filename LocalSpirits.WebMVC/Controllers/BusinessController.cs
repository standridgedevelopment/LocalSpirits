using LocalSpirits.Data;
using LocalSpirits.Models.ActivityFeed;
using LocalSpirits.Models.Business;
using LocalSpirits.Models.Visited;
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

        public ActionResult Create(int id)
        {
            var model = new BusinessCreate();
            model.CityID = id;
            ModelState.Clear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BusinessCreate model, string id)
        {
            if (!ModelState.IsValid) return View(model);

            var businessService = CreateBusinessService();
            string result = businessService.Create(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Business was created.";
                return RedirectToAction($"Details/{model.CityID}", "City");
            }
            else ModelState.AddModelError("", "Business could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var businessService = CreateBusinessService();
            var visitedService = CreateVisitedService();
            var visit = visitedService.GetVisitByBusinessID(id);
            var model = businessService.GetByID(id);

            if (visit.Rating > 1 && visit != null)
            {
                model.ReviewFromUser = true;
            }

            ModelState.Clear();

            return View(model);
        }

        public ActionResult Search(string id)
        {
            if (id != null)
            {
                var businessService = CreateBusinessService();
                var model = businessService.GetByName(id);
                ModelState.Clear();
                return View(model);
            }
            //string state =  $"{city.State}"
            else return RedirectToAction($"State", "City");
        }
        public ActionResult Edit(int id)
        {
            var businessService = CreateBusinessService();
            var detail = businessService.GetByID(id);
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

            var businessService = CreateBusinessService();

            string result = (businessService.Update(model, id));
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
        public ActionResult AddRating(int id)
        {
            var businessService = CreateBusinessService();
            var visitedService = CreateVisitedService();
  
            var foundVisit = visitedService.GetVisitByBusinessID(id);
            ModelState.Clear();
            if (foundVisit.BusinessID == null)
            {
                var newVisit = new VisitedCreate();
                newVisit.BusinessID = id;
                visitedService.CreateVisit(newVisit);
                var model = new VisitedDetail();
                model.BusinessID = id;
                return View(model);
            }
            return View(foundVisit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRating(int id, VisitedDetail model)
        {
            var visitedService = CreateVisitedService();

            if (!ModelState.IsValid) return View(model);

            string result = visitedService.UpdateBusinessFollow(model, model.BusinessID);
            if (result == "Okay")
            {
                TempData["SaveResult"] = "Rating updated!";
                return RedirectToAction($"Details/{model.BusinessID}");
            }
            else ModelState.AddModelError("", "Business could not be updated.");
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            var service = CreateBusinessService();
            var model = service.GetByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBusiness(int id)
        {
            var businessService = CreateBusinessService();

            businessService.Delete(id);

            TempData["SaveResult"] = "Business was deleted";

            return RedirectToAction("Index");
        }

        public ActionResult CheckIfFollowing(int id)
        {
            var businessService = CreateBusinessService();
            var friendService = CreateFriendsService();

            var thisBusiness = businessService.GetByID(id);
            var checkFollowing = friendService.CheckForFollow(id);
            ModelState.Clear();
            if (checkFollowing == true)
                return PartialView("Following", thisBusiness);
            return PartialView("NotFollowing", thisBusiness);
        }
        public ActionResult FollowBusiness(int id)
        {
            var businessService = CreateBusinessService();
            var profileService = CreateProfileService();
            var friendService = CreateFriendsService();

            var thisBusiness = businessService.GetByID(id);
            var checkFollowing = friendService.CheckForFollow(id);

            var activityFeedItem = new ActivityFeedCreate();
            activityFeedItem.Activity = TypeOfActivity.Follow;
            activityFeedItem.Content = $"{thisBusiness.Name}, {thisBusiness.City}";
            activityFeedItem.ObjectID = thisBusiness.ID;
            activityFeedItem.ObjectType = "Business";
            activityFeedItem.BusinessID = id;


            if (checkFollowing == true)
            {
                profileService.RemoveFeedItem(activityFeedItem);
                friendService.StopFollowingBusiness(id);
                return RedirectToAction($"Details/{id}", "Business");
            }
            profileService.CreateFeedItem(activityFeedItem);
            friendService.FollowBusiness(id);

            return RedirectToAction($"Details/{id}", "Business");

        }
        private BusinessService CreateBusinessService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new BusinessService();
            return service;
        }
        private VisitedService CreateVisitedService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new VisitedService(userId);
            return service;
        }
        private ProfileServices CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }
        private FriendsService CreateFriendsService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FriendsService(userId);
            return service;
        }
    }
}