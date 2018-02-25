using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
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
            var exists = _context.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == dto.FollowerId);
            if (exists)
                return BadRequest("Already follwings");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FollowerId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(string id)
        {
            var usedId = User.Identity.GetUserId();

            var following = _context.Followings
                .SingleOrDefault(f => f.FollowerId == usedId && f.FolloweeId == id);

            if (following == null)
                return NotFound();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);

        }


    }

}
