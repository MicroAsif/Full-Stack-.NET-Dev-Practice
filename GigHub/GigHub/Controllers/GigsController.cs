using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        // GET: Gigs
        private readonly ApplicationDbContext _context;
        public GigsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize]
        public ActionResult Create()
        {
            var genres = new GigFormViewModel
            {
                Genres =  _context.Genres.ToList()
            };
            return View(genres);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View(viewModel);
            }
            var gigModel = new Gig()
            {
                Venue = viewModel.Venue, 
                ArtistId = User.Identity.GetUserId() , 
                DateTime = viewModel.GetDateTime(), 
                GenreId = viewModel.Genre 
            };
            _context.Gigs.Add(gigModel);
            _context.SaveChanges();
            return RedirectToAction("Mine");
        }

        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _context.Attendences.Where(x => x.AttendeeId == userId).Select(x => x.Gig)
                .Include(x => x.Artist).Include(g => g.Genre).ToList();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                ShowActivity = User.Identity.IsAuthenticated, 
                Heading = "Gigs I'm Attending"
            };
            return View("Gigs", viewModel);
        }

        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var mine = _context.Gigs.Where(x => x.ArtistId == userId && x.DateTime > DateTime.Now).Include(x => x.Genre)
                .ToList();
            return View(mine);
        }

       
    }
}