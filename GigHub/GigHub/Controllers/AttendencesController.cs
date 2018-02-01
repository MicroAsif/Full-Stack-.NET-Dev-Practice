using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IHttpActionResult Attendence([FromBody] int gigId)
        {
            var attendeeId = "0a7ccbf1-6f3d-4f2c-92d6-1ebfc3b324f3";
            var exits = _context.Attendences.Any(a => a.AttendeeId == attendeeId && a.GigId == gigId);
            if (exits)
                return BadRequest("the attendence already exists");
            var attendecne = new Attendence
            {
                GigId = gigId,
                AttendeeId = attendeeId
            };
            _context.Attendences.Add(attendecne);
            _context.SaveChanges();
            return Ok();
        }
    }
}
