using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context; 
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            string userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id && g.ArtistId == userId);

            if (gig.IsCancel)
                return NotFound();

            gig.IsCancel = true;

            var notification = new Notification
            {
                Gig = gig, 
                Type = NotificationType.GigCanceled, 
                DateTime = DateTime.Now
            };
            var attendees = _context.Attendences.Where(a => a.GigId == gig.Id).Select(x => x.Attendee).ToList();

            foreach (var attendee in attendees)
            {
                var userNotification = new UserNotification
                {
                    User = attendee, 
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
