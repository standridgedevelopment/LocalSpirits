using LocalSpirits.Data;
using LocalSpirits.Models.Comment;
using LocalSpirits.Models.Friends;
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
        public ActionResult Index(string id)
        {
            var service = CreateProfileService();
            var ownProfile = service.GetProfile();

            if (ownProfile.Username == id || id == null)
                return RedirectToAction("HomeProfile");

            ModelState.Clear();

            return RedirectToAction($"Activity/{id}");
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

            var service = CreateProfileService();
            string result = service.CreateUser(model);
            if (result == "okay")
            {
                TempData["SaveResult"] = "Your profile was created.";
                return RedirectToAction("Index");
            };

            if (result == "username taken")
            {
                ModelState.AddModelError("", "Username not available.");
                TempData["SaveResult"] = "Profile not created.";
                return View(model);
            };
            ModelState.AddModelError("", "Profile could not be created.");

            return View(model);
        }

        //Search By Name Results
        public ActionResult Search(string id)
        {
            if (id != null)
            {
                var service = CreateProfileService();
                var model = service.GetByName(id);
                ModelState.Clear();
                return View(model);
            }
            //string state =  $"{city.State}"
            else return RedirectToAction($"State", "City");
        }

        public ActionResult HomeProfile()
        {
            var service = CreateProfileService();
            var model = service.GetProfile();
            ModelState.Clear();

            return View(model);
        }

        public ActionResult GetNoticiationCount()
        {
            var profileService = CreateProfileService();
            var profile = profileService.GetIncomingNotifications();

            return PartialView("Notifications", profile);
        }
        public ActionResult GetAllNotifications()
        {
            var profileService = CreateProfileService();
            var notifications = profileService.GetAllNotifications();

            return View("_ListNotifications", notifications);
        }
        public ActionResult About(string id)
        {
            var service = CreateProfileService();
            var model = service.GetByUsername(id);

            ModelState.Clear();

            return View(model);
        }
        public ActionResult Activity(string id)
        {
            var profileService = CreateProfileService();
            var activityFeed = profileService.GetOneFriendsActivityFeed(id);
            ModelState.Clear();
            return View("GetFeed", activityFeed);
        }
        public ActionResult LikeFeedItem(int id)
        {
            var profileService = CreateProfileService();
            var foundLike = profileService.GetLike(id);
            if (foundLike != null)
            {
                profileService.UnlikeFeedItem(id);
                return RedirectToAction("Activity", "Home");
            }
            profileService.LikeFeedItem(id);
            profileService.GenerateLikeNotification((id));
            return RedirectToAction("Activity", "Home");
        }
        //public ActionResult LikeProfileItem(int id, string username)
        //{
        //    var profileService = CreateProfileService();
        //    var foundLike = profileService.GetLike(id);
        //    if (foundLike != null)
        //    {
        //        profileService.UnlikeFeedItem(id);
        //        return RedirectToAction($"Index/{username}", "Profile");
        //    }
        //    profileService.LikeFeedItem(id);
        //    return RedirectToAction($"Index/{username}", "Profile");
        //}
        public ActionResult LikeProfileItemAjax(int id, string username)
        {
            var profileService = CreateProfileService();
            var foundLike = profileService.GetLike(id);
            string message = "PersonId";

            if (foundLike != null)
            {
                profileService.UnlikeFeedItem(id);
                return Content(message);
            }
            profileService.LikeFeedItem(id);
            return Content(message);
        }
        public ActionResult GetLikes(int id)
        {
            var profileService = CreateProfileService();
            var feedItem = profileService.GetFeedItem(id);

            return PartialView("_GetCurrentLikes", feedItem);
        }
        public ActionResult GetCommentLikes(int id)
        {
            var profileService = CreateProfileService();
            var comment = profileService.GetComment(id);

            return PartialView("_GetCurrentCommentLikes", comment);
        }
        public ActionResult GetComments(int id)
        {
            var profileService = CreateProfileService();
            var feedItem = profileService.GetFeedItem(id);

            return PartialView("_GetCurrentComments", feedItem);
        }
        public ActionResult GetReplies(int id)
        {
            var profileService = CreateProfileService();
            var comment = profileService.GetComment(id);

            return PartialView("_GetCurrentReplies", comment);
        }
        public ActionResult GetPost(int id)
        {
            var profileService = CreateProfileService();
            var feedItem = profileService.GetFeedItem(id);
            ModelState.Clear();

            return View("PostDetail", feedItem);
        }
        public ActionResult GetComment(int id)
        {
            var profileService = CreateProfileService();
            var comment = profileService.GetComment(id);

            return View("CommentDetail", comment);
        }
        public ActionResult SetHeartState(int id)
        {
            var profileService = CreateProfileService();

            var foundLike = profileService.GetLike(id);

            var activityItem = profileService.GetFeedItem(id);
            if (foundLike != null)
            {
                profileService.UnlikeFeedItem(id);
                return PartialView("_ReloadLikes", activityItem);
            }
            profileService.LikeFeedItem(id);
            profileService.GenerateLikeNotification((id));
            return PartialView("_ReloadLikes", activityItem);
        }
        [HttpGet]
        public ActionResult AddComment(int id)
        {
            var profileService = CreateProfileService();
            var userProfile = profileService.GetProfile();


            var model = new CommentCreate
            {
                SenderFullName = userProfile.FullName,
                SenderUsername = userProfile.Username,
                FeedID = id,
                Created = DateTimeOffset.Now,
            };

            return PartialView("_AddComment", model);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult PostComment(CommentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var profileService = CreateProfileService();
            var activityItem = profileService.GetFeedItem((int)model.FeedID);


            if (profileService.NewComment(model))
            {
                TempData["SaveResult"] = "Comment Posted.";
                profileService.GenerateCommentNotification((int)model.FeedID);
                return PartialView("_ReloadComments", activityItem);
            }

            ModelState.AddModelError("", "Your comment could not be posted.");
            return PartialView("_ReloadComments", activityItem);
        }

        public ActionResult AddReply(int id)
        {
            var profileService = CreateProfileService();
            var userProfile = profileService.GetProfile();


            var model = new ReplyCreate
            {
                SenderFullName = userProfile.FullName,
                SenderUsername = userProfile.Username,
                SenderProfilePicture = userProfile.ProfilePicture,
                CommentID = id,
                Created = DateTimeOffset.Now,
            };

            return PartialView("_AddReply", model);
        }
        public ActionResult PostReply(ReplyCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var profileService = CreateProfileService();
            var comment = profileService.GetComment((int)model.CommentID);


            if (profileService.NewReply(model))
            {
                TempData["SaveResult"] = "Reply Posted.";
                return PartialView("_ReloadReplies", comment);
            }

            ModelState.AddModelError("", "Your reply could not be posted.");
            return PartialView("_ReloadReplies", comment);
        }
        public ActionResult Edit()
        {
            var service = CreateProfileService();
            var detail = service.GetProfile();
            var model =
                new ProfileEdit
                {
                    Username = detail.Username,
                    FirstName = detail.FirstName,
                    LastName = detail.LastName,
                    City = detail.City,
                    State = detail.State,
                    ZipCode = detail.ZipCode,
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateProfileService();

            if (service.UpdateProfile(model))
            {
                TempData["SaveResult"] = "Your profile was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your profile could not be updated.");
            return View(model);
        }

        private ProfileServices CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }
    }
}