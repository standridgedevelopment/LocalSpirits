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
                        //Events = events,
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

                    List<Event> events = GetEvents(entity.AllVisits);

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

                    List<Event> events = GetEvents(entity.AllVisits);

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
                    List<Event> events = GetEvents(profile.AllVisits);
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
        public List<Event> GetEvents(List<Visited> AllVisits)
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
                        daysOfWeek = visit.Event.DaysOfWeekConverted.ToArray(),
                        startRecur = visit.Event.startRecur,
                        endRecur = visit.Event.endRecur,
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
                            foreach (var activity in otherProfile.Feed)
                            {
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
                                        ObjectID = activity.ObjectID,
                                        ObjectType = activity.ObjectType,
                                        Created = activity.Created,
                                    };
                                    activityFeed.Add(activityItem);
                                }
                            }
                        }
                        //BusinessProfile
                        if (friend.BusinessID != null)
                        {
                            var otherProfile = ctx.Businesses.Single(e => e.ID == friend.BusinessID);
                            foreach (var activity in otherProfile.Feed)
                            {
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
                                        Created = activity.Created,
                                    };
                                    activityFeed.Add(activityItem);
                                }
                            }
                        }

                    }
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
                    };
                    activityFeed.Add(activityItem);
                }
                return activityFeed;
            }

        }
    }
}
