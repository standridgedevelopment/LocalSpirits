using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult CheckForFriend(string id)
        {
            var profileService = CreateProfileService();
            var friendService = CreateFriendService();
            string result = friendService.CheckForFriend(id);
            var model = profileService.GetByUsername(id);
            ModelState.Clear();
            if (result == "pending") return View("PendingFriends", model);
            if (result == "friends") return View("AreFriends", model);
            return View("NotFriends", model);
        }

        public ActionResult SendFriendRequest(string id)
        {
            var friendService = CreateFriendService();
            friendService.SendFriendRequest(id);
            return RedirectToAction($"Index/{id}", "Profile");
        }
        public ActionResult CancelFriendRequest(string id)
        {
            var friendService = CreateFriendService();
            friendService.CancelFriendRequest(id);
            return RedirectToAction($"Index/{id}", "Profile");
        }
        public ActionResult AcceptFriendRequest(string id)
        {
            var friendService = CreateFriendService();

            friendService.AddFriend(id);
            return RedirectToAction($"FriendsList");
        }

        public ActionResult RemoveFriend(string id)
        {
            var friendService = CreateFriendService();

            friendService.RemoveFriend(id);
            return RedirectToAction($"FriendsList");
        }

        public ActionResult FriendsList()
        {
            var friendService = CreateFriendService();
            var friendsList = friendService.GetFriendsList();
            return View(friendsList);
        }

        public ActionResult FriendRequests()
        {
            var friendService = CreateFriendService();
            var friendRequests = friendService.GetFriendsRequests();
            if (friendRequests.Count != 0)
                return View(friendRequests);
            else return View(friendRequests);
        }

        private ProfileServices CreateProfileService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);
            return service;
        }
        private FriendsService CreateFriendService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new FriendsService(userId);
            return service;
        }
    }
}