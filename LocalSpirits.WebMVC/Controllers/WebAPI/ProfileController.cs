using LocalSpirits.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocalSpirits.WebMVC.Controllers.WebAPI
{

    [Authorize]
    [RoutePrefix("/api/Profile")]
    public class ProfileController : ApiController
    {
        private bool SetHeartState(int ActivityId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ProfileServices(userId);

            return service.LikeFeedItem(ActivityId);
        }

        [Route("/Heart/{id}")]
        [HttpPut]
        public bool ToggleHeartOn(int id) => SetHeartState(id, true);

        [Route( "/Heart /{id}")]
        [HttpDelete]
        public bool ToggleHeartOff(int id) => SetHeartState(id, false);
    }
}

