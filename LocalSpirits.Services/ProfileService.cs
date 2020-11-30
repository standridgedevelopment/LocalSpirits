using LocalSpirits.Data;
using LocalSpirits.Models.ActivityFeed;
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
    public class ProfileServices
    {
        readonly List<ProfileDetail> searchResults = new List<ProfileDetail>();

        private readonly Guid _userId;

        public ProfileServices(Guid userId)
        {
            _userId = userId;
        }
        public string CreateUser(ProfileCreate model)
        {
            var checkForUserName = GetByUsername(model.Username);
            if (checkForUserName.Username != null) return "username taken";

            var entity = new Profile()
            {
                ID = _userId,
                Username = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = (Data.StateName)model.State,
                ZipCode = model.ZipCode
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Profiles.Add(entity);
                ctx.SaveChanges();
                return "okay";
            }
        }

        public ProfileDetail GetProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);

                    //List<Event> events = GetEvents(entity.AllVisits);

                    return new ProfileDetail
                    {
                        Username = entity.Username,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Notifications = entity.Notifications,
                        FriendRequests = entity.FriendRequests,
                        FriendsList = entity.FriendsList,
                        Feed = entity.Feed,
                    };
                }
                catch { }
                return new ProfileDetail();

            }

        }
        public ProfileDetail GetFullProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);

                    ICollection<Event> events = GetEvents(entity.AllVisits);

                    return new ProfileDetail
                    {
                        Username = entity.Username,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Events = events,
                        FriendRequests = entity.FriendRequests,
                        FriendsList = entity.FriendsList,
                        Feed = entity.Feed,
                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }
        public ProfileDetail GetIncomingFriendRequests()
        {
            var incomingFriends = new List<FriendRequest>();
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);
                    foreach (var item in entity.FriendRequests)
                    {
                        if (item.SendersUsername != entity.Username)
                            incomingFriends.Add(item);
                    }

                    //List<Event> events = GetEvents(entity.AllVisits);

                    return new ProfileDetail
                    {

                        FriendRequests = incomingFriends,

                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }

        public ProfileDetail GetByUsername(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.Username.ToLower() == username.ToLower());

                    ICollection<Event> events = GetEvents(entity.AllVisits);

                    return new ProfileDetail
                    {
                        Username = entity.Username,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        Events = events,
                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }

        public List<ProfileDetail> GetByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var Profiles = ctx.Profiles.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name)).ToList();
                foreach (var profile in Profiles)
                {
                    ICollection<Event> events = GetEvents(profile.AllVisits);
                    var found = new ProfileDetail
                    {
                        Username = profile.Username,
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        City = profile.City,
                        State = profile.State,
                        ZipCode = profile.ZipCode,
                        Events = events,
                    };
                    searchResults.Add(found);
                }
                return searchResults;
            }
        }



        public bool UpdateProfile(ProfileEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity = ctx.Profiles.Single(e => e.ID == _userId);

                entity.Username = model.Username;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.City = model.City;
                entity.State = model.State;
                entity.ZipCode = model.ZipCode;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Profiles.Single(e => e.ID == _userId);

                ctx.Profiles.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        public ICollection<Event> GetEvents(ICollection<Visited> AllVisits)
        {
            List<Event> events = new List<Event>();
            foreach (var visit in AllVisits)
            {
                if (visit.AddToCalendar == true)
                {
                    var visitdetails = new Data.Event
                    {
                        id = visit.Event.id,
                        title = $"{visit.Event.TypeOfEvent} at {visit.Event.Business.Name}, {visit.Event.City}",
                        TypeOfEvent = visit.Event.TypeOfEvent,
                        BusinessID = visit.Event.BusinessID,
                        Business = visit.Event.Business,
                        start = visit.Event.start,
                        end = visit.Event.end,                     
                        ThirdPartyWebsite = visit.Event.ThirdPartyWebsite,
                        color = visit.Event.color,
                    };
                    events.Add(visitdetails);
                }

            }
            return events;
        }

        public bool CreateFeedItem(ActivityFeedCreate model)
        {
            var entity = new ActivityFeed()
            {
                UserID = _userId,
                BusinessID = model.BusinessID,
                Activity = $"{model.Activity}",
                ObjectID = model.ObjectID,
                ObjectType = model.ObjectType,
                Content = model.Content,
                Created = DateTimeOffset.Now,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ActivityFeed.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }
        public bool CreateBusinessFeedItem(ActivityFeedCreate model)
        {
            var entity = new ActivityFeed()
            {
                BusinessID = model.BusinessID,
                Activity = $"{model.Activity}",
                ObjectID = model.ObjectID,
                ObjectType = model.ObjectType,
                Content = model.Content,
                Created = DateTimeOffset.Now,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.ActivityFeed.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }

        public bool RemoveFeedItem(ActivityFeedCreate model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.ActivityFeed.Single(e => e.UserID == _userId && e.Content == model.Content);
                ctx.ActivityFeed.Remove(entity);
                ctx.SaveChanges();
                return true;
            }

        }
        public bool RemoveBusinessFeedItem(ActivityFeedCreate model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                var eventFeedItems = ctx.ActivityFeed.Where(e => e.ObjectType == model.ObjectType && e.ObjectID == model.ObjectID).ToList();
                var visitedItems = ctx.Visits.Where(e => e.EventID == model.ObjectID).ToList();
                foreach (var events in eventFeedItems)
                {
                    ctx.ActivityFeed.Remove(events);
                }
                foreach (var visits in visitedItems)
                {
                    ctx.Visits.Remove(visits);
                }
                ctx.SaveChanges();
                return true;
            }

        }
        public List<ActivityFeedListItem> GetFullFriendsActivityFeed()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<ActivityFeedListItem> activityFeed = new List<ActivityFeedListItem>();
                try
                {
                    var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                    
                    foreach (var friend in userProfile.FriendsList)
                    {
                        //FriendsProfile
                        if (friend.FriendsID != null)
                        {
                            var otherProfile = ctx.Profiles.Single(e => e.ID == friend.FriendsID);

                            //Liked By User??
                            foreach (var activity in otherProfile.Feed)
                            {
                                bool likedByUser = LikedByUser(activity);
                                var timePosted = GetFeedPostTime(activity);

                                if ((DateTimeOffset.Now - activity.Created).TotalDays <= 3)
                                {
                                    var activityItem = new ActivityFeedListItem
                                    {
                                        ID = activity.ID,
                                        UserID = activity.UserID,
                                        Name = otherProfile.FullName,
                                        Content = activity.Content,
                                        Activity = activity.Activity,
                                        Username = otherProfile.Username,
                                        UsersFullName = userProfile.FullName,
                                        ObjectID = activity.ObjectID,
                                        ObjectType = activity.ObjectType,
                                        Created = activity.Created,
                                        BusinessID = activity.BusinessID,
                                        AmountOfLikes = activity.AmountOfLikes,
                                        LikedByUser = likedByUser,
                                        WhenPosted = timePosted,


                                    };
                                    activityFeed.Add(activityItem);
                                }
                                else continue;
                            }
                        }
                        //BusinessProfile
                        if (friend.BusinessID != null)
                        {
                            var otherProfile = ctx.Businesses.Single(e => e.ID == friend.BusinessID);
                            foreach (var activity in otherProfile.Feed)
                            {
                                bool likedByUser = LikedByUser(activity);
                                var timePosted = GetFeedPostTime(activity);

                                if ((DateTimeOffset.Now - activity.Created).TotalDays <= 3 && activity.Activity != "Follow")
                                {
                                    var activityItem = new ActivityFeedListItem
                                    {
                                        ID = activity.ID,
                                        UserID = activity.UserID,
                                        Name = otherProfile.Name,
                                        Content = activity.Content,
                                        Activity = activity.Activity,
                                        ObjectID = activity.ObjectID,
                                        ObjectType = activity.ObjectType,
                                        UsersFullName = userProfile.FullName,
                                        Created = activity.Created,
                                        BusinessID = activity.BusinessID,
                                        AmountOfLikes = activity.AmountOfLikes,
                                        LikedByUser = likedByUser,
                                        WhenPosted = timePosted,
                                    };
                                    activityFeed.Add(activityItem);
                                }
                                else continue;
                            }
                        }

                    }
                    activityFeed = activityFeed.OrderByDescending(d => d.Created.DateTime).ToList();
                    return activityFeed;
                }
                catch { return activityFeed; }
            }
        }
        public List<ActivityFeedListItem> GetOneFriendsActivityFeed(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);
                List<ActivityFeedListItem> activityFeed = new List<ActivityFeedListItem>();

                foreach (var activity in otherProfile.Feed)
                {
                    bool likedByUser = LikedByUser(activity);
                    var timePosted = GetFeedPostTime(activity);

                    var activityItem = new ActivityFeedListItem
                    {
                        ID = activity.ID,
                        UserID = activity.UserID,
                        Name = otherProfile.FullName,
                        Content = activity.Content,
                        Activity = activity.Activity,
                        Username = otherProfile.Username,
                        ObjectID = activity.ObjectID,
                        ObjectType = activity.ObjectType,
                        Created = activity.Created,
                        BusinessID = activity.BusinessID,
                        AmountOfLikes = activity.AmountOfLikes,
                        LikedByUser = likedByUser,
                        WhenPosted = timePosted,
                    };
                    activityFeed.Add(activityItem);
                }
                return activityFeed;
            }

        }
        public Like GetLike(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                try
                {
                    var foundLike = ctx.Likes.Single(e => e.ActivityFeedID == id && e.UserID == _userId);
                    return foundLike;
                }
                catch {}
            return null;
            }
        }
        public bool LikeFeedItem(int id)
        {
            var entity = new Like()
            {
                UserID = _userId,
                ActivityFeedID = id,
                Liked = true,
                Created = DateTimeOffset.Now,
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Likes.Add(entity);
                ctx.SaveChanges();
                return true;
            }
        }
        public bool UnlikeFeedItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundLike = ctx.Likes.Single(e => e.ActivityFeedID == id && e.UserID == _userId);
                ctx.Likes.Remove(foundLike);
                ctx.SaveChanges();
                return true;

            }
        }
        public bool LikedByUser(ActivityFeed activity)
        {
            foreach (var like in activity.Likes)
            {
                if (like.UserID == _userId)
                {
                    return true;
                }
                
            }
            return false;
        }
        public bool GenerateNotification(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundLike = ctx.Likes.Single(e => e.ActivityFeedID == id && e.UserID == _userId);
                var notification = new Notification
                {
                    SenderFullName = foundLike.Profile.FullName,
                    SenderUsername = foundLike.Profile.Username,
                    Profile_ID = foundLike.ActivityFeed.UserID,
                    TimeCreated = DateTimeOffset.Now,
                    Recieved = false,
                };
               
                ctx.Notifications.Add(notification);
                ctx.SaveChanges();
                return true;

            }
        }
        public ProfileDetail GetIncomingNotifications()
        {
            var notifications = new List<Notification>();
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);
                    foreach (var item in entity.Notifications)
                    {
                        if (item.Recieved == false)
                            notifications.Add(item);
                    }

                    //List<Event> events = GetEvents(entity.AllVisits);

                    return new ProfileDetail
                    {

                        Notifications = notifications,

                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }
        public string GetFeedPostTime(ActivityFeed activity)
        {
            var timePosted = DateTimeOffset.Now - activity.Created;
            if (timePosted.Days > 0) return $"{timePosted.Days}d";
            if (timePosted.Hours > 0) return $"{timePosted.Hours}h";
            if (timePosted.Minutes > 0) return $"{timePosted.Minutes}m";
            return "now";
        }
    }
}
