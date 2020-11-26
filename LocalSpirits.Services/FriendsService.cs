using LocalSpirits.Data;
using LocalSpirits.Models.Friends;
using LocalSpirits.Models.Profile;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class FriendsService
    {
        private readonly Guid _userId;

        public FriendsService(Guid userId)
        {
            _userId = userId;
        }
        public bool AddFriend(string username)
        {
            

            using (var ctx = new ApplicationDbContext())
            {
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);
                var yourFriendObject = new Friend()
                {
                    FriendsUsername = otherProfile.Username,
                    ProfileID = _userId,
                    FriendsID = otherProfile.ID,
                };
                var theirFriendObject = new Friend()
                {
                    FriendsUsername = userProfile.Username,
                    ProfileID = otherProfile.ID,
                    FriendsID = _userId,
                };

                var deleteFriendRequest = ctx.FriendRequests.Where(e => e.ProfileID == _userId && e.FriendsID == otherProfile.ID)
                    .Single();
                var deleteOtherRequest = ctx.FriendRequests.Where(e => e.ProfileID == otherProfile.ID && e.FriendsID == _userId)
                    .Single();
                ctx.FriendRequests.Remove(deleteFriendRequest);
                ctx.FriendRequests.Remove(deleteOtherRequest);
                ctx.Friends.Add(yourFriendObject);
                ctx.Friends.Add(theirFriendObject);
                return ctx.SaveChanges() == 4;
            }
        }

        public bool RemoveFriend(string username)
        {


            using (var ctx = new ApplicationDbContext())
            {
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);

                var deleteFriend= ctx.Friends.Where(e => e.ProfileID == _userId && e.FriendsID == otherProfile.ID)
                    .Single();
                var otherDeleteFriend = ctx.Friends.Where(e => e.ProfileID == otherProfile.ID && e.FriendsID == _userId)
                    .Single();
             
                ctx.Friends.Remove(deleteFriend);
                ctx.Friends.Remove(otherDeleteFriend);
                return ctx.SaveChanges() == 2;
            }
        }

        public bool SendFriendRequest(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);

                var sentRequest = new FriendRequest()
                {
                    Sender = userProfile.FullName,
                    Reciever = otherProfile.FullName,
                    ProfileID = _userId,
                    FriendsID = otherProfile.ID,
                    SendersUsername = userProfile.Username,
                    Created = DateTime.Now
                };
                var recievedRequest = new FriendRequest()
                {
                    Sender = userProfile.FullName,
                    Reciever = otherProfile.FullName,
                    ProfileID = otherProfile.ID,
                    FriendsID = _userId,
                    SendersUsername = userProfile.Username,
                    Created = DateTime.Now
                };
                ctx.FriendRequests.Add(sentRequest);
                ctx.FriendRequests.Add(recievedRequest);
                return ctx.SaveChanges() == 2;
            }
        }
        public bool CancelFriendRequest(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);
                var friendRequest = ctx.FriendRequests.Single(e => e.ProfileID == _userId && e.FriendsID == otherProfile.ID);
                var otherRequest = ctx.FriendRequests.Single(e => e.ProfileID == otherProfile.ID && e.FriendsID == _userId);
                ctx.FriendRequests.Remove(friendRequest);
                ctx.FriendRequests.Remove(otherRequest);
                return ctx.SaveChanges() == 1;
            }
        }
        public string CheckForFriend(string username)
        {
            var profileService = new ProfileServices(_userId);
            using (var ctx = new ApplicationDbContext())
            {
                var myProfile = profileService.GetProfile();
                var otherProfile = profileService.GetByUsername(username);
                try
                {
                    foreach (var friendRequest in myProfile.FriendRequests)
                    {
                        if (friendRequest.Reciever == otherProfile.FullName)
                            return "pending";
                    }

                }
                catch { }
                foreach (var friend in myProfile.FriendsList)
                {
                    if (friend.FriendsUsername == username)
                        return "friends";
                }
                return "not friends";
            }
        }
        public List<ProfileDetail> GetFriendsList()
        {
            var profileService = new ProfileServices(_userId);
            var foundFriends = new List<ProfileDetail>();
            using (var ctx = new ApplicationDbContext())
            {
                var userProfile = profileService.GetProfile();

                foreach (var friend in userProfile.FriendsList)
                {
                    var friendsProfile = profileService.GetByUsername(friend.FriendsUsername);
                    var friendInList = new ProfileDetail
                    {
                        FullName = friendsProfile.FullName,
                        Username = friendsProfile.Username,
                        FirstName = friendsProfile.FirstName,
                        LastName = friendsProfile.LastName,
                        City = friendsProfile.City,
                        State = friendsProfile.State,
                        ZipCode = friendsProfile.ZipCode,

                    };
                    foundFriends.Add(friendInList);
                }
            }
            return foundFriends;
        }
        public List<FriendRequestListItem> GetFriendsRequests()
        {
            var friendsRequests = new List<FriendRequestListItem>();
            using (var ctx = new ApplicationDbContext())
            {
                var foundRequests = ctx.FriendRequests.Where(e => e.ProfileID == _userId).ToList();
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);

                foreach (var request in userProfile.FriendRequests)
                {
                    if (request.SendersUsername != userProfile.Username)
                    {
                        var friendInList = new FriendRequestListItem
                        {
                            ProfileID = request.ProfileID,
                            FullName = request.Sender,
                            TimeSent = request.Created,
                            Username = request.SendersUsername,
                        };
                        friendsRequests.Add(friendInList);
                    }

                }


            }
            return friendsRequests;
        }
    }
}
