using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context; 
        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();
           
            var notifications =  _context.UserNotifications.Where(x => x.UserId == userId && !x.IsRead)
                .Select(x=>x.Notification)
                .Include(u => u.Gig.Artist).ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
