using LocalSpirits.Data;
using LocalSpirits.Models.ActivityFeed;
using LocalSpirits.Models.Comment;
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
                ZipCode = model.ZipCode,
                ProfilePicture = model.GetProfilePicture,
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
                        ProfilePicture = entity.ProfilePicture,
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
                        ProfilePicture = entity.ProfilePicture,
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
                        ProfilePicture = entity.ProfilePicture,
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
                entity.ProfilePicture = model.GetProfilePicture;

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
        public bool UpdateFeedItem(ActivityFeedCreate model)
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
                var activity = $"{model.Activity}";
                try
                {
                    var oldActivity = ctx.ActivityFeed.Where(e => e.ObjectID == model.ObjectID && e.Activity == activity && e.UserID == _userId).First();
                    ctx.ActivityFeed.Remove(oldActivity);
                }
                catch { }
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
                            var orderedFeed = otherProfile.Feed.OrderByDescending(d => d.Created.DateTime).ToList();
                            foreach (var activity in orderedFeed)
                            {
                                bool likedByUser = LikedByUser(activity);
                                var timePosted = GetFeedPostTime(activity);

                                //Who Liked the Post?
                                var whoLiked = new List<Profile>();
                                foreach (var like in activity.Likes)
                                {
                                    whoLiked.Add(like.Profile);
                                }

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
                                        ProfilePicture = otherProfile.ProfilePicture,
                                        UsersFullName = userProfile.FullName,
                                        ObjectID = activity.ObjectID,
                                        ObjectType = activity.ObjectType,
                                        Created = activity.Created,
                                        BusinessID = activity.BusinessID,
                                        WhoLiked = whoLiked,
                                        AmountOfLikes = activity.AmountOfLikes,
                                        LikedByUser = likedByUser,
                                        WhenPosted = timePosted,


                                    };
                                    activityFeed.Add(activityItem);
                                }
                                else break;
                            }
                        }
                        //BusinessProfile
                        if (friend.BusinessID != null)
                        {
                            var otherProfile = ctx.Businesses.Single(e => e.ID == friend.BusinessID);
                            var orderedFeed = otherProfile.Feed.OrderByDescending(d => d.Created.DateTime).ToList();
                            foreach (var activity in orderedFeed)
                            {
                                bool likedByUser = LikedByUser(activity);
                                var timePosted = GetFeedPostTime(activity);

                                //Who Liked The Post
                                var whoLiked = new List<Profile>();
                                foreach (var like in activity.Likes)
                                {
                                    whoLiked.Add(like.Profile);
                                }

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
                                        WhoLiked = whoLiked,
                                        AmountOfLikes = activity.AmountOfLikes,
                                        LikedByUser = likedByUser,
                                        WhenPosted = timePosted,
                                    };
                                    activityFeed.Add(activityItem);
                                }
                                else break;
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
                    var whoLiked = new List<Profile>();
                    foreach (var like in activity.Likes)
                    {
                        whoLiked.Add(like.Profile);
                    }

                    var activityItem = new ActivityFeedListItem
                    {
                        ID = activity.ID,
                        UserID = activity.UserID,
                        Name = otherProfile.FullName,
                        Content = activity.Content,
                        Activity = activity.Activity,
                        Username = otherProfile.Username,
                        ProfilePicture = otherProfile.ProfilePicture,
                        ObjectID = activity.ObjectID,
                        ObjectType = activity.ObjectType,
                        Created = activity.Created,
                        BusinessID = activity.BusinessID,
                        WhoLiked = whoLiked,
                        AmountOfLikes = activity.AmountOfLikes,
                        LikedByUser = likedByUser,
                        WhenPosted = timePosted,
                    };
                    activityFeed.Add(activityItem);
                }
                activityFeed = activityFeed.OrderByDescending(d => d.Created.DateTime).ToList();

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
                catch { }
                return null;
            }
        }
        public ActivityFeedListItem GetFeedItem(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var activity = ctx.ActivityFeed.Single(e => e.ID == id);
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var activityItem = new ActivityFeedListItem();

                bool likedByUser = LikedByUser(activity);
                var timePosted = GetFeedPostTime(activity);
                var whoLiked = new List<Profile>();
                foreach (var like in activity.Likes)
                {
                    whoLiked.Add(like.Profile);
                }

                if (activity.UserID != null)
                {
                    activityItem = new ActivityFeedListItem
                    {
                        ID = activity.ID,
                        UserID = activity.UserID,
                        Name = activity.Profile.FullName,
                        Content = activity.Content,
                        Activity = activity.Activity,
                        Username = activity.Profile.Username,
                        ProfilePicture = activity.Profile.ProfilePicture,
                        UsersFullName = userProfile.FullName,
                        ObjectID = activity.ObjectID,
                        ObjectType = activity.ObjectType,
                        Created = activity.Created,
                        BusinessID = activity.BusinessID,
                        WhoLiked = whoLiked,
                        AmountOfLikes = activity.AmountOfLikes,
                        AmountOfComments = activity.AmountOfComments,
                        Comments = activity.Comments,
                        Likes = activity.Likes,
                        LikedByUser = likedByUser,
                        WhenPosted = timePosted,
                    };
                }
                else
                {
                    activityItem = new ActivityFeedListItem
                    {
                        ID = activity.ID,
                        UserID = activity.UserID,
                        Name = activity.Business.Name,
                        Content = activity.Content,
                        Activity = activity.Activity,
                        ObjectID = activity.ObjectID,
                        ObjectType = activity.ObjectType,
                        UsersFullName = userProfile.FullName,
                        Created = activity.Created,
                        BusinessID = activity.BusinessID,
                        WhoLiked = whoLiked,
                        AmountOfLikes = activity.AmountOfLikes,
                        AmountOfComments = activity.AmountOfComments,
                        Likes = activity.Likes,
                        Comments = activity.Comments,
                        LikedByUser = likedByUser,
                        WhenPosted = timePosted,
                    };
                }
                return activityItem;
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
        public bool CommentLikedByUser(Comment comment)
        {
            foreach (var like in comment.Likes)
            {
                if (like.UserID == _userId)
                {
                    return true;
                }

            }
            return false;
        }
        public CommentListItem GetComment(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var comment = ctx.Comments.Single(e => e.CommentID == id);
                var userProfile = ctx.Profiles.Single(e => e.ID == _userId);
                var commentItem = new CommentListItem();

                bool likedByUser = CommentLikedByUser(comment);
                var timePosted = GetCommentPostTime(comment);

                commentItem = new CommentListItem
                {
                    CommentID = comment.CommentID,
                    ActivityFeedID = comment.FeedID,
                    SenderFullName = comment.SenderFullName,
                    CommentContent = comment.CommentContent,
                    SenderUsername = comment.SenderUsername,
                    Created = comment.Created,
                    AmountOfLikes = comment.AmountOfLikes,
                    AmountOfReplies = comment.AmountOfReplies,
                    Replies = comment.Replies,
                    Likes = comment.Likes,
                    LikedByUser = likedByUser,
                    WhenPosted = timePosted,
                };
                return commentItem;
            }
        }
        public bool GenerateLikeNotification(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundLike = ctx.Likes.Single(e => e.ActivityFeedID == id && e.UserID == _userId);
                var profile = GetProfile();

                var notification = new Notification
                {
                    SenderFullName = foundLike.Profile.FullName,
                    SenderUsername = foundLike.Profile.Username,
                    SendersProfilePicture = profile.ProfilePicture,
                    Profile_ID = foundLike.ActivityFeed.UserID,
                    ActivityFeedID = id,
                    TimeCreated = DateTimeOffset.Now,
                    Recieved = false,
                };

                ctx.Notifications.Add(notification);
                ctx.SaveChanges();
                return true;

            }
        }
        public bool GenerateCommentNotification(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var foundComment = ctx.Comments.Where(e => e.FeedID == id && e.SenderID == _userId).First();
                var profile = GetProfile();

                var notification = new Notification
                {
                    SenderFullName = foundComment.SenderFullName,
                    SenderUsername = foundComment.SenderUsername,
                    SendersProfilePicture = profile.ProfilePicture,
                    Profile_ID = foundComment.ActivityFeed.UserID,
                    CommentID = foundComment.CommentID,
                    ActivityFeedID = id,
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
        public ICollection<Notification> GetAllNotifications()
        {

            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Profiles.Single(e => e.ID == _userId);
                foreach (var item in entity.Notifications)
                {
                    if (item.Recieved == false)
                        item.Recieved = true;
                }
                ctx.SaveChanges();

                //List<Event> events = GetEvents(entity.AllVisits);

                return entity.Notifications;

            }
        }
        public bool NewComment(CommentCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Comment
                {
                    SenderFullName = model.SenderFullName,
                    SenderUsername = model.SenderUsername,
                    SenderProfilePicture = model.SenderProfilePicture,
                    SenderID  = _userId,
                    FeedID = model.FeedID,
                    Created = DateTimeOffset.Now,
                    CommentContent = model.CommentContent,
                };

                ctx.Comments.Add(entity);
                ctx.SaveChanges();
                return true;

            }
        }
        public bool NewReply(ReplyCreate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = new Reply
                {
                    SenderFullName = model.SenderFullName,
                    SenderUsername = model.SenderFullName,
                    CommentID = model.CommentID,
                    Created = DateTimeOffset.Now,
                    ReplyContent = model.ReplyContent,
                };

                ctx.Replies.Add(entity);
                ctx.SaveChanges();
                return true;

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
        public string GetCommentPostTime(Comment comment)
        {
            var timePosted = DateTimeOffset.Now - comment.Created;
            if (timePosted.Days > 0) return $"{timePosted.Days}d";
            if (timePosted.Hours > 0) return $"{timePosted.Hours}h";
            if (timePosted.Minutes > 0) return $"{timePosted.Minutes}m";
            return "now";
        }
    }
}
