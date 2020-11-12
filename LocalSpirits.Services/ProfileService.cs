using LocalSpirits.Data;
using LocalSpirits.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Services
{
    public class ProfileService
    {
        private readonly Guid _userId;

        public ProfileService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateUser(ProfileCreate model)
        {
            var entity = new Profile()
            {
                ID = _userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                State = model.State
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Profiles.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public ProfileDetail GetProfile()
        {
            using (var ctx = new ApplicationDbContext())
            {

                try
                {
                    var entity = ctx.Profiles.Single(e => e.ID == _userId);
                    return new ProfileDetail
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        City = entity.City,
                        State = entity.State,
                    };
                }
                catch { }
                return new ProfileDetail();

            }
        }
        public bool UpdateProfile(ProfileEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {

                var entity = ctx.Profiles.Single(e => e.ID == _userId);

                if (model.FirstName != null) entity.FirstName = model.FirstName;
                if (model.LastName != null) entity.LastName = model.LastName;
                if (model.City != null) entity.City = model.City;
                if (model.State != null) entity.State = model.State;

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
    }
}
