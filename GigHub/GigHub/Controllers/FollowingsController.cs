using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Followings.Any(x => x.FolloweeId == dto.FollowerId && x.FolloweeId == userId);
            if (exists)
                return BadRequest("Already follwings");

            var following = new Following
            {
                FolloweeId = userId, 
                FollowerId = dto.FollowerId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }


    }

}
