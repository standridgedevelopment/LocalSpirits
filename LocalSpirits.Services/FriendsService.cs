using LocalSpirits.Data;
using LocalSpirits.Models.Friends;
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
        public bool AddFriend(Guid id)
        {
            var entity = new Friend()
            {
                ProfileID = _userId,
                FriendsID = id,
            };

            using (var ctx = new ApplicationDbContext())
            {
                var deleteFriendRequest = ctx.FriendRequests.Where(e => e.ProfileID == id)
                    .Single();
                ctx.FriendRequests.Remove(deleteFriendRequest);
                ctx.Friends.Add(entity);
                return ctx.SaveChanges() == 2;
            }
        }

        public bool SendFriendRequest(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);

                var entity = new FriendRequest()
                {
                    ProfileID = _userId,
                    FriendsID = otherProfile.ID,
                    Created = DateTime.Now
                };
                ctx.FriendRequests.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public bool CancelFriendRequest(string username)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var otherProfile = ctx.Profiles.Single(e => e.Username == username);
                var friendRequest = ctx.FriendRequests.Single(e => e.ProfileID == _userId && e.FriendsID == otherProfile.ID);
                ctx.FriendRequests.Remove(friendRequest);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FriendRequestListItem> GetFriendRequests()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.FriendRequests.Where(e => e.ProfileID == _userId)
                    .Select
                    (e => new FriendRequestListItem
                    {
                        ProfileID = e.ProfileID,
                        FullName = $"{e.FriendsProfile.FirstName}",
                        TimeSent = e.Created,
                    }
                    );
                return query.ToArray();
            }
        }
    }
}
