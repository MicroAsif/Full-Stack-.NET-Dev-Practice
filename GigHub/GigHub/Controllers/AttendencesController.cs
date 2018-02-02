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
    public class AttendencesController : ApiController
    {
        private ApplicationDbContext _context; 
        public AttendencesController()
        {
            _context = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attendence(AttendenceDto dto)
        {
            var attendeeId = User.Identity.GetUserId();
            var exits = _context.Attendences.Any(a => a.AttendeeId == attendeeId && a.GigId == dto.GigId);
            if (exits)
                return BadRequest("the attendence already exists");
            var attendecne = new Attendence
            {
                GigId = dto.GigId,
                AttendeeId = attendeeId
            };
            _context.Attendences.Add(attendecne);
            _context.SaveChanges();
            return Ok();
        }
    }
}
